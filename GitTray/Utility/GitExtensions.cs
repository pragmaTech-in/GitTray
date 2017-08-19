using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using GitTray.Utility;

namespace GitTray
{
  internal struct GitStreamReader
  {
    public StreamReader StandardError;
    public StreamReader StandardOutput;
  }

  public static class GitExtensions
  {
    private const string DryFetch    = @"fetch origin --dry-run";
    private const string AutoFetch   = @"fetch origin";
    private const string LocalRevParse   = @"rev-parse @";
    private const string RemoteRevParse  = @"rev-parse @{u}";
    private const string BaseRevParse    = @"merge-base @ @{u}";
    private const string Log = @"log --pretty=format:[%s][%an][%ad] --decorate --date=relative -1 ";

    private static string _logCmd;
    private static string _fetchCmd;
    private static string _logFormat;
    private static Process _gitProcess;

    public static ProcessStartInfo
      GitConfig { get; private set; }

    private static ProcessStartInfo
      CreateGitProcess(GitTrayUi gitTrayUi, out Process gitProcess)
    {
      ProcessStartInfo gitInfo = new ProcessStartInfo
        {
          CreateNoWindow = true,
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          FileName = gitTrayUi.UserSettings.GitLocation.Text,
          UseShellExecute = false
        };

      gitProcess = new Process();

      return gitInfo;
    }

    private static void
      CreateMessage(StringBuilder message, DisplayText displayText)
    {
      if (_logFormat == @"Detail Log (Max 256 char)")
      {
        message.AppendLine("\n" + displayText.Branch.Trim());
        message.AppendLine(displayText.Log.Trim() + " (" + displayText.Name.Trim() + ")");
      }
      else
      {
        message.AppendLine("\n" + displayText.Branch.Trim());
      }
    }

    private static void
      CreateMessage(StringBuilder message, string errorLog)
    {
      message.AppendLine("\n" + errorLog);
    }

    private static void
      GetCommitLog(List<string> fetchResult, StringBuilder message)
    {
      foreach (var br in fetchResult)
      {
        if (!br.Contains("fatal:"))
        {
          //Get commit message of remote
          var getLog = GetCommitLog(br);

          DisplayText displayText;
          displayText.Branch = br.Trim();
          displayText.Log = getLog[0];
          displayText.Name = getLog[1];

          CreateMessage(message, displayText);
        }
        else
        {
          CreateMessage(message, br);
        }
      }
    }

    private static List<string>
      GetCommitLog(string remotebranch)
    {
      List<string> result = new List<string>();
      char[] delimiter = { '[', ']', '\r', '\n' };
      string[] space = { "\b" };

      GitConfig.Arguments = _logCmd + remotebranch.Split(space, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

      _gitProcess.StartInfo = GitConfig;
      _gitProcess.Start();

      StreamReader stdOut = _gitProcess.StandardOutput;

      _gitProcess.WaitForExit();
      _gitProcess.Close();

      while (!stdOut.EndOfStream)
      {
        var readLine = stdOut.ReadLine();
        if (readLine == null) continue;
        var splitLine = readLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        result.AddRange(splitLine);
      }

      return result;
    }

    private static void
      ProcessGitRepo(List<string> fetchResult, DataRow row)
    {
      char[] delimiter = { '>', '\r', '\n' };
      bool isError = false;

      GitConfig.Arguments = _fetchCmd;
      GitStreamReader stdFetchData = NewProcess();

      while (!stdFetchData.StandardError.EndOfStream)
      {
        var readLine = stdFetchData.StandardError.ReadLine();

        if (readLine != null && readLine.Contains("->"))
        {
          var splitLine = readLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
          fetchResult.AddRange(splitLine.Where(o => o.Contains("origin")));
        }
        else if (readLine != null && readLine.Contains("fatal:"))
        {
          var splitLine = readLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
          fetchResult.AddRange(splitLine);
          isError = true;
        }
      }

      if (isError == false)
      {
        string localHead = null;
        string remoteHead = null;
        string baseHead = null;

        GitConfig.Arguments = LocalRevParse;
        var stdData = NewProcess();
        while (!stdData.StandardOutput.EndOfStream) { localHead = stdData.StandardOutput.ReadLine(); }

        GitConfig.Arguments = BaseRevParse;
        stdData = NewProcess();
        while (!stdData.StandardOutput.EndOfStream) { baseHead = stdData.StandardOutput.ReadLine(); }

        GitConfig.Arguments = RemoteRevParse;
        stdData = NewProcess();
        while (!stdData.StandardOutput.EndOfStream) { remoteHead = stdData.StandardOutput.ReadLine(); }

        SetStatus(row, localHead, remoteHead, baseHead);
      }
      else
      {
        row["Status"] = "Error";
      }
    }

    private static void
      SetStatus(DataRow row, string localHead, string remoteHead, string baseHead)
    {
      if (localHead.Equals(remoteHead))
        row["Status"] = "Up-to-date";

      else if (localHead.Equals(baseHead))
        row["Status"] = "Pull-Requested";

      else if (remoteHead.Equals(baseHead))
        row["Status"] = "Ready-to-Push";

      else
        row["Status"] = "Diverged";
    }

    private static GitStreamReader
      NewProcess()
    {
      GitStreamReader stdData;
      _gitProcess.StartInfo = GitConfig;
      _gitProcess.Start();
      stdData.StandardError = _gitProcess.StandardError;
      stdData.StandardOutput = _gitProcess.StandardOutput;
      _gitProcess.WaitForExit();
      _gitProcess.Close();
      return stdData;
    }

    public static void
      UpdateGitConfiguration(GitTrayUi gitTrayUi)
    {
      _logCmd = Log;

      _logFormat = gitTrayUi.UserSettings.LogMode.Text;

      _fetchCmd = gitTrayUi.UserSettings.FetchType.Text == @"Manual" ? DryFetch : AutoFetch;

      GitTrayUi.TrayTimer.Interval = Convert.ToInt32(gitTrayUi.UserSettings.Cycle.Value) * 10000;

      GitConfig = CreateGitProcess(gitTrayUi, out _gitProcess);
    }

    public static void
      UpdateGitConfiguration(SettingWindow settingWindow)
    {
      _logCmd = Log;

      if (settingWindow == null) return;

      _logFormat = settingWindow.LogMode.Text;

      _fetchCmd = settingWindow.FetchType.Text == @"Manual" ? DryFetch : AutoFetch;

      GitTrayUi.TrayTimer.Interval = Convert.ToInt32(settingWindow.Cycle.Value) * 10000;

      GitConfig.FileName = settingWindow.GitLocation.Text;
    }

    public static string
      ProcessGitRepo()
    {
      DataRowCollection dataRowCollection = DataBase.GitTrayTableDataPool.Rows;
      StringBuilder message = new StringBuilder();

      foreach (DataRow row in dataRowCollection)
      {
        var fetchResult = new List<string>();
        var gitDir = row["Projects"];
        
        GitConfig.WorkingDirectory = gitDir.ToString();

        if (row["Activity"].ToString() == "Stopped") continue;

        row["Activity"] = "Running";

        ProcessGitRepo(fetchResult, row);

        GetCommitLog(fetchResult, message);

        row["Activity"] = "Sleeping";
      }

      return message.ToString();
    }
  }
}
