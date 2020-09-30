namespace WordLadderGame
{
    public class Program
    {
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