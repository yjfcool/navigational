using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Navigational
{
    static class StartupSelector
    {
        [System.STAThreadAttribute()]
        //[System.Diagnostics.DebuggerNonUserCodeAttribute()]
        static void Main()
        {
            bool isWPFEnabled = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["IsWPFEnabled"].ToString());

            if (isWPFEnabled == false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new WindowsForms.Form1());
            }
            else
            {
                System.Windows.Application application = new WPF.App();
                application.Run(new WPF.MainWindow());
            }
        }

    }
}
