using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CasinoGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var exitCode = -1;
            while (exitCode != 0)
            {
                var form = new FrmSlotMachine();
                Application.Run(form);
                exitCode = form.exitCode;
                Console.WriteLine($"Exit Code: {exitCode}");
            }
        }
    }
}
