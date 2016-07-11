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
    public WakuAndPiece() {
      InitializeComponent();
      this.state = new FormState();
      // 変更があったら有効にする
      this.state.piecesMoveChanged += (x) => {
        this.drawPiecesMove.Enabled = x != null;
      };
      this.state.problemChanged += (x) => {
        this.drawPieces.Enabled = x != null;
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
        /* 今はテキストから読み込んでるだけ 5/22 */
        // フレーム,ピース情報を読み込む
        using (StreamReader questReader = readQuestreader.StandardOutput) { //new StreamReader("quest.txt")
          problem = Problem.fromStream(questReader);
        }
      }
    }
    
    /* ソルバーにフレーム,ピース情報を出力 */
    private void outputSolve_Click(object sender, EventArgs e) {

      ProcessStartInfo processInfo = new ProcessStartInfo("solver.exe");
      processInfo.UseShellExecute = false;
      processInfo.RedirectStandardInput = true;
      processInfo.RedirectStandardOutput = true;

      using (Process processSolver = Process.Start(processInfo)) {
        /* 今はテキストに書き込んでるだけ */
        // フレーム,ピース情報を書き込む
        using (StreamWriter picInfoWriter = processSolver.StandardInput) { // new StreamWriter("toPic.txt") 
          problem.toStream(picInfoWriter);
          using (StreamReader answerReader = processSolver.StandardOutput) { //new StreamReader("answer.txt")
            piecesMove = problem.readAnswerStream(answerReader);
          }
        }
      }
    }

    /* */
    private void answerfromSolver_Click(object sender, EventArgs e) {
    }

    /* 描画 */
    private void drawPieces_Click(object sender, EventArgs e) {
      canvas.Image = new Bitmap(canvas.Height, canvas.Width);
      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        // アンチエイリアス
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Random rng = new Random();
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
      return (new SolidBrush(Color.FromArgb(rng.Next(255), rng.Next(255), rng.Next(255))));
    }

    /* WakuAndPieceが読まれた際の処理(1度だけ) */
    private void WakuAndPiece_Load(object sender, EventArgs e) {
      this.labelpanel.Controls.Add(canvas);
      this.canvas.Location = new Point(0, 0);
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
      return (new PointF((float)X, (float)Y));
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

    // 回転
    public Vertex rotate(double rad) {
      double nx = X * Math.Cos(rad) - Y * Math.Sin(rad);
      double ny = X * Math.Sin(rad) + Y * Math.Cos(rad);
      return (new Vertex(nx, ny));
    }
    public Vertex rotate(Vertex origin, double rad) {
      return ((this - origin).rotate(rad) + origin);
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
  }

  // 図形(フレームの穴とピースに使われる)
  class Polygon {
    // 重心の取得
    public Vertex getGravity() {
      return vertices.Aggregate((acc, x) => acc + x) / vertices.Length;
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
      return (new Polygon(vertices));
    }

    // 図形の描画
    public void draw(Graphics g, Brush brush, PictureBox canvas) {
      PointF[] points = this.vertices.Select((x) => x.toPointF()).ToArray();
      g.FillPolygon(brush, points);

      // IDを表示するためのTextbox
      TextBox IDTextbox = new TextBox();
      IDTextbox.Location = getGravity().toPoint();
      IDTextbox.Text = ID.ToString();
      IDTextbox.Size = new Size(25, 10);
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
      return (new Polygon(rotated, this.ID));
    }

    // 平行移動
    public Polygon move(double X, double Y) {
      Vertex offset = new Vertex(X, Y);
      Vertex[] moved = this.vertices.Select((x) => x + offset).ToArray();
      // 移動後のPolygonを返す
      return (new Polygon(moved, this.ID));
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
      return (new Frame(holes));
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
      return (new Problem(frame, pieces));
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
      return (piecesMove);
    }
  }
}