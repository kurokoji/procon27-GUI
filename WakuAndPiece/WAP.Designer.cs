namespace WakuAndPiece {
  partial class WakuAndPiece {
    /// <summary>
    /// 必要なデザイナー変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナーで生成されたコード

    /// <summary>
    /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディターで変更しないでください。
    /// </summary>
    private void InitializeComponent() {
      this.drawPieces = new System.Windows.Forms.Button();
      this.readFramePiece = new System.Windows.Forms.Button();
      this.outputSolve = new System.Windows.Forms.Button();
      this.answerfromSolver = new System.Windows.Forms.Button();
      this.canvas = new System.Windows.Forms.PictureBox();
      this.drawPiecesMove = new System.Windows.Forms.Button();
      this.labelpanel = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
      this.SuspendLayout();
      // 
      // drawPieces
      // 
      this.drawPieces.Enabled = false;
      this.drawPieces.Location = new System.Drawing.Point(36, 211);
      this.drawPieces.Name = "drawPieces";
      this.drawPieces.Size = new System.Drawing.Size(110, 33);
      this.drawPieces.TabIndex = 3;
      this.drawPieces.Text = "描画";
      this.drawPieces.UseVisualStyleBackColor = true;
      this.drawPieces.Click += new System.EventHandler(this.drawPieces_Click);
      // 
      // readFramePiece
      // 
      this.readFramePiece.Location = new System.Drawing.Point(36, 52);
      this.readFramePiece.Name = "readFramePiece";
      this.readFramePiece.Size = new System.Drawing.Size(110, 33);
      this.readFramePiece.TabIndex = 0;
      this.readFramePiece.Text = "読み込み";
      this.readFramePiece.UseVisualStyleBackColor = true;
      this.readFramePiece.Click += new System.EventHandler(this.readFramePiece_Click);
      // 
      // outputSolve
      // 
      this.outputSolve.Enabled = false;
      this.outputSolve.Location = new System.Drawing.Point(36, 104);
      this.outputSolve.Name = "outputSolve";
      this.outputSolve.Size = new System.Drawing.Size(110, 33);
      this.outputSolve.TabIndex = 1;
      this.outputSolve.Text = "Solverに渡す";
      this.outputSolve.UseVisualStyleBackColor = true;
      this.outputSolve.Click += new System.EventHandler(this.outputSolve_Click);
      // 
      // answerfromSolver
      // 
      this.answerfromSolver.Enabled = false;
      this.answerfromSolver.Location = new System.Drawing.Point(36, 156);
      this.answerfromSolver.Name = "answerfromSolver";
      this.answerfromSolver.Size = new System.Drawing.Size(110, 33);
      this.answerfromSolver.TabIndex = 2;
      this.answerfromSolver.Text = "answer";
      this.answerfromSolver.UseVisualStyleBackColor = true;
      this.answerfromSolver.Click += new System.EventHandler(this.answerfromSolver_Click);
      // 
      // canvas
      // 
      this.canvas.BackColor = System.Drawing.Color.Transparent;
      this.canvas.Location = new System.Drawing.Point(252, 52);
      this.canvas.Name = "canvas";
      this.canvas.Size = new System.Drawing.Size(910, 514);
      this.canvas.TabIndex = 4;
      this.canvas.TabStop = false;
      // 
      // drawPiecesMove
      // 
      this.drawPiecesMove.Enabled = false;
      this.drawPiecesMove.Location = new System.Drawing.Point(36, 261);
      this.drawPiecesMove.Name = "drawPiecesMove";
      this.drawPiecesMove.Size = new System.Drawing.Size(110, 34);
      this.drawPiecesMove.TabIndex = 5;
      this.drawPiecesMove.Text = "Move後描画";
      this.drawPiecesMove.UseVisualStyleBackColor = true;
      this.drawPiecesMove.Click += new System.EventHandler(this.drawPiecesMove_Click);
      // 
      // labelpanel
      // 
      this.labelpanel.BackColor = System.Drawing.Color.Transparent;
      this.labelpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.labelpanel.Location = new System.Drawing.Point(252, 52);
      this.labelpanel.Name = "labelpanel";
      this.labelpanel.Size = new System.Drawing.Size(910, 514);
      this.labelpanel.TabIndex = 6;
      // 
      // WakuAndPiece
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1249, 648);
      this.Controls.Add(this.labelpanel);
      this.Controls.Add(this.drawPiecesMove);
      this.Controls.Add(this.canvas);
      this.Controls.Add(this.drawPieces);
      this.Controls.Add(this.answerfromSolver);
      this.Controls.Add(this.outputSolve);
      this.Controls.Add(this.readFramePiece);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "WakuAndPiece";
      this.Text = "WAP";
      this.Load += new System.EventHandler(this.WakuAndPiece_Load);
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button readFramePiece;
    private System.Windows.Forms.Button outputSolve;
    private System.Windows.Forms.Button answerfromSolver;
    private System.Windows.Forms.PictureBox canvas;
    private System.Windows.Forms.Button drawPiecesMove;
    private System.Windows.Forms.Button drawPieces;
    private System.Windows.Forms.Panel labelpanel;
  }
}

