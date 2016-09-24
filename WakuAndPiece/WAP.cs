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
      this.state.problemChanged += (x) => {
        listSwitchCombo.SelectedIndex = 0;
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
      readQuestInfo.RedirectStandardError = false;

      // 読み込みプロセスの開始
      using (Process readQuestreader = Process.Start(readQuestInfo)) {
        string text = readQuestreader.StandardOutput.ReadToEnd();
        readQuestreader.WaitForExit();
        // フレーム,ピース情報を読み込む
        problem = Problem.fromStream(new StringReader(text));
      }
      /*
      using (TextReader tr = new StreamReader("sample_.txt")) {
        problem = Problem.fromStream(tr);
      }
      */

      // ID被りがあったピースが1個以上あればID変更フォームを表示
      if (problem.missingPieces.Count > 0) {
        ChangeID changeid = new ChangeID(problem, pieceListpanel);
        changeid.Show();
      } else {
        // リストへの描画
        problem.showpieceList(pieceListpanel);
      }
      problem.framedraw(framePanel);
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
      canvas.Image = new Bitmap(canvas.Width, canvas.Height);
      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        // アンチエイリアス
        g.SmoothingMode = SmoothingMode.AntiAlias;
        // 子コントロールをclear
        canvas.Controls.Clear();
        foreach (Polygon pol in problem.pieces) {
          pol.draw(g, pol.brush, canvas);
        }      
      }
    }

    /* 動かした後の描画 */
    private void drawPiecesMove_Click(object sender, EventArgs e) {
      canvas.Image = new Bitmap(canvas.Width, canvas.Height);
      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        // アンチエイリアス
        g.SmoothingMode = SmoothingMode.AntiAlias;
        // 子コントロールをclear
        canvas.Controls.Clear();
        foreach (PieceMove pmv in piecesMove) {
          pmv.draw(g, pmv.piece.brush, canvas);
        }
      }
    }

    /* WakuAndPieceが読まれた際の処理(1度だけ) */
    private void WakuAndPiece_Load(object sender, EventArgs e) {
      this.textboxPanel.Controls.Add(canvas);
      this.canvas.Location = new Point(0, 0);
      listSwitchCombo.DropDownStyle = ComboBoxStyle.DropDownList;
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
        // 問題
        using (StreamReader sr = new StreamReader(proOfd.OpenFile())) {
          problem = Problem.fromStream(sr);
        }
        // リストへの描画
        problem.showpieceList(pieceListpanel);
        problem.framedraw(framePanel);
        if (ansOfd.ShowDialog() == DialogResult.OK) {
          // 解答
          using (StreamReader sr = new StreamReader(ansOfd.OpenFile())) {
            piecesMove = problem.readAnswerStream(sr);
          }
        }
      }
    }

    private void listSwitchCombo_SelectedIndexChanged(object sender, EventArgs e) {
      problem.showpieceList(pieceListpanel, listSwitchCombo.SelectedIndex);
    }
  }

  // 座標
  public class Vertex {
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
  public class PieceMove {
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
  public class Polygon {
    // 重心の取得
    // http://www.biwako.shiga-u.ac.jp/sensei/mnaka/ut/polygonarea.html
    public Vertex getGravity() {
      double sumS = 0;
      double sumX = 0, sumY = 0;
      int N = vertices.Length;

      for (int i = 0; i < N; ++i) {
        int j = (i + 1) % N;
        Vertex g = vertices[j] - vertices[i];
        double st = g.X * g.Y / 2;
        Vertex ss = new Vertex(g.X * vertices[i].Y, g.Y * vertices[i].X);
        Vertex gt = new Vertex(vertices[j].X - g.X / 3, vertices[j].Y - g.Y / 3);
        Vertex gs = new Vertex(vertices[j].X - g.X / 2, vertices[j].Y - g.Y / 2);
        sumX += st * gt.X + ss.X * gs.X;
        sumY += st * gt.Y + ss.Y * gs.Y;
        sumS += st + ss.X;
      }
      return new Vertex(sumX, -sumY) / Math.Abs(sumS);
    }

    // 面積の取得
    private double getArea() {
      double sumS = 0;
      int N = vertices.Length;

      for (int i = 0; i < N; ++i) {
        double s = (vertices[i % N].X * vertices[(i + 1) % N].Y - vertices[i % N].Y * vertices[(i + 1) % N].X) / 2;
        sumS += s;
      }
      return sumS;
    }

    public Vertex[] vertices { get; }
    public int ID { get; set; }
    public double area { get; }
    public Brush brush { get; }

    // Vertexクラスをセットするコンストラクタ
    public Polygon(Vertex[] vertices) {
      this.vertices = vertices;
      area = getArea();
    }

    public Polygon(Vertex[] vertices, int ID, Brush brush) {
      this.vertices = vertices;
      this.ID = ID;
      this.brush = brush;
      area = getArea();
    }

    // StreamReaderから読み込む
    public static Polygon fromStream(TextReader sr) {
      // 要素数を入力
      int N = int.Parse(sr.ReadLine());
      // 要素数分だけ確保
      Vertex[] vertices = new Vertex[N];
      for (int i = 0; i < N; i++) {
        // 座標を入力
        string[] tokens = sr.ReadLine().Split(' ');
        double x = double.Parse(tokens[0]);
        double y = double.Parse(tokens[1]);
        Console.Error.WriteLine("{0} {1}", x, y);
        vertices[i] = new Vertex(x, y);
      }
      return new Polygon(vertices);
    }

    //const int ID_WIDTH = 70;
    //const int ID_HEIGHT = 20;
    Vertex LABEL_GAP { get; } = new Vertex(10, 10);
    // 表示倍率
    const double MAG = 0.5;
    // 図形の描画
    public void draw(Graphics g, Brush brush, PictureBox canvas) {
      PointF[] points = this.vertices.Select((x) => (x * MAG).toPointF()).ToArray();
      g.FillPolygon(brush, points);
      // 頂点の描画
      foreach (PointF p in points) {
        g.DrawEllipse(new Pen(Color.Red, 2), p.X, p.Y, 2, 2);
      }

      // IDを表示するためのTextbox
      Label IDLabel = new Label();
      IDLabel.BackColor = Color.Transparent;
      IDLabel.Font = new Font("Meiryo UI", 8);
      IDLabel.Location = ((getGravity() - LABEL_GAP) * MAG).toPoint();
      IDLabel.Text = ID.ToString();
      //IDLabel.Size = new Size(ID_WIDTH, ID_HEIGHT);
      IDLabel.AutoSize = true;
      // canvasの子コントロールに追加(これで表示される)
      canvas.Controls.Add(IDLabel);
    }

    // 図形の描画(list用)
    public void draw(Graphics g, Brush brush, PictureBox canvas, Vertex displace) {
      PointF[] points = this.vertices.Select((x) => ((x + displace) * MAG).toPointF()).ToArray();
      g.FillPolygon(brush, points);

      // IDを表示するためのLabel
      Label IDLabel = new Label();
      IDLabel.BackColor = Color.Transparent;
      IDLabel.Font = new Font("Meiryo UI", 8);
      IDLabel.Location = ((getGravity() - LABEL_GAP) * MAG + (displace * MAG)).toPoint();
      IDLabel.Text = ID.ToString() + "(" + vertices.Length.ToString() + ")";
      //IDLabel.Size = new Size(ID_WIDTH, ID_HEIGHT);
      IDLabel.AutoSize = true;
      // canvasの子コントロールに追加(これで表示される)
      canvas.Controls.Add(IDLabel);
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
      return new Polygon(rotated, this.ID, this.brush);
    }

    // 平行移動
    public Polygon move(double X, double Y) {
      Vertex offset = new Vertex(X, Y);
      Vertex[] moved = this.vertices.Select((x) => x + offset).ToArray();
      // 移動後のPolygonを返す
      return new Polygon(moved, this.ID, this.brush);
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
  public class Frame {
    public Hole[] holes { get; }
    // 穴情報をセットするコンストラクタ
    public Frame(Hole[] holes) {
      this.holes = holes;
    }

    // StreamReaderから読み込む
    public static Frame fromStream(TextReader sr) {
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

  public class Problem {
    public Frame frame { get; }
    public Piece[] pieces { get; }
    public List<Piece> missingPieces;

    // フレーム情報とピース情報をセットするコンストラクタ
    private Problem(Frame frame, Piece[] pieces, List<Piece> missingPieces) {
      this.frame = frame;
      this.pieces = pieces;
      this.missingPieces = missingPieces;
    }
    // Streamから各情報を読み取る
    public static Problem fromStream(TextReader sr) {
      Frame frame = Frame.fromStream(sr);
      // 要素数の入力
      int N = int.Parse(sr.ReadLine());
      // 要素数分だけ確保(最終的に渡す用)
      Piece[] respieces = new Piece[N];
      // IDが被ったときに一時的に保存するリスト
      List<Piece> missingPieces = new List<Piece>();
      // 被っていてもとりあえずここに保存する
      List<Piece>[] pieces = new List<Piece>[N];
      for (int i = 0; i < N; i++) {
        pieces[i] = new List<Piece>();
      }

      for (int i = 0; i < N; i++) {
        int inputID = int.Parse(sr.ReadLine());
        pieces[inputID].Add(Piece.fromStream(sr));
      }
      Random rng = new Random();
      // 被りがあればmissingPiecesにAdd
      for (int i = 0; i < N; i++) {
        if (pieces[i].Count == 1) {
          respieces[i] = new Piece(pieces[i][0].vertices, i, randomBrush(rng));
        } else {
          foreach (Piece piece in pieces[i]) {
            missingPieces.Add(new Piece(piece.vertices, i, randomBrush(rng)));
          }
        }
      }

      return new Problem(frame, respieces, missingPieces);
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
    public void showpieceList(Panel listPanel, int swi = 0) {
      // listPanelの子コントロールをクリア
      listPanel.Controls.Clear();
      // ピースとピースの幅
      const int SHOW_WIDTH = 30;

      // 一番右にある点のX座標とピース自体の幅を取得
      double canvas_width = pieces.Max(x => x.getRightMost());
      double canvas_height = pieces.Select(x => Math.Abs(x.getTopMost() - x.getBottomMost())).Sum();
      PictureBox canvas = new PictureBox();
      canvas.Size = new Size((int)canvas_width / 2, ((int)canvas_height + SHOW_WIDTH * (pieces.Length + 1)) / 2);
      canvas.Image = new Bitmap(canvas.Width, canvas.Height);
      listPanel.Controls.Add(canvas);
      canvas.Location = new Point(0, 0);

      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Random rng = new Random();
        // 原点からどれだけずらすか
        double displace = SHOW_WIDTH;
        // ピースの表示順を指定
        List<Piece> res = new List<Piece>(pieces);
        if (swi == 1) {
          res.Reverse();
        } else if (swi == 2) {
          res.Sort((a, b) => b.area.CompareTo(a.area));
        } else if (swi == 3) {
          res.Sort((a, b) => a.area.CompareTo(b.area));
        } else if (swi == 4) {
          res.Sort((a, b) => a.vertices.Length.CompareTo(b.vertices.Length));
        } else if (swi == 5) {
          res.Sort((a, b) => b.vertices.Length.CompareTo(a.vertices.Length));
        }

        // 描画
        foreach (Polygon pol in res) {
          pol.draw(g, pol.brush, canvas, new Vertex(-pol.getLeftMost() + SHOW_WIDTH, displace - pol.getTopMost()));
          displace += Math.Abs(pol.getTopMost() - pol.getBottomMost()) + SHOW_WIDTH;
        }
      }
      canvas.Refresh();
    }

    // フレーム描画
    public void framedraw(Panel panel) {
      panel.Controls.Clear();
      panel.AutoScroll = true;
      PictureBox canvas = new PictureBox();
      canvas.Size = panel.Size;
      panel.Controls.Add(canvas);
      canvas.Image = new Bitmap(canvas.Width, canvas.Height);
      canvas.Location = new Point(0, 0);

      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Random rng = new Random();
        foreach (Polygon pol in frame.holes) {
          PointF[] points = pol.vertices.Select((x) => (x * 0.5).toPointF()).ToArray();
          // 直線で描画
          g.DrawPolygon(new Pen(Color.DarkBlue, 3), points);
        }
      }
    }

    /* 色をランダムに生成 */
    private static Brush randomBrush(Random rng) {
      return new SolidBrush(Color.FromArgb(rng.Next(90, 255), rng.Next(90, 255), rng.Next(90, 255)));
    }
  }
}