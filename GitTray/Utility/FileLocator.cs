using System;
using System.IO;
using System.Windows.Forms;


namespace GitTray.Utility
{
  public static class FileLocator
  {
    private const string Gitexe = @"git.exe";
    private const string GitRepo = @".git";

    public static string 
      FindInSystemPath()
    {
      if (File.Exists(Gitexe))
      {
        return Path.GetFullPath(Gitexe);
      }

      var values = Environment.GetEnvironmentVariable("PATH");

      if (values == null) return null;
      foreach (var path in values.Split(';'))
      {
        var gitPath = Path.Combine(path, Gitexe);

        if (File.Exists(gitPath))
        {
          return gitPath;  
        }
      }

      return null;
    }

    public static string 
      FindInSpecifiedPath(string filePath)
    {
      if (File.Exists(filePath))
      {
        return Path.GetFullPath(filePath);
      }

      try
      {
        var listfiles = Directory.GetFiles(filePath, Gitexe, SearchOption.AllDirectories);
        foreach (var file in listfiles)
        {
          if (File.Exists(file))
          {
            return file;
          }
        }
      }
      catch (UnauthorizedAccessException)
      {
        MessageBox.Show(@"Unable to access path provided. Please check the path and try again",
          @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      return null;
    }

    public static bool
      IsValidGitRepo(string dirPath)
    {
      try
      {
        return Directory.Exists(Path.Combine(dirPath, GitRepo));
      }
      catch (UnauthorizedAccessException)
      {
        MessageBox.Show(@"Unable to access path provided. Please check the path and try again",
          @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      catch (DirectoryNotFoundException)
      {
        //Ignore error here. It will be caught as not git repository.
      }
      return false;
    }
  }
}
