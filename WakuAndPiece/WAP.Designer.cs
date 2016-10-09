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
      this.beamWidth = new System.Windows.Forms.NumericUpDown();
      this.readPieces = new System.Windows.Forms.Button();
      this.readFrame = new System.Windows.Forms.Button();
      this.frameparam2 = new System.Windows.Forms.NumericUpDown();
      this.frameparam1 = new System.Windows.Forms.NumericUpDown();
      this.mergePiecesSolve = new System.Windows.Forms.Button();
      this.showmergePieces = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
      this.textboxPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.beamWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.frameparam2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.frameparam1)).BeginInit();
      this.SuspendLayout();
      // 
      // drawPieces
      // 
      this.drawPieces.Enabled = false;
      this.drawPieces.Location = new System.Drawing.Point(36, 295);
      this.drawPieces.Name = "drawPieces";
      this.drawPieces.Size = new System.Drawing.Size(110, 33);
      this.drawPieces.TabIndex = 3;
      this.drawPieces.Text = "描画";
      this.drawPieces.UseVisualStyleBackColor = true;
      this.drawPieces.Click += new System.EventHandler(this.drawPieces_Click);
      // 
      // readFramePiece
      // 
      this.readFramePiece.Enabled = false;
      this.readFramePiece.Location = new System.Drawing.Point(36, 15);
      this.readFramePiece.Name = "readFramePiece";
      this.readFramePiece.Size = new System.Drawing.Size(110, 33);
      this.readFramePiece.TabIndex = 0;
      this.readFramePiece.Text = "一括読み込み";
      this.readFramePiece.UseVisualStyleBackColor = true;
      this.readFramePiece.Click += new System.EventHandler(this.readFramePiece_Click);
      // 
      // outputSolve
      // 
      this.outputSolve.Enabled = false;
      this.outputSolve.Location = new System.Drawing.Point(36, 248);
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
      this.canvas.Size = new System.Drawing.Size(4000, 4000);
      this.canvas.TabIndex = 4;
      this.canvas.TabStop = false;
      // 
      // drawPiecesMove
      // 
      this.drawPiecesMove.Enabled = false;
      this.drawPiecesMove.Location = new System.Drawing.Point(36, 342);
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
      this.saveAns.Location = new System.Drawing.Point(36, 437);
      this.saveAns.Name = "saveAns";
      this.saveAns.Size = new System.Drawing.Size(110, 33);
      this.saveAns.TabIndex = 7;
      this.saveAns.Text = "保存";
      this.saveAns.UseVisualStyleBackColor = true;
      this.saveAns.Click += new System.EventHandler(this.SaveAns_Click);
      // 
      // oldProAns
      // 
      this.oldProAns.Location = new System.Drawing.Point(36, 484);
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
      // beamWidth
      // 
      this.beamWidth.Location = new System.Drawing.Point(36, 547);
      this.beamWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.beamWidth.Name = "beamWidth";
      this.beamWidth.Size = new System.Drawing.Size(110, 23);
      this.beamWidth.TabIndex = 13;
      this.beamWidth.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
      // 
      // readPieces
      // 
      this.readPieces.Location = new System.Drawing.Point(36, 61);
      this.readPieces.Name = "readPieces";
      this.readPieces.Size = new System.Drawing.Size(110, 33);
      this.readPieces.TabIndex = 14;
      this.readPieces.Text = "ピース読み込み";
      this.readPieces.UseVisualStyleBackColor = true;
      this.readPieces.Click += new System.EventHandler(this.readPieces_Click);
      // 
      // readFrame
      // 
      this.readFrame.Enabled = false;
      this.readFrame.Location = new System.Drawing.Point(36, 108);
      this.readFrame.Name = "readFrame";
      this.readFrame.Size = new System.Drawing.Size(110, 33);
      this.readFrame.TabIndex = 15;
      this.readFrame.Text = "枠読み込み";
      this.readFrame.UseVisualStyleBackColor = true;
      this.readFrame.Click += new System.EventHandler(this.readFrame_Click);
      // 
      // frameparam2
      // 
      this.frameparam2.Location = new System.Drawing.Point(99, 207);
      this.frameparam2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.frameparam2.Name = "frameparam2";
      this.frameparam2.Size = new System.Drawing.Size(47, 23);
      this.frameparam2.TabIndex = 16;
      this.frameparam2.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
      // 
      // frameparam1
      // 
      this.frameparam1.DecimalPlaces = 1;
      this.frameparam1.Location = new System.Drawing.Point(36, 207);
      this.frameparam1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.frameparam1.Name = "frameparam1";
      this.frameparam1.Size = new System.Drawing.Size(47, 23);
      this.frameparam1.TabIndex = 17;
      this.frameparam1.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
      // 
      // mergePiecesSolve
      // 
      this.mergePiecesSolve.Enabled = false;
      this.mergePiecesSolve.Location = new System.Drawing.Point(36, 155);
      this.mergePiecesSolve.Name = "mergePiecesSolve";
      this.mergePiecesSolve.Size = new System.Drawing.Size(110, 38);
      this.mergePiecesSolve.TabIndex = 18;
      this.mergePiecesSolve.Text = "合成ピース\r\n読み込み";
      this.mergePiecesSolve.UseVisualStyleBackColor = true;
      this.mergePiecesSolve.Click += new System.EventHandler(this.mergePiecesSolve_Click);
      // 
      // showmergePieces
      // 
      this.showmergePieces.Enabled = false;
      this.showmergePieces.Location = new System.Drawing.Point(36, 390);
      this.showmergePieces.Name = "showmergePieces";
      this.showmergePieces.Size = new System.Drawing.Size(110, 33);
      this.showmergePieces.TabIndex = 19;
      this.showmergePieces.Text = "合成ピース描画";
      this.showmergePieces.UseVisualStyleBackColor = true;
      this.showmergePieces.Click += new System.EventHandler(this.showmergePieces_Click);
      // 
      // WakuAndPiece
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1351, 741);
      this.Controls.Add(this.showmergePieces);
      this.Controls.Add(this.mergePiecesSolve);
      this.Controls.Add(this.frameparam1);
      this.Controls.Add(this.frameparam2);
      this.Controls.Add(this.readFrame);
      this.Controls.Add(this.readPieces);
      this.Controls.Add(this.beamWidth);
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
      ((System.ComponentModel.ISupportInitialize)(this.beamWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.frameparam2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.frameparam1)).EndInit();
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
    private System.Windows.Forms.NumericUpDown beamWidth;
    private System.Windows.Forms.Button readPieces;
    private System.Windows.Forms.Button readFrame;
    private System.Windows.Forms.NumericUpDown frameparam2;
    private System.Windows.Forms.NumericUpDown frameparam1;
    private System.Windows.Forms.Button mergePiecesSolve;
    private System.Windows.Forms.Button showmergePieces;
  }
}

