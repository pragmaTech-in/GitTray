using System;
using System.Data;
using System.Timers;
using System.Windows.Forms;
using GitTray.Properties;
using GitTray.Utility;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace GitTray
{
  struct DisplayText
  {
    public string Branch;
    public string Log;
    public string Name;
  }

  public partial class GitTrayUi : Form
  {
    internal readonly SettingWindow UserSettings = new SettingWindow();

    internal static System.Timers.Timer TrayTimer = new System.Timers.Timer();

    private static bool _isProcessRunning;

    public static bool IsErrorPresent;

    private void 
      GitTrayUi_Load(object sender, EventArgs e)
    {
      StatusTableView();
      UserSettings.LoadSavedData();
      UserSettings.Closed += UserSettingsOnClosed;
    }

    private void 
      UserSettingsOnClosed(object sender, EventArgs eventArgs)
    {
      DataGridView1OnSelectionChanged(this, null);
    }

    private void 
      StartProcess()
    {
      _isProcessRunning = true;

      if (String.IsNullOrEmpty(UserSettings.GitLocation.Text))
      {
        MessageBox.Show(@"Git Location cannot be empty, please specify the git.exe location in Settings",
        @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
        IsErrorPresent = true;
        return;
      }

      // Instantiate timer
      TrayTimer.Elapsed += GitTrayNotification;
      TrayTimer.Start();
    }

    private void
      StopProces()
    {
      TrayTimer.Stop();
    }

    private void 
      Show_TrayClick(object sender, EventArgs e)
    {
      Show();
    }

    private void 
      StatusTableView()
    {
      dataGridView1.DataSource = DataBase.GitTrayTableDataPool;
      dataGridView1.SelectionChanged += DataGridView1OnSelectionChanged;

      dataGridView1.Columns[0].Width = 50;
      dataGridView1.Columns[0].FillWeight = 500;

      dataGridView1.Columns[1].Width = 50;
      dataGridView1.Columns[1].FillWeight = 100;

      dataGridView1.Columns[2].Width = 50;
      dataGridView1.Columns[2].FillWeight = 150;
    }

    private void
      DataGridView1OnSelectionChanged(object sender, EventArgs eventArgs)
    {
      if (DataBase.GitTrayTableDataPool.Rows.Count != 0)
      {
        if (_isProcessRunning || IsErrorPresent) return;

        StartProcess();
        StartStopButton.Enabled = true;
      }
      else
      {
        if (_isProcessRunning) StopProces();
        StartStopButton.Enabled = false;
      }
    }

    private void 
      ShowSettings_Click(object sender, EventArgs e)
    {
      UserSettings.ShowDialog(this);
    }

    private void 
      AboutMenu_Click(object sender, EventArgs e)
    {
      var oFrm = new Version();
      oFrm.ShowDialog(this);
      oFrm.Dispose();
    }

    private void 
      GitTrayNotification(object source, ElapsedEventArgs e)
    {
      TrayTimer.Stop();

      var fetchResult = GitExtensions.ProcessGitRepo();

      if (!string.IsNullOrEmpty(fetchResult))
      {
        if (fetchResult.Contains("fatal:"))
        {
          GitTray.ShowBalloonTip(1000, "Fatal Error", fetchResult, ToolTipIcon.Error);  
        }
        else
        {
          GitTray.ShowBalloonTip(1000, "New Software Available!", fetchResult, ToolTipIcon.Info);  
        }
      }

      TrayTimer.Start();
    }

    private void
      ExitMenu_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void
      SaveOnClose()
    {
      Settings.Default.GitLocation = UserSettings.GitLocation.Text;
      Settings.Default.FetchType = UserSettings.FetchType.Text;
      Settings.Default.LogFormat = UserSettings.LogMode.Text;
      Settings.Default.PollCycle = UserSettings.Cycle.Value;

      var serSettingsTableDataPool = JsonConvert.SerializeObject(DataBase.SettingsTableDataPool);
      Settings.Default.SettingsTableDataPool = serSettingsTableDataPool;
      Settings.Default.Save();
    }

    private void
      StartStopButton_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewRow gridrow in dataGridView1.SelectedRows)
      {
        foreach (DataRow datarow in DataBase.GitTrayTableDataPool.Rows)
        {
          if (datarow["Projects"] != gridrow.Cells[0].Value) continue;

          if (String.IsNullOrEmpty(datarow["Activity"].ToString()))
          {
            datarow["Activity"] = "Started";
          }
          else
            switch ((string)datarow["Activity"])
            {
              case "Sleeping":
                datarow["Activity"] = "Stopped";
                break;
              case "Stopped":
                datarow["Activity"] = "Started";
                break;
            }
        }
      }
    }

    private static void
      AddToRegistry()
    {
      RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

      if (rk != null) rk.SetValue("Git Tray", Application.ExecutablePath);
    }

    public
      GitTrayUi()
    {
      InitializeComponent();
      GitExtensions.UpdateGitConfiguration(this);
      DataBase.InitializeTables();
      AddToRegistry();
    }

    protected override void
      OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);

      switch (e.CloseReason)
      {
        case CloseReason.WindowsShutDown:
          return;
        case CloseReason.UserClosing:
          e.Cancel = true;
          Hide();
          break;
        case CloseReason.None:
          break;
        case CloseReason.MdiFormClosing:
          break;
        case CloseReason.TaskManagerClosing:
          SaveOnClose();
          return;
        case CloseReason.FormOwnerClosing:
          break;
        case CloseReason.ApplicationExitCall:
          SaveOnClose();
          return;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
