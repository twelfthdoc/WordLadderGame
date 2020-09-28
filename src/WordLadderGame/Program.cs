using System.Linq;

namespace WordLadderGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                // Attempt these calls...
                Startup.Initialize(args.Any() ? args[0] : null);
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