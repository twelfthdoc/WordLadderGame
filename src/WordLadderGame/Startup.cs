using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordLadderGame.Common;
using WordLadderGame.Engine.v0_1;
using WordLadderGame.Interfaces;

namespace WordLadderGame
{
    public static class Startup
    {
        #region Private Members & Constants
        private const string DEFAULT_LOCATION = @".\Infrastructure\words-english.txt";  // Default location of word list
        private const int WORD_LENGTH = 4;  // For this program we only care about 4-letter words.

        private static bool QuitFlag;
        #endregion

        #region Properties
        public static IWordLadder WordLadder { get; private set; }
        public static HashSet<string> WordList { get; set; }
        #endregion

        // Function run on start-up
        public static void Initialize(string targetLocation)
        {
            // If no argument specified, set target location to default
            targetLocation ??= DEFAULT_LOCATION;

            // Test location path for invalid characters
            if (targetLocation.Any(o => Path.GetInvalidPathChars().Any(c => c == o)))
            {
                Console.WriteLine(@"Target location path contains Invalid Characters. Reverting to default dictionary...");
                targetLocation = DEFAULT_LOCATION;
            }

            // Try to open file from target location
            try
            {
                using var file = new StreamReader(targetLocation);
                AssembleDictionary(file);
            }
            catch (Exception exception)
            {
                Console.WriteLine(@"The file at target location cannot be read.");
                Console.WriteLine(exception.Message);
                Console.WriteLine(@"Reverting to default dictionary...");
            }

            // If file cannot be found/opened or the assembled dictionary has 0 entries, open default dictionary file
            if (WordList.IsNullOrEmpty())
            {
                try
                {
                    using var file = new StreamReader(DEFAULT_LOCATION);
                    AssembleDictionary(file);
                }
                catch (Exception exception)
                {
                    // If default dictionary file cannot be opened, report error and throw exception
                    Console.WriteLine(@"The default dictionary cannot be read.");
                    Console.WriteLine(exception.Message);
                    throw exception;
                }
            }

            QuitFlag = false;
            WordLadder = new WordLadder();
        }

        // Main function
        public static void Run()
        {
            while (!QuitFlag)
            {
                Console.WriteLine(@"Please enter a starting word:");
                var startWord = GetWordFromInput();
                Console.WriteLine();

                Console.WriteLine(@"Please enter an ending word:");
                var endWord = GetWordFromInput();
                Console.WriteLine();
                                
                Console.WriteLine(@"--------------------------------------------------");
                Console.WriteLine(@"Processing...");

                // Pass information on to solution engine
                WordLadder.FindSolution(startWord, endWord);

                Console.WriteLine();

                bool release = false;
                while (!release)
                {
                    // Asks user to search again
                    Console.WriteLine(@"Do you wish to search again? [Y/N]");
                    var c = Console.ReadKey().Key;

                    switch (c)
                    {
                        case ConsoleKey.Y:
                            // User wants to search again - release and go back to beginnging of Run()
                            release = true;
                            break;
                        case ConsoleKey.N:
                            // User want to quit - release and set QuitFlag to true
                            release = true;
                            QuitFlag = true;
                            break;
                        default:
                            // Invalid input, switch requires Y or N key to be pressed for release
                            Console.WriteLine(@"Invalid input! Please select [Y]es or [N]o.");
                            break;
                    }
                }
            }
        }

        // Function run on closing the program
        public static void Close()
        {
            // Do stuff here that needs to be done before closing the program

            // Finally, announce program is closing
            Console.WriteLine(@"Terminating program...");
            Console.ReadKey();
        }

        #region Internal Helper Methods
        // Assembles in-memory Word List for the WordLadder engine
        internal static void AssembleDictionary(StreamReader file)
        {
            var wordList = new List<string>();

            while(!file.EndOfStream)
            {
                wordList.Add(file.ReadLine());
            }

            // Filter words so that they contain only letters (i.e. no punctuation) and are exactly equal to our word length
            // Order words alphabetically
            // HashSet the words so that all words are unique
            WordList = wordList.Where(o => o.IsAlpha() && o.Length == WORD_LENGTH)
                .OrderBy(o => o)
                .ToHashSet();
        }

        // Checks user input is valid for the WordLadder engine
        internal static string GetWordFromInput()
        {
            while(true)
            {
                var word = Console.ReadLine();

                if (word.Length != WORD_LENGTH)
                {
                    Console.WriteLine(@"Invalid word length!");
                }
                else if (!word.IsAlpha())
                {
                    Console.WriteLine(@"Invalid characters used!");
                }
                else if (!WordList.Contains(word))
                {
                    Console.WriteLine(@"Word is not in current dictionary!");
                }
                else
                {
                    return word.ToUpper();
                }
            }
        }
        #endregion
    }
}