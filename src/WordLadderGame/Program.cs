using WordLadderGame;

namespace BluePrism_Technical_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Attempt these calls...
                Startup.Initialize(args[0]);
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