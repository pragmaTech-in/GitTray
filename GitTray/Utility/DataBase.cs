using System;
using System.Data;
using System.Windows.Forms;
using GitTray.Properties;
using Newtonsoft.Json;

namespace GitTray.Utility
{
  /// <summary>
  /// Create a DataBase to store user settings at runtime
  /// </summary>
  public class DataBase
  {

    private static DataTable _cachedSettingsTable = new DataTable();

    private static DataTable _cachedGitTrayTable = new DataTable();

    public static DataTable GitTrayTableDataPool = new DataTable();

    public static DataTable SettingsTableDataPool = new DataTable();
    
    private static void 
      UpdateTableCache()
    {
      _cachedSettingsTable = SettingsTableDataPool.Copy();
      _cachedGitTrayTable = GitTrayTableDataPool.Copy();
    }

    private static void
      AddOrUpdateItemsInTrayTable()
    {
      for (int index = 0; index < SettingsTableDataPool.Rows.Count;)
      {
        var cell = SettingsTableDataPool.Rows[index][0];

        if (cell == null || GitTrayTableDataPool.Rows.Contains(cell))
        {
          //Skip item
          index++;
          continue;
        }

        if (FileLocator.IsValidGitRepo(cell.ToString()))
        {
           GitTrayTableDataPool.Rows.Add(cell.ToString());
           index++;
        }
        else
        {
          MessageBox.Show(@"Not a git repository, please check the path and try again.",
            @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);

          //clear the row
          SettingsTableDataPool.Rows[index].Delete();
          SettingsTableDataPool.AcceptChanges();
        }
      }
    }

    private static void
      DeleteItemsFromTrayTable(int gitTrayTableCount, int settingsTableCount)
    {
      for (int index = 0; index < gitTrayTableCount;)
      {
        if (settingsTableCount == 0)
        {
          GitTrayTableDataPool.Clear();
          break;
        }
        try
        {
          var cell = GitTrayTableDataPool.Rows[index][0];

          if (cell == null || SettingsTableDataPool.Rows.Contains(cell))
          {
            index++;
            continue;
          }
        }
        catch (IndexOutOfRangeException)
        {
          break;
        }

        GitTrayTableDataPool.Rows[index].Delete();
        GitTrayTableDataPool.AcceptChanges();
      }
    }

    public static void 
      DiscardChanges()
    {
      SettingsTableDataPool = _cachedSettingsTable.Copy();
      GitTrayTableDataPool  = _cachedGitTrayTable.Copy();
    }

    public static void
      InitializeTables()
    {

      //Configure required Table Headers
      GitTrayTableDataPool.Columns.AddRange(new[]
       {
         new DataColumn("Projects", typeof (string)),
         new DataColumn("Status", typeof (string)),
         new DataColumn("Activity", typeof(string))
       });

      SettingsTableDataPool.Columns.AddRange(new[] { new DataColumn("Projects", typeof(string)), });

      GitTrayTableDataPool.PrimaryKey = new[] { GitTrayTableDataPool.Columns["Projects"] };
      SettingsTableDataPool.PrimaryKey = new[] { SettingsTableDataPool.Columns["Projects"] };

      //Load saved table
      if (!Settings.Default.SettingsTableDataPool.Contains("Projects")) return;
      SettingsTableDataPool = JsonConvert.DeserializeObject<DataTable>(Settings.Default.SettingsTableDataPool);
      CloneSettingDataPool();
    }

    public static void
      CloneSettingDataPool()
    {
      int gitTrayTableCount = GitTrayTableDataPool.Rows.Count;
      int settingsTableCount = SettingsTableDataPool.Rows.Count;

      if (gitTrayTableCount > settingsTableCount)
        DeleteItemsFromTrayTable(gitTrayTableCount, settingsTableCount);
      else
        AddOrUpdateItemsInTrayTable();

      UpdateTableCache();
    }

  }
}
