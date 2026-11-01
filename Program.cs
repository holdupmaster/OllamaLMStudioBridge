using System;
using System.Windows.Forms;

namespace OllamaLmStudioBridge
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Configure modern Windows Forms High-DPI mode (PerMonitorV2)
            ApplicationConfiguration.Initialize();

            // Setup global error handling
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show($"Fatal application error: {ex.Message}\n\n{ex.StackTrace}", 
                        "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Application.Run(new MainForm());
        }
    }
}
