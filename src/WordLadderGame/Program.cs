namespace WordLadderGame
{
    public class Program
    {
        /// <summary>
        /// The program's main entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                // Attempt these calls...
                Startup.Initialize(args);
                Startup.Run();
            }
            finally
            {
                // ...but always run this.
                Startup.Close();
            }
        }
    }
}