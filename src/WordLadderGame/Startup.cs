using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordLadderGame.Common;
using WordLadderGame.Interfaces;

namespace WordLadderGame
{
    public static class Startup
    {
        #region Private Members
        private const string DEFAULT_LOCATION = @".\Infrastructure\words-english.txt";
        private const int WORD_LENGTH = 5;
        #endregion

        #region Properties
        public static HashSet<string> Dictionary { get; set; }
        private static bool QuitFlag { get; set; } = false;
        public static IWordLadder WordLadder { get; }
        #endregion

        // Fucntion run on start-up
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
            if (Dictionary.IsNullOrEmpty())
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
        }

        // Main function
        public static void Run()
        {
            while (!QuitFlag)
            {
                var valid = false;
                string startWord = null;
                string endWord = null;

                do
                {
                    Console.WriteLine(@"Please enter a starting 5-letter word:");
                    startWord = Console.ReadLine();

                    if (startWord.Length != 5)
                    {
                        Console.WriteLine(@"Invalid word length!");
                    }
                    else if (!startWord.IsAlpha())
                    {
                        Console.WriteLine(@"Invalid characters used!");
                    }
                    else if (!Dictionary.Contains(startWord))
                    {
                        Console.WriteLine(@"Word is not in current dictionary!");
                    }
                    else
                    {
                        startWord = startWord.ToUpper();
                        valid = true;
                    }

                    Console.WriteLine();
                }
                while (!valid);

                valid = false;

                do
                {
                    Console.WriteLine(@"Please enter a ending 5-letter word:");
                    endWord = Console.ReadLine();

                    if (endWord.Length != 5)
                    {
                        Console.WriteLine(@"Invalid word length!");
                    }
                    else if (!endWord.IsAlpha())
                    {
                        Console.WriteLine(@"Invalid characters used!");
                    }
                    else if (!Dictionary.Contains(endWord))
                    {
                        Console.WriteLine(@"Word is not in current dictionary!");
                    }
                    else
                    {
                        endWord = endWord.ToUpper();
                        valid = true;
                    }

                    Console.WriteLine();
                }
                while (!valid);

                Console.WriteLine(@"--------------------------------------------------");
                Console.WriteLine(@"Processing...");

                WordLadder.FindSolution(startWord, endWord);
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
        // Assembles in-memory Dictionary for Word Ladder game
        internal static void AssembleDictionary(StreamReader file)
        {
            var wordList = new List<string>();

            while(!file.EndOfStream)
            {
                wordList.Add(file.ReadLine());
            }

            // Filter words so that they contain only letters (i.e. no punctuation) and are exactly equal to our word length
            Dictionary = wordList.Where(o => o.IsAlpha() && o.Length == WORD_LENGTH).ToHashSet();
        }
        #endregion
    }
}