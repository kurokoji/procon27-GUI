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
      this.readFramePiece = new System.Windows.Forms.Button();
      this.outputSolve = new System.Windows.Forms.Button();
      this.SuspendLayout();
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
      this.outputSolve.Location = new System.Drawing.Point(36, 104);
      this.outputSolve.Name = "outputSolve";
      this.outputSolve.Size = new System.Drawing.Size(110, 33);
      this.outputSolve.TabIndex = 1;
      this.outputSolve.Text = "Solverに渡す";
      this.outputSolve.UseVisualStyleBackColor = true;
      this.outputSolve.Click += new System.EventHandler(this.outputSolve_Click);
      // 
      // WakuAndPiece
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1188, 596);
      this.Controls.Add(this.outputSolve);
      this.Controls.Add(this.readFramePiece);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "WakuAndPiece";
      this.Text = "WAP";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button readFramePiece;
    private System.Windows.Forms.Button outputSolve;
  }
}

