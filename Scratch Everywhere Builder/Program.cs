namespace Scratch_Everywhere_Builder
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        // also with args
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
            ApplicationConfiguration.Initialize();
            if (args.Length > 0)
            {
                Application.Run(new Main(args[0]));
            }
            
            else
                Application.Run(new Main("none"));
        }
    }
}