namespace WakuAndPiece {
  partial class ChangeID {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.canvas = new System.Windows.Forms.PictureBox();
      this.changeIDbox = new System.Windows.Forms.TextBox();
      this.changeButton = new System.Windows.Forms.Button();
      this.emptyList = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
      this.SuspendLayout();
      // 
      // canvas
      // 
      this.canvas.BackColor = System.Drawing.Color.Transparent;
      this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.canvas.Location = new System.Drawing.Point(62, 35);
      this.canvas.Name = "canvas";
      this.canvas.Size = new System.Drawing.Size(499, 283);
      this.canvas.TabIndex = 0;
      this.canvas.TabStop = false;
      // 
      // changeIDbox
      // 
      this.changeIDbox.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.changeIDbox.Location = new System.Drawing.Point(605, 35);
      this.changeIDbox.Name = "changeIDbox";
      this.changeIDbox.Size = new System.Drawing.Size(139, 28);
      this.changeIDbox.TabIndex = 1;
      // 
      // changeButton
      // 
      this.changeButton.Location = new System.Drawing.Point(605, 70);
      this.changeButton.Name = "changeButton";
      this.changeButton.Size = new System.Drawing.Size(67, 28);
      this.changeButton.TabIndex = 2;
      this.changeButton.Text = "変更";
      this.changeButton.UseVisualStyleBackColor = true;
      this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
      // 
      // emptyList
      // 
      this.emptyList.AutoSize = true;
      this.emptyList.Font = new System.Drawing.Font("Meiryo UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.emptyList.Location = new System.Drawing.Point(803, 35);
      this.emptyList.Name = "emptyList";
      this.emptyList.Size = new System.Drawing.Size(93, 20);
      this.emptyList.TabIndex = 3;
      this.emptyList.Text = "空きのあるID\r\n";
      // 
      // ChangeID
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(952, 445);
      this.Controls.Add(this.emptyList);
      this.Controls.Add(this.changeButton);
      this.Controls.Add(this.changeIDbox);
      this.Controls.Add(this.canvas);
      this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.Name = "ChangeID";
      this.Text = "ChangeID";
      ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox canvas;
    private System.Windows.Forms.TextBox changeIDbox;
    private System.Windows.Forms.Button changeButton;
    private System.Windows.Forms.Label emptyList;
  }
}