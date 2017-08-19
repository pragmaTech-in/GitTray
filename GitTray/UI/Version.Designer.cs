using System;

namespace GitTray
{
  sealed partial class Version
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Version));
            this.DisplayInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DisplayInfo
            // 
            this.DisplayInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DisplayInfo.BackColor = System.Drawing.Color.White;
            this.DisplayInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DisplayInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DisplayInfo.Font = new System.Drawing.Font("Palatino Linotype", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayInfo.ForeColor = System.Drawing.Color.LightSlateGray;
            this.DisplayInfo.Location = new System.Drawing.Point(0, 0);
            this.DisplayInfo.Margin = new System.Windows.Forms.Padding(4);
            this.DisplayInfo.Multiline = true;
            this.DisplayInfo.Name = "DisplayInfo";
            this.DisplayInfo.ReadOnly = true;
            this.DisplayInfo.Size = new System.Drawing.Size(440, 103);
            this.DisplayInfo.TabIndex = 1;
            this.DisplayInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DisplayInfo.TextChanged += new System.EventHandler(this.DisplayInfo_TextChanged);
            // 
            // Version
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 103);
            this.Controls.Add(this.DisplayInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Version";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Git Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox DisplayInfo;
  }
}