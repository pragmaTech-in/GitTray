using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using GitTray.Properties;
using GitTray.Utility;

namespace GitTray
{
  public partial class SettingWindow : Form
  {
    private static bool _isSettingChanged;

    private void 
      OnFirstLoadDefaultValues()
    {
      FetchType.SelectedIndex = 1;
      LogMode.SelectedIndex = 0;
      Cycle.Value = 1;
      GitLocation.Text = AutoDetectGitLocation();
    }

    private void
      SettingWindow_Load(object sender, EventArgs e)
    {
      SettingsDataGridView();
    }

    private void
      DataGridView1OnUserAddedRow(object sender, DataGridViewRowEventArgs dataGridViewRowEventArgs)
    {
      _remove.Enabled = dataGridView1.Rows.Count > 1;
    }

    private void
      SettingsDataGridView()
    {
      dataGridView1.DataSource = DataBase.SettingsTableDataPool;

      dataGridView1.UserAddedRow += DataGridView1OnUserAddedRow;
      dataGridView1.RowsAdded += DataGridView1OnRowsAdded;

      DataBase.SettingsTableDataPool.RowDeleted += SettingsTableDataPoolOnRowDeleted;
    }

    private void
      DataGridView1OnRowsAdded(object sender, DataGridViewRowsAddedEventArgs dataGridViewRowsAddedEventArgs)
    {
      _remove.Enabled = dataGridView1.Rows.Count > 1;
    }

    private void
      SettingsTableDataPoolOnRowDeleted(object sender, DataRowChangeEventArgs dataRowChangeEventArgs)
    {
      _remove.Enabled = dataGridView1.Rows.Count > 1;
    }

    private void 
      CloseButton_Click(object sender, EventArgs e)
    {
      DataBase.DiscardChanges();
      _isSettingChanged = false;

      Close();
    }

    private void 
      AddButton_Click(object sender, EventArgs e)
    {
      var oDir = new FolderBrowserDialog { Description = @"Choose a Git Repository" };
      if (oDir.ShowDialog() == DialogResult.OK)
      {
        try
        {
          DataBase.SettingsTableDataPool.Rows.Add(oDir.SelectedPath);
        }
        catch (ConstraintException)
        {
          MessageBox.Show(@"This repository is already availalbe in the Watch-List", @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void 
      RemoveButton_Click(object sender, EventArgs e)
    {
      if (dataGridView1.Rows.Count <= 1) return;

      foreach (DataGridViewRow item in dataGridView1.SelectedRows)
      {
        dataGridView1.Rows.RemoveAt(item.Index);
      }
      DataBase.CloneSettingDataPool();
    }

    private void 
      OkButton_Click(object sender, EventArgs e)
    {
      SyncSettings();

      Close();
    }

    private void 
      Cycle_ValueChanged(object sender, EventArgs e)
    {
      _isSettingChanged = true;
    }

    private void 
      GitExeBrowserButton_Click(object sender, EventArgs e)
    {
      var oDir = new FolderBrowserDialog { Description = @"Specify location of git.exe" };
      if (oDir.ShowDialog() == DialogResult.OK)
      {
        UpdateOnValidPath(oDir.SelectedPath);
      }
    }

    private bool UpdateOnValidPath(string path)
    {
      var exeLoc = FileLocator.FindInSpecifiedPath(path);

      if (exeLoc != null)
      {
        GitLocation.Text = exeLoc;
        GitTrayUi.IsErrorPresent = false;
        return true;
      }
      
      MessageBox.Show(@"Git not found in the specified path, please check the path and try again.",
        @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
      GitTrayUi.IsErrorPresent = true;
      
      return false;
    }

    private void 
      LogFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
      _isSettingChanged = true;
    }

    private void 
      FetchType_SelectedIndexChanged(object sender, EventArgs e)
    {
      _isSettingChanged = true;
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void 
      checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      _isSettingChanged = true;
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void 
      checkBox3_CheckedChanged(object sender, EventArgs e)
    {
      _isSettingChanged = true;
    }

    private void 
      GitLocation_TextChanged(object sender, EventArgs e)
    {
      if (!String.IsNullOrEmpty(GitLocation.Text) && UpdateOnValidPath(GitLocation.Text))
        _isSettingChanged = true;
    }

    private static string 
      AutoDetectGitLocation()
    {
      var exeLoc = FileLocator.FindInSystemPath();
      if (exeLoc != null)
      {
        return exeLoc;
      }
      MessageBox.Show(@"Git not found in system PATH , please specify git location in Settings",
        @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
      GitTrayUi.IsErrorPresent = true;
      return null;
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private void
      DataGridView1OnDataError(object sender, DataGridViewDataErrorEventArgs dataGridViewDataErrorEventArgs)
    {
      //Dummy
    }

    public
      SettingWindow()
    {
      InitializeComponent();
      OnFirstLoadDefaultValues();
    }

    public void
      LoadSavedData()
    {
      if (Settings.Default == null) return;

      Cycle.Value = Settings.Default.PollCycle;
      FetchType.Text = Settings.Default.FetchType;
      LogMode.Text = Settings.Default.LogFormat;

      if (!string.IsNullOrEmpty(Settings.Default.GitLocation))
        GitLocation.Text = Settings.Default.GitLocation;

      _isSettingChanged = true;
      SyncSettings();
    }

    public void
      SyncSettings()
    {
      DataBase.CloneSettingDataPool();

      if (_isSettingChanged)
      {
        GitExtensions.UpdateGitConfiguration(this);
        _isSettingChanged = false;
      }
    }
  }
}
