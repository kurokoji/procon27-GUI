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
using System.Text.RegularExpressions;

namespace WakuAndPiece {
  using Piece = Polygon;

  public partial class ChangeID : Form {
    public ChangeID() {
      InitializeComponent();
    }

    private Problem problem;
    public Panel listpanel;

    public ChangeID(Problem problem, Panel panel) {
      InitializeComponent();
      this.problem = problem;
      this.listpanel = panel;
      showPiece();
      showEmptyID();
    }

    private Brush randomBrush(Random rng) {
      return new SolidBrush(Color.FromArgb(rng.Next(255), rng.Next(255), rng.Next(255)));
    }

    // 空きのIDを表示
    private void showEmptyID() {
      emptyList.Text = "空きのあるID" + "\n\n";
      for (int i = 0; i < problem.pieces.Length; ++i) {
        if (problem.pieces[i] == null) {
          emptyList.Text += i.ToString() + "\n";
        }
      }
    }

    // ピースを表示
    private void showPiece() {
      canvas.Image = new Bitmap(canvas.Height, canvas.Width);
      using (Graphics g = Graphics.FromImage(canvas.Image)) {
        // アンチエイリアス
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Random rng = new Random();
        // 子コントロールをclear
        canvas.Controls.Clear();
        problem.misspieces[0].draw(g, randomBrush(rng), canvas);
      }
    }

    private void changeButton_Click(object sender, EventArgs e) {
      warningMsg(changeIDbox.Text);
      // 空にする
      changeIDbox.Text = "";
    }

    private void warningMsg(string s) {
      // 正規表現で入力された数が0以上の整数であるか判定
      if (Regex.IsMatch(s, @"^[+]?[0-9]+$")) {
        int N = int.Parse(s);
        // 入力されたIDが存在するか(配列外参照を防ぐ)
        if (N < problem.pieces.Length) {
          // 入力されたIDが空であれば変更を許可する
          if (problem.pieces[N] == null) {
            DialogResult res = MessageBox.Show("IDを" + s + "へ変更します", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes) {
              problem.pieces[N] = new Piece(problem.misspieces[0].vertices, N);
              // 修正すべきピースが無くなったら閉じる
              if (problem.misspieces.Count > 1) {
                // 先頭を削除
                problem.misspieces.RemoveAt(0);
                // ピースを表示
                showPiece();
                // 空きのIDを表示
                showEmptyID();
              } else {
                problem.showpieceList(listpanel);
                this.Close();
              }
            }
          } else {
            MessageBox.Show(s + "は空ではありません", "エラー");
          }
        } else {
          MessageBox.Show(s + "はIDとして存在しません", "エラー");
        }
      } else {
        MessageBox.Show("0以上の整数を入力してください", "エラー");
      }
    }
  }
}
