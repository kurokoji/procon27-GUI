using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }

    Problem problem;

    /* フレーム,ピース情報の読み込み */
    private void readFramePiece_Click(object sender, EventArgs e) {
      /*
      Process readQuestprocess = new Process();
      readQuestprocess.StartInfo.FileName = "";
      readQuestprocess.StartInfo.UseShellExecute = false;
      readQuestprocess.StartInfo.RedirectStandardOutput = true;
      // 読み込みプロセスの開始
      readQuestprocess.Start();
      */

      /* 今はテキストから読み込んでるだけ 5/22 */

      // フレーム,ピース情報を読み込む
      using (StreamReader questReader = new StreamReader("quest.txt")) { //readFrameprocess.StandardOutput;
        problem = Problem.fromStream(questReader);
      }
    }

    /* ソルバーにフレーム,ピース情報を出力 */
    private void outputSolve_Click(object sender, EventArgs e) {
      /*
      Process writeprocess = new Process();
      writeprocess.StartInfo.FileName = "";
      writeprocess.StartInfo.UseShellExecute = false;
      writeprocess.StartInfo.RedirectStandardInput = true;
      writeprocess.Start();
      */

      /* 今はテキストに書き込んでるだけ */

      // フレーム,ピース情報を書き込む
      using (StreamWriter picInfoWriter = new StreamWriter("toPic.txt")) { // writeprocess.StandardOutput
        problem.toStream(picInfoWriter);
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
  }

  // 図形(フレームの穴とピースに使われる)
  class Polygon {
    public Vertex[] vertices { get; }
    // Vertexクラスをセットするコンストラクタ
    public Polygon(Vertex[] vertices) {
      this.vertices = vertices;
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
    public void toStream(StreamWriter sw) {
      // 要素数を出力
      sw.WriteLine(vertices.Length);
      foreach (Vertex vertex in vertices) {
        // 座標を出力
        sw.WriteLine("{0} {1}", vertex.X, vertex.Y);
      }
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
  }
}