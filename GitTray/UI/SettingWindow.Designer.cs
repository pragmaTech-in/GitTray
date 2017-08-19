using System;
using System.Windows.Forms;

namespace GitTray
{
  partial class SettingWindow
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingWindow));
      this._okButton = new System.Windows.Forms.Button();
      this._closeButton = new System.Windows.Forms.Button();
      this._controlTab = new System.Windows.Forms.TabControl();
      this._controlTabGeneral = new System.Windows.Forms.TabPage();
      this.panel2 = new System.Windows.Forms.Panel();
      this.LogFormatLabel = new System.Windows.Forms.Label();
      this.LogMode = new System.Windows.Forms.ComboBox();
      this.GitExeBrowserButton = new System.Windows.Forms.Button();
      this.GitLocation = new System.Windows.Forms.TextBox();
      this.SystemGitLabel = new System.Windows.Forms.Label();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.FetchType = new System.Windows.Forms.ComboBox();
      this.FetchLabel = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.Cycle = new System.Windows.Forms.NumericUpDown();
      this.PollInfoLabel = new System.Windows.Forms.Label();
      this._controlTabRepository = new System.Windows.Forms.TabPage();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this._remove = new System.Windows.Forms.Button();
      this._add = new System.Windows.Forms.Button();
      this.RepoLabel = new System.Windows.Forms.Label();
      this._controlTab.SuspendLayout();
      this._controlTabGeneral.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.Cycle)).BeginInit();
      this._controlTabRepository.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // _okButton
      // 
      resources.ApplyResources(this._okButton, "_okButton");
      this._okButton.Name = "_okButton";
      this._okButton.UseVisualStyleBackColor = true;
      this._okButton.Click += new System.EventHandler(this.OkButton_Click);
      // 
      // _closeButton
      // 
      resources.ApplyResources(this._closeButton, "_closeButton");
      this._closeButton.Name = "_closeButton";
      this._closeButton.UseVisualStyleBackColor = true;
      this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // _controlTab
      // 
      this._controlTab.Controls.Add(this._controlTabGeneral);
      this._controlTab.Controls.Add(this._controlTabRepository);
      resources.ApplyResources(this._controlTab, "_controlTab");
      this._controlTab.Name = "_controlTab";
      this._controlTab.SelectedIndex = 0;
      // 
      // _controlTabGeneral
      // 
      this._controlTabGeneral.BackColor = System.Drawing.Color.White;
      this._controlTabGeneral.Controls.Add(this.panel2);
      resources.ApplyResources(this._controlTabGeneral, "_controlTabGeneral");
      this._controlTabGeneral.Name = "_controlTabGeneral";
      // 
      // panel2
      // 
      this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel2.Controls.Add(this.LogFormatLabel);
      this.panel2.Controls.Add(this.LogMode);
      this.panel2.Controls.Add(this.GitExeBrowserButton);
      this.panel2.Controls.Add(this.GitLocation);
      this.panel2.Controls.Add(this.SystemGitLabel);
      this.panel2.Controls.Add(this.checkBox3);
      this.panel2.Controls.Add(this.checkBox2);
      this.panel2.Controls.Add(this.FetchType);
      this.panel2.Controls.Add(this.FetchLabel);
      this.panel2.Controls.Add(this.label2);
      this.panel2.Controls.Add(this.Cycle);
      this.panel2.Controls.Add(this.PollInfoLabel);
      resources.ApplyResources(this.panel2, "panel2");
      this.panel2.Name = "panel2";
      // 
      // LogFormatLabel
      // 
      resources.ApplyResources(this.LogFormatLabel, "LogFormatLabel");
      this.LogFormatLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.LogFormatLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.LogFormatLabel.Name = "LogFormatLabel";
      // 
      // LogMode
      // 
      this.LogMode.FormattingEnabled = true;
      this.LogMode.Items.AddRange(new object[] {
            resources.GetString("LogMode.Items"),
            resources.GetString("LogMode.Items1")});
      resources.ApplyResources(this.LogMode, "LogMode");
      this.LogMode.Name = "LogMode";
      this.LogMode.SelectedIndexChanged += new System.EventHandler(this.LogFormat_SelectedIndexChanged);
      // 
      // GitExeBrowserButton
      // 
      resources.ApplyResources(this.GitExeBrowserButton, "GitExeBrowserButton");
      this.GitExeBrowserButton.Name = "GitExeBrowserButton";
      this.GitExeBrowserButton.UseVisualStyleBackColor = true;
      this.GitExeBrowserButton.Click += new System.EventHandler(this.GitExeBrowserButton_Click);
      // 
      // GitLocation
      // 
      resources.ApplyResources(this.GitLocation, "GitLocation");
      this.GitLocation.Name = "GitLocation";
      this.GitLocation.TextChanged += new System.EventHandler(this.GitLocation_TextChanged);
      // 
      // SystemGitLabel
      // 
      resources.ApplyResources(this.SystemGitLabel, "SystemGitLabel");
      this.SystemGitLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.SystemGitLabel.Name = "SystemGitLabel";
      // 
      // checkBox3
      // 
      resources.ApplyResources(this.checkBox3, "checkBox3");
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.UseVisualStyleBackColor = true;
      // 
      // checkBox2
      // 
      resources.ApplyResources(this.checkBox2, "checkBox2");
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.UseVisualStyleBackColor = true;
      // 
      // FetchType
      // 
      resources.ApplyResources(this.FetchType, "FetchType");
      this.FetchType.FormattingEnabled = true;
      this.FetchType.Items.AddRange(new object[] {
            resources.GetString("FetchType.Items"),
            resources.GetString("FetchType.Items1")});
      this.FetchType.Name = "FetchType";
      this.FetchType.SelectedIndexChanged += new System.EventHandler(this.FetchType_SelectedIndexChanged);
      // 
      // FetchLabel
      // 
      resources.ApplyResources(this.FetchLabel, "FetchLabel");
      this.FetchLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.FetchLabel.Name = "FetchLabel";
      // 
      // label2
      // 
      resources.ApplyResources(this.label2, "label2");
      this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.label2.Name = "label2";
      // 
      // Cycle
      // 
      resources.ApplyResources(this.Cycle, "Cycle");
      this.Cycle.Name = "Cycle";
      this.Cycle.ValueChanged += new System.EventHandler(this.Cycle_ValueChanged);
      // 
      // PollInfoLabel
      // 
      resources.ApplyResources(this.PollInfoLabel, "PollInfoLabel");
      this.PollInfoLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.PollInfoLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.PollInfoLabel.Name = "PollInfoLabel";
      // 
      // _controlTabRepository
      // 
      this._controlTabRepository.BackColor = System.Drawing.Color.White;
      this._controlTabRepository.Controls.Add(this.dataGridView1);
      this._controlTabRepository.Controls.Add(this._remove);
      this._controlTabRepository.Controls.Add(this._add);
      this._controlTabRepository.Controls.Add(this.RepoLabel);
      resources.ApplyResources(this._controlTabRepository, "_controlTabRepository");
      this._controlTabRepository.Name = "_controlTabRepository";
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
      this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridView1.DataError += DataGridView1OnDataError;
      resources.ApplyResources(this.dataGridView1, "dataGridView1");
      this.dataGridView1.Name = "dataGridView1";
      // 
      // _remove
      // 
      resources.ApplyResources(this._remove, "_remove");
      this._remove.Name = "_remove";
      this._remove.UseVisualStyleBackColor = true;
      this._remove.Click += new System.EventHandler(this.RemoveButton_Click);
      // 
      // _add
      // 
      resources.ApplyResources(this._add, "_add");
      this._add.Name = "_add";
      this._add.UseVisualStyleBackColor = true;
      this._add.Click += new System.EventHandler(this.AddButton_Click);
      // 
      // RepoLabel
      // 
      resources.ApplyResources(this.RepoLabel, "RepoLabel");
      this.RepoLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
      this.RepoLabel.Name = "RepoLabel";
      // 
      // SettingWindow
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.WhiteSmoke;
      this.Controls.Add(this._controlTab);
      this.Controls.Add(this._closeButton);
      this.Controls.Add(this._okButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SettingWindow";
      this.Load += new System.EventHandler(this.SettingWindow_Load);
      this._controlTab.ResumeLayout(false);
      this._controlTabGeneral.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.Cycle)).EndInit();
      this._controlTabRepository.ResumeLayout(false);
      this._controlTabRepository.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Button _okButton;
    private Button _closeButton;
    private TabControl _controlTab;
    private TabPage _controlTabGeneral;
    private TabPage _controlTabRepository;
    private Label RepoLabel;
    private Button _remove;
    private Button _add;
    private Panel panel2;
    private DataGridView dataGridView1;
    private Label label2;
    private Label PollInfoLabel;
    private Label FetchLabel;
    private CheckBox checkBox2;
    private CheckBox checkBox3;
    private Label SystemGitLabel;
    private Button GitExeBrowserButton;
    private Label LogFormatLabel;
    internal NumericUpDown Cycle;
    internal ComboBox FetchType;
    internal ComboBox LogMode;
    internal TextBox GitLocation;
  }
}