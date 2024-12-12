using System;
using System.Windows.Forms;

namespace memory_game
{
    internal static class Program
    {
        public const string AppTitle = "Memory Game";
        public const string AppAutor = "Andrej Vitez";
        public const string AppAutorUrl = "http://www.vitez-studios.com";
        public const string AppRegistryRoot = "Memory game";
        public const string StrRegFailed = "Registry support has been disabled.";
        public const bool Debug = false; 

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
