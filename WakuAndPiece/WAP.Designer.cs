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
      this.canvas = new System.Windows.Forms.PictureBox();
      this.drawPiecesMove = new System.Windows.Forms.Button();
      this.textboxPanel = new System.Windows.Forms.Panel();
      this.framePanel = new System.Windows.Forms.Panel();
      this.saveAns = new System.Windows.Forms.Button();
      this.oldProAns = new System.Windows.Forms.Button();
      this.pieceListpanel = new System.Windows.Forms.Panel();
      this.listSwitchCombo = new System.Windows.Forms.ComboBox();
      this.problemState = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
      this.textboxPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // drawPieces
      // 
      this.drawPieces.Enabled = false;
      this.drawPieces.Location = new System.Drawing.Point(36, 164);
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
      this.outputSolve.Location = new System.Drawing.Point(36, 108);
      this.outputSolve.Name = "outputSolve";
      this.outputSolve.Size = new System.Drawing.Size(110, 33);
      this.outputSolve.TabIndex = 1;
      this.outputSolve.Text = "Solverに渡す";
      this.outputSolve.UseVisualStyleBackColor = true;
      this.outputSolve.Click += new System.EventHandler(this.outputSolve_Click);
      // 
      // canvas
      // 
      this.canvas.BackColor = System.Drawing.Color.Transparent;
      this.canvas.Location = new System.Drawing.Point(-1, -1);
      this.canvas.Name = "canvas";
      this.canvas.Size = new System.Drawing.Size(1820, 1028);
      this.canvas.TabIndex = 4;
      this.canvas.TabStop = false;
      // 
      // drawPiecesMove
      // 
      this.drawPiecesMove.Enabled = false;
      this.drawPiecesMove.Location = new System.Drawing.Point(36, 220);
      this.drawPiecesMove.Name = "drawPiecesMove";
      this.drawPiecesMove.Size = new System.Drawing.Size(110, 34);
      this.drawPiecesMove.TabIndex = 5;
      this.drawPiecesMove.Text = "Move後描画";
      this.drawPiecesMove.UseVisualStyleBackColor = true;
      this.drawPiecesMove.Click += new System.EventHandler(this.drawPiecesMove_Click);
      // 
      // textboxPanel
      // 
      this.textboxPanel.AutoScroll = true;
      this.textboxPanel.BackColor = System.Drawing.Color.Transparent;
      this.textboxPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textboxPanel.Controls.Add(this.canvas);
      this.textboxPanel.Location = new System.Drawing.Point(188, 52);
      this.textboxPanel.Name = "textboxPanel";
      this.textboxPanel.Size = new System.Drawing.Size(910, 514);
      this.textboxPanel.TabIndex = 6;
      // 
      // framePanel
      // 
      this.framePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.framePanel.Location = new System.Drawing.Point(701, 52);
      this.framePanel.Name = "framePanel";
      this.framePanel.Size = new System.Drawing.Size(380, 300);
      this.framePanel.TabIndex = 5;
      // 
      // saveAns
      // 
      this.saveAns.Enabled = false;
      this.saveAns.Location = new System.Drawing.Point(36, 272);
      this.saveAns.Name = "saveAns";
      this.saveAns.Size = new System.Drawing.Size(110, 33);
      this.saveAns.TabIndex = 7;
      this.saveAns.Text = "保存";
      this.saveAns.UseVisualStyleBackColor = true;
      this.saveAns.Click += new System.EventHandler(this.SaveAns_Click);
      // 
      // oldProAns
      // 
      this.oldProAns.Location = new System.Drawing.Point(36, 321);
      this.oldProAns.Name = "oldProAns";
      this.oldProAns.Size = new System.Drawing.Size(110, 33);
      this.oldProAns.TabIndex = 8;
      this.oldProAns.Text = "過去の問題と解答";
      this.oldProAns.UseVisualStyleBackColor = true;
      this.oldProAns.Click += new System.EventHandler(this.oldProAns_Click);
      // 
      // pieceListpanel
      // 
      this.pieceListpanel.AutoScroll = true;
      this.pieceListpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pieceListpanel.Location = new System.Drawing.Point(1118, 52);
      this.pieceListpanel.Name = "pieceListpanel";
      this.pieceListpanel.Size = new System.Drawing.Size(220, 514);
      this.pieceListpanel.TabIndex = 9;
      // 
      // listSwitchCombo
      // 
      this.listSwitchCombo.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.listSwitchCombo.FormattingEnabled = true;
      this.listSwitchCombo.Items.AddRange(new object[] {
            "ID昇順",
            "ID降順",
            "面積昇順",
            "面積降順",
            "頂点数昇順",
            "頂点数降順"});
      this.listSwitchCombo.Location = new System.Drawing.Point(1118, 23);
      this.listSwitchCombo.Name = "listSwitchCombo";
      this.listSwitchCombo.Size = new System.Drawing.Size(121, 25);
      this.listSwitchCombo.TabIndex = 11;
      this.listSwitchCombo.SelectedIndexChanged += new System.EventHandler(this.listSwitchCombo_SelectedIndexChanged);
      // 
      // problemState
      // 
      this.problemState.AutoSize = true;
      this.problemState.BackColor = System.Drawing.Color.Transparent;
      this.problemState.Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.problemState.Location = new System.Drawing.Point(185, 14);
      this.problemState.Name = "problemState";
      this.problemState.Size = new System.Drawing.Size(0, 17);
      this.problemState.TabIndex = 12;
      // 
      // WakuAndPiece
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1192, 654);
      this.Controls.Add(this.problemState);
      this.Controls.Add(this.framePanel);
      this.Controls.Add(this.listSwitchCombo);
      this.Controls.Add(this.pieceListpanel);
      this.Controls.Add(this.oldProAns);
      this.Controls.Add(this.saveAns);
      this.Controls.Add(this.textboxPanel);
      this.Controls.Add(this.drawPiecesMove);
      this.Controls.Add(this.drawPieces);
      this.Controls.Add(this.outputSolve);
      this.Controls.Add(this.readFramePiece);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "WakuAndPiece";
      this.Text = "WAP";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.WakuAndPiece_Load);
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
      this.textboxPanel.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button readFramePiece;
    private System.Windows.Forms.Button outputSolve;
    private System.Windows.Forms.PictureBox canvas;
    private System.Windows.Forms.Button drawPiecesMove;
    private System.Windows.Forms.Button drawPieces;
    private System.Windows.Forms.Panel textboxPanel;
    private System.Windows.Forms.Button saveAns;
    private System.Windows.Forms.Button oldProAns;
    private System.Windows.Forms.Panel pieceListpanel;
    private System.Windows.Forms.Panel framePanel;
    private System.Windows.Forms.ComboBox listSwitchCombo;
    private System.Windows.Forms.Label problemState;
  }
}

