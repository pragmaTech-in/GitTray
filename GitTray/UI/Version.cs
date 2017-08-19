using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GitTray
{
  struct SwInfoFormat
  {
    internal string AuthorInfo;
    internal string CopyrightInfo;
    internal VersionFormat SwVersionInfo;
  }

  struct VersionFormat
  {
    internal string SwMajorVersion;
    internal string SwMinorVersion;
    internal string SwBuildVersion;
  }

  public sealed partial class Version : Form
  {
    private readonly SwInfoFormat _swInfo;

    public Version()
    {
      InitializeComponent();
      _swInfo = SwInfoFormat();
      Load += Version_Load;
      Text = Text + @" v" + _swInfo.SwVersionInfo.SwMajorVersion + @"." + _swInfo.SwVersionInfo.SwMinorVersion;
      
    }

    public void Version_Load(object sender, EventArgs e)
    {
      StringBuilder details = new StringBuilder();
      details.AppendFormat("Author: {0}", _swInfo.AuthorInfo + Environment.NewLine);
      details.AppendFormat("Version: v{0}.{1} (Build: {2})", _swInfo.SwVersionInfo.SwMajorVersion,
      _swInfo.SwVersionInfo.SwMinorVersion, _swInfo.SwVersionInfo.SwBuildVersion);
      details.Append(Environment.NewLine+_swInfo.CopyrightInfo);

      DisplayInfo.AppendText(details.ToString());
    }

    private static SwInfoFormat SwInfoFormat()
    {
      SwInfoFormat swInfoFormat;
      var version = Assembly.GetExecutingAssembly().GetName().Version;
      var compInf = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

      swInfoFormat.AuthorInfo = @"PragmaTech India Pvt Ltd";
      swInfoFormat.CopyrightInfo = compInf.LegalCopyright;
      swInfoFormat.SwVersionInfo.SwMajorVersion = version.Major.ToString();
      swInfoFormat.SwVersionInfo.SwMinorVersion = version.Minor.ToString();
      swInfoFormat.SwVersionInfo.SwBuildVersion = version.Build.ToString();
      return swInfoFormat;
    }

        private void DisplayInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
