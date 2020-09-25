using WordLadderGame;

namespace BluePrism_Technical_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Startup.Initialize(args[0]);           
            Startup.Run();
            Startup.Close();
        }
    }
}