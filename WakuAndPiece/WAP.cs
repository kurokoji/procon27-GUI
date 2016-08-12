using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WakuAndPiece {
  using Piece = Polygon;
  using Hole = Polygon;

  public partial class WakuAndPiece : Form {
    const string problemExtension = "kudo";
    const string answerExtension = "shinobu";

    public WakuAndPiece() {
      InitializeComponent();
      this.state = new FormState();
      // 変更があったら有効にする
      this.state.piecesMoveChanged += (x) => {
        this.drawPiecesMove.Enabled = x != null;
      };
      this.state.piecesMoveChanged += (x) => {
        this.saveAns.Enabled = x != null;
      };
      this.state.problemChanged += (x) => {
        this.drawPieces.Enabled = x != null;
      };
      this.state.problemChanged += (x) => {
        this.outputSolve.Enabled = x != null;
      };
    }
    
    // 状態(変更があったか)を表すクラス
    class FormState {
      public delegate void PieceMoveDelegate(PieceMove[] piecesMove);
      public delegate void ProblemDelegate(Problem problem);

      private PieceMove[] piecesMove_;
      private Problem problem_;

      public PieceMoveDelegate piecesMoveChanged;
      public ProblemDelegate problemChanged;

      public PieceMove[] piecesMove {
        get { return this.piecesMove_; }
        set {
          this.piecesMove_ = value;
          piecesMoveChanged(this.piecesMove_);
        }
      }
      public Problem problem {
        get { return this.problem_; }
        set {
          this.problem_ = value;
          problemChanged(this.problem_);
        }
      }
    };

    FormState state;

    Problem problem {
      get { return state.problem; }
      set { state.problem = value; }
    }
    PieceMove[] piecesMove {
      get { return state.piecesMove; }
      set { state.piecesMove = value; }
    }

    /* フレーム,ピース情報の読み込み */
    private void readFramePiece_Click(object sender, EventArgs e) {
      ProcessStartInfo readQuestInfo = new ProcessStartInfo("scaller.exe");
      readQuestInfo.UseShellExecute = false;
      readQuestInfo.RedirectStandardOutput = true;

      // 読み込みプロセスの開始
      using (Process readQuestreader = Process.Start(readQuestInfo)) {
        // フレーム,ピース情報を読み込む
        problem = Problem.fromStream(readQuestreader.StandardOutput);
      }
      // リストへの描画
      problem.showpieceList(pieceListpanel);
    }
    
    /* ソルバーにフレーム,ピース情報を出力 */
    private void outputSolve_Click(object sender, EventArgs e) {

      ProcessStartInfo processInfo = new ProcessStartInfo("solver.exe");
      processInfo.UseShellExecute = false;
      processInfo.RedirectStandardInput = true;
      processInfo.RedirectStandardOutput = true;

      using (Process processSolver = Process.Start(processInfo)) {
        // フレーム,ピース情報を書き込む
        problem.toStream(processSolver.StandardInput);
        processSolver.StandardInput.Flush();
        piecesMove = problem.readAnswerStream(processSolver.StandardOutput);
      }
    }

    /* 描画 */
    private void drawPieces_Click(object sender, EventArgs e) {
      canvas.Image = new Bitmap(canvas.Height, canvas.Width);
      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        // アンチエイリアス
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Random rng = new Random();
        // 子コントロールをclear
        canvas.Controls.Clear();
        foreach (Polygon pol in problem.pieces) {
          pol.draw(g, randomBrush(rng), canvas);
        }      
      }
    }

    /* 動かした後の描画 */
    private void drawPiecesMove_Click(object sender, EventArgs e) {
      canvas.Image = new Bitmap(canvas.Height, canvas.Width);
      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        // アンチエイリアス
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Random rng = new Random();
        // 子コントロールをclear
        canvas.Controls.Clear();
        foreach (PieceMove pmv in piecesMove) {
          pmv.draw(g, randomBrush(rng), canvas);
        }
      }
    }
    /* 色をランダムに生成 */
    private Brush randomBrush(Random rng) {
      return new SolidBrush(Color.FromArgb(rng.Next(255), rng.Next(255), rng.Next(255)));
    }

    /* WakuAndPieceが読まれた際の処理(1度だけ) */
    private void WakuAndPiece_Load(object sender, EventArgs e) {
      this.textboxPanel.Controls.Add(canvas);
      this.canvas.Location = new Point(0, 0);
    }

    /* 解答の保存をする際のボタン */
    private void SaveAns_Click(object sender, EventArgs e) {
      // 日付の取得
      DateTime nowDate = DateTime.Now;
      
      // 保存するファイルの名前
      string name = nowDate.ToString("yyyy_MM_dd_HH_mm_ss");
      string problemFileName = String.Format("Problem_{0}.{1}", name, problemExtension);
      string answerFileName = String.Format("Answer_{0}.{1}", name, answerExtension);

      // 問題
      using (StreamWriter sw = new StreamWriter(problemFileName)) {
        problem.toStream(sw);
      }
      // 解答
      using (StreamWriter sw = new StreamWriter(answerFileName)) {
        sw.WriteLine(piecesMove.Length);
        foreach (PieceMove pm in piecesMove) {
          pm.toStream(sw);
        }
      }
    }

    /* 過去の問題と解答の読み込み */
    private void oldProAns_Click(object sender, EventArgs e) {
      // 「ファイルを開く」ダイアログ
      OpenFileDialog proOfd = new OpenFileDialog();
      OpenFileDialog ansOfd = new OpenFileDialog();

      proOfd.Filter = String.Format("{0}ファイル(*.{0})|*.{0}|すべてのファイル(*.*)|*.*", problemExtension);
      proOfd.Title = "開くファイルを選択してください";
      proOfd.RestoreDirectory = true;

      ansOfd.Filter = String.Format("{0}ファイル(*.{0})|*.{0}|すべてのファイル(*.*)|*.*", answerExtension);
      ansOfd.Title = "開くファイルを選択してください";
      ansOfd.RestoreDirectory = true;

      if (proOfd.ShowDialog() == DialogResult.OK) {
        if (ansOfd.ShowDialog() == DialogResult.OK) {
          // 問題
          using (StreamReader sr = new StreamReader(proOfd.OpenFile())) {
            problem = Problem.fromStream(sr);
          }
          // 解答
          using (StreamReader sr = new StreamReader(ansOfd.OpenFile())) {
            piecesMove = problem.readAnswerStream(sr);
          }
          // リストへの描画
          problem.showpieceList(pieceListpanel);
        }
      }
    }
  }

  // 座標
  class Vertex {
    public double X { get; }
    public double Y { get; }
    // X,Yに値をセットするコンストラクタ
    public Vertex(double x, double y) {
      X = x;
      Y = y;
    }

    // PointFに変換
    public PointF toPointF() {
      return new PointF((float)X, (float)Y);
    }
    // Pointに変換
    public Point toPoint() {
      return new Point((int)X, (int)Y);
    }

    // vertex同士の演算用の演算子オーバーロード
    public static Vertex operator +(Vertex lhs, Vertex rhs) {
      return new Vertex(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }
    public static Vertex operator -(Vertex lhs, Vertex rhs) {
      return new Vertex(lhs.X - rhs.X, lhs.Y - rhs.Y);
    }
    // vertexとdoubleの演算用の演算子オーバーロード
    public static Vertex operator /(Vertex lhs, double rhs) {
      return new Vertex(lhs.X / rhs, lhs.Y / rhs);
    }
    public static Vertex operator *(Vertex lhs, double rhs) {
      return new Vertex(lhs.X * rhs, lhs.Y * rhs);
    }

    // 回転
    public Vertex rotate(double rad) {
      double nx = X * Math.Cos(rad) - Y * Math.Sin(rad);
      double ny = X * Math.Sin(rad) + Y * Math.Cos(rad);
      return new Vertex(nx, ny);
    }
    public Vertex rotate(Vertex origin, double rad) {
      return (this - origin).rotate(rad) + origin;
    }
  }

  // 動かすピース情報
  class PieceMove {
    public Piece piece { get; }
    public double X { get; }   // X方向に動かす分
    public double Y { get; }   // Y方向に動かす分
    public double rad { get; } // 回転させる角度

    public PieceMove(double x, double y, double rad, Piece piece) {
      X = x;
      Y = y;
      this.rad = rad;
      this.piece = piece;
    }

    // 描画
    public void draw(Graphics g, Brush brush, PictureBox canvas) {
      piece.rotate(rad).move(X, Y).draw(g, brush, canvas);
    }

    // 解答の出力
    public void toStream(StreamWriter sw) {
      sw.WriteLine("{0} {1} {2} {3}", piece.ID, X, Y, rad);
    }
  }

  // 図形(フレームの穴とピースに使われる)
  class Polygon {
    // 重心の取得
    public Vertex getGravity() {
      return vertices.Aggregate((acc, x) => acc + x) / vertices.Length / 2.0;
    }

    public Vertex[] vertices { get; }
    public int ID { get; set; }

    // Vertexクラスをセットするコンストラクタ
    public Polygon(Vertex[] vertices) {
      this.vertices = vertices;
    }

    public Polygon(Vertex[] vertices, int ID) {
      this.vertices = vertices;
      this.ID = ID;
    }

    // StreamReaderから読み込む
    public static Polygon fromStream(StreamReader sr) {
      // 要素数を入力
      int N = int.Parse(sr.ReadLine());
      // 要素数分だけ確保
      Vertex[] vertices = new Vertex[N];
      for (int i = 0; i < N; i++) {
        // 座標を入力
        string[] tokens = sr.ReadLine().Split(' ');
        double x = double.Parse(tokens[0]);
        double y = double.Parse(tokens[1]);
        vertices[i] = new Vertex(x, y);
      }
      return new Polygon(vertices);
    }

    const int ID_WIDTH = 20;
    const int ID_HEIGHT = 10;
    // 図形の描画
    public void draw(Graphics g, Brush brush, PictureBox canvas) {
      PointF[] points = this.vertices.Select((x) => (x / 2.0).toPointF()).ToArray();
      g.FillPolygon(brush, points);

      // IDを表示するためのTextbox
      TextBox IDTextbox = new TextBox();
      IDTextbox.Font = new Font("Meiryo UI", 8);
      IDTextbox.Location = getGravity().toPoint();
      IDTextbox.Text = ID.ToString();
      IDTextbox.Size = new Size(ID_WIDTH, ID_HEIGHT);
      // canvasの子コントロールに追加(これで表示される)
      canvas.Controls.Add(IDTextbox);
    }

    // 図形の描画(list用)
    public void draw(Graphics g, Brush brush, PictureBox canvas, Vertex space) {
      PointF[] points = this.vertices.Select((x) => ((x + space) / 2.0).toPointF()).ToArray();
      g.FillPolygon(brush, points);

      // IDを表示するためのTextbox
      TextBox IDTextbox = new TextBox();
      IDTextbox.Font = new Font("Meiryo UI", 8);
      IDTextbox.Location = (getGravity() + (space / 2.0)).toPoint();
      IDTextbox.Text = ID.ToString();
      IDTextbox.Size = new Size(ID_WIDTH, ID_HEIGHT);
      // canvasの子コントロールに追加(これで表示される)
      canvas.Controls.Add(IDTextbox);
    }
     
    public void toStream(StreamWriter sw) {
      // 要素数を出力
      sw.WriteLine(vertices.Length);
      foreach (Vertex vertex in vertices) {
        // 座標を出力
        sw.WriteLine("{0} {1}", vertex.X, vertex.Y);
      }
    }

    // 回転
    public Polygon rotate(double rad, int origin_id = 0) {
      Vertex origin = this.vertices[origin_id];
      Vertex[] rotated = this.vertices.Select((x) => x.rotate(origin, rad)).ToArray();
      // 回転後のPolygonを返す
      return new Polygon(rotated, this.ID);
    }

    // 平行移動
    public Polygon move(double X, double Y) {
      Vertex offset = new Vertex(X, Y);
      Vertex[] moved = this.vertices.Select((x) => x + offset).ToArray();
      // 移動後のPolygonを返す
      return new Polygon(moved, this.ID);
    }

    // 最も左にある座標のX座標の取得
    public double getLeftMost() {
      double res = vertices[0].X;
      foreach (Vertex v in vertices) {
        res = Math.Min(v.X, res);
      }
      return res;
    }

    // 最も右にある座標のX座標の取得
    public double getRightMost() {
      double res = vertices[0].X;
      foreach (Vertex v in vertices) {
        res = Math.Max(v.X, res);
      }
      return res;
    }

    // 最も上にある座標のY座標の取得
    public double getTopMost() {
      double res = vertices[0].Y;
      foreach (Vertex v in vertices) {
        res = Math.Min(v.Y, res);
      }
      return res;
    }

    // 最も下にある座標のY座標の取得
    public double getBottomMost() {
      double res = vertices[0].Y;
      foreach (Vertex v in vertices) {
        res = Math.Max(v.Y, res);
      }
      return res;
    }
  }

  // フレーム
  class Frame {
    public Hole[] holes { get; }
    // 穴情報をセットするコンストラクタ
    public Frame(Hole[] holes) {
      this.holes = holes;
    }

    // StreamReaderから読み込む
    public static Frame fromStream(StreamReader sr) {
      // 要素数の入力
      int N = int.Parse(sr.ReadLine());
      // 要素数分だけ確保
      Hole[] holes = new Hole[N];
      for (int i = 0; i < N; i++) {
        // StreamReaderを渡し,HoleのfromStream内で座標を読み込む
        holes[i] = Hole.fromStream(sr);
      }
      return new Frame(holes);
    }

    // Streamに出力
    public void toStream(StreamWriter sw) {
      // 穴の数を書き込む
      sw.WriteLine(holes.Length);
      foreach (Hole hole in holes) {
        hole.toStream(sw);
      }
    }
  }

  class Problem {
    public Frame frame { get; }
    public Piece[] pieces { get; }

    // フレーム情報とピース情報をセットするコンストラクタ
    private Problem(Frame frame, Piece[] pieces) {
      this.frame = frame;
      this.pieces = pieces;
    }
    // Streamから各情報を読み取る
    public static Problem fromStream(StreamReader sr) {
      Frame frame = Frame.fromStream(sr);
      // 要素数の入力
      int N = int.Parse(sr.ReadLine());
      // 要素数分だけ確保
      Piece[] pieces = new Piece[N];
      for (int i = 0; i < N; i++) {
        pieces[i] = Piece.fromStream(sr);
        pieces[i].ID = i;
      }
      return new Problem(frame, pieces);
    }

    // Streamに出力
    public void toStream(StreamWriter sw) {
      frame.toStream(sw);
      // ピースの数を出力
      sw.WriteLine(pieces.Length);
      foreach (Piece piece in pieces) {
        piece.toStream(sw);
      }
    }

    // 解答を読み込む
    public PieceMove[] readAnswerStream(StreamReader sr) {
      int N = int.Parse(sr.ReadLine());
      PieceMove[] piecesMove = new PieceMove[N];
      for (int i = 0; i < N; i++) {
        string[] tokens = sr.ReadLine().Split(' ');
        int id = int.Parse(tokens[0]);
        double x = double.Parse(tokens[1]);
        double y = double.Parse(tokens[2]);
        double rad = double.Parse(tokens[3]);

        piecesMove[i] = new PieceMove(x, y, rad, pieces[id]);
      }
      return piecesMove;
    }
    
    // ピースリストへの描画
    public void showpieceList(Panel listPanel) {
      // listPanelの子コントロールをクリア
      listPanel.Controls.Clear();
      // ピースとピースの幅
      const int show_Width = 30;
      
      // 一番右にある点のX座標とピース自体の幅を取得
      double canvas_Width = -1.0, canvas_Height = 0.0;
      foreach (Polygon pol in pieces) {
        canvas_Width = Math.Max(canvas_Width, pol.getRightMost());
        canvas_Height += Math.Abs(pol.getTopMost() - pol.getBottomMost());
      }

      // PanelよりPictureBoxが大きくなったらスクロールバーを表示
      listPanel.AutoScroll = true;
      PictureBox canvas = new PictureBox();
      // 余裕をもって大きさを二倍取る
      canvas.Size = new Size((int)canvas_Width, (int)canvas_Height + show_Width * pieces.Length);
      canvas.Image = new Bitmap(canvas.Height, canvas.Width);
      listPanel.Controls.Add(canvas);
      canvas.Location = new Point(0, 0);

      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Random rng = new Random();
        // 原点からどれだけずらすか
        double space = show_Width;
        // 描画
        foreach (Polygon pol in pieces) {
          pol.draw(g, randomBrush(rng), canvas, new Vertex(-pol.getLeftMost() + show_Width, space - pol.getTopMost()));
          space += Math.Abs(pol.getTopMost() - pol.getBottomMost()) + show_Width;
        }
      }
    }

    /* 色をランダムに生成 */
    private Brush randomBrush(Random rng) {
      return new SolidBrush(Color.FromArgb(rng.Next(255), rng.Next(255), rng.Next(255)));
    }
  }
}