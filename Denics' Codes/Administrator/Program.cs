using System;
using System.Windows.Forms;

namespace Denics.Administrator
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
            Application.Run(new LogInPage());
            // Change 'Sample' ^^^^^^^^ to 'your' page name to run your program
            // Example: Application.Run(new AvailabilityPage());    
        }
    }
}