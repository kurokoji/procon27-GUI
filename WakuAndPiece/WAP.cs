﻿using System;
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
    PieceMove[] piecesMove;

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

    /* 答えをSolverから受け取る */
    private void answerfromSolver_Click(object sender, EventArgs e) {
      /*
      Process readprocess = new Process();
      readprocess.StartInfo.FileName = "";
      readprocess.StartInfo.UseShellExecute = false;
      readprocess.StartInfo.RedirectStandardOutput = true;
      readprocess.Start();
      */

      using (StreamReader answerReader = new StreamReader("answer.txt")) {
        piecesMove = problem.readAnswerStream(answerReader);
      }
    }

    /* 描画 */
    private void drawPieces_Click(object sender, EventArgs e) {
      using (Graphics g = canvas.CreateGraphics()) {
        Random rng = new Random();
        foreach (Polygon pol in problem.pieces) {
          pol.draw(g, randomBrush(rng)); 
        }
      }
    }

    /* 動かした後の描画 */
    private void drawPiecesMove_Click(object sender, EventArgs e) {
      using (Graphics g = canvas.CreateGraphics()) {
        Random rng = new Random();
        foreach (PieceMove pmv in piecesMove) {
          pmv.draw(g, randomBrush(rng));
        }
      }
    }

    /* 色をランダムに生成 */
    private Brush randomBrush(Random rng) {
      return (new SolidBrush(Color.FromArgb(rng.Next(255), rng.Next(255), rng.Next(255))));
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

    // vertex同士の演算用の演算子オーバーロード
    public static Vertex operator +(Vertex lhs, Vertex rhs) {
      return new Vertex(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }
    public static Vertex operator -(Vertex lhs, Vertex rhs) {
      return new Vertex(lhs.X - rhs.X, lhs.Y - rhs.Y);
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
    public void draw(Graphics g, Brush brush) {
      piece.rotate(rad).move(X, Y).draw(g, brush);
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

    // 図形の描画
    public void draw(Graphics g, Brush brush) {
      PointF[] points = this.vertices.Select((x) => x.toPointF()).ToArray();
      g.FillPolygon(brush, points);
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
      return (new Polygon(rotated));
    }

    // 平行移動
    public Polygon move(double X, double Y) {
      Vertex offset = new Vertex(X, Y);
      Vertex[] moved = this.vertices.Select((x) => x + offset).ToArray();
      // 移動後のPolygonを返す
      return (new Polygon(moved));
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