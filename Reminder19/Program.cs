using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Reminder19.src
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Reminder19());
            }
            catch (Exception e)
            {
                MessageBox.Show("Fatal Error Occured! "+e.Message+" Reminder 19 will be closed!");
            }
        }
    }
}