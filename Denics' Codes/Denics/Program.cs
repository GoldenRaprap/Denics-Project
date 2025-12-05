using System;
using System.Windows.Forms;
using Denics.FrontPage;

namespace Denics
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (sender, e) =>
            {
                MessageBox.Show("Unhandled UI Exception: " + e.Exception.Message, "Error");
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Exception ex = e.ExceptionObject as Exception;
                MessageBox.Show("Unhandled Non-UI Exception: " + (ex?.Message ?? "Unknown error"), "Error");
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FHomepage());
            // Example: Application.Run(new AvailabilityPage());    
        }
    }
}