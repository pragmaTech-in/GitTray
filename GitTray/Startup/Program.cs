using System;
using System.Windows.Forms;

namespace GitTray
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        Application.Run(new GitTrayUi());
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, @"Git Repository Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
