using System;
using System.Windows.Forms;
using StudentEnrollmentApp.UI;

namespace StudentEnrollmentApp
{
    static class Program
    {
        /// <summary>
        /// Главна точка на изпълнение.
        /// Автор: Владимир Василев Владимиров - F113578
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
