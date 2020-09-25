namespace WordLadderGame
{
    public static class Startup
    {
        private const string DEFAULT_LOCATION = "./FileNameHere.txt";

        public static void Initialize(string targetLocation)
        {
            targetLocation ??= DEFAULT_LOCATION;
        }

        public static void Run()
        {

        }

        public static void Close()
        {

        }
    }
}