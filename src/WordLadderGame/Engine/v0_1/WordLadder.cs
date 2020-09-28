using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordLadderGame.Common;
using WordLadderGame.Interfaces;

namespace WordLadderGame.Engine.v0_1
{
    public class WordLadder : IWordLadder
    {
        public List<string> WordList { get; set; }

        public WordLadder()
        {
            WordList = Startup.WordList.ToList();
        }

        public void FindSolution(string startWord, string endWord)
        {
            /*
             * In principle, there are several ways to solve the game.
             * The key is to identify like words and connect them together.
             * Once this is done, it should possible to count the number of relationships
             * and this would be the minimum number of steps.
             * The method that most appeals to me is to create a TreeView heirarchy
             * by placing the starting word as the parent at the top of the tree
             * and allow the engine to computationally add nodes until it finds the ending word.
             * This should theoretically give the shortest number of steps,
             * provided the intermediate words are in the dictionary in use.
            */         

            // Remove trivial cases
            // If starting word and ending word are the same
            if (startWord == endWord)
            {
                Console.WriteLine(startWord);
                Console.WriteLine(@"Number of Steps: 0");
                Console.WriteLine();
                return;
            }

            // If starting word and ending word are one change away
            if (startWord.IsSimilar(endWord))
            {
                Console.WriteLine(startWord);
                Console.WriteLine(endWord);
                Console.WriteLine(@"Number of Steps: 1");
                Console.WriteLine();
                return;
            }

            // TODO: Implement Engine

            var tree = new TreeView();
            tree.Root.Value = startWord;
            var id = 2;

            foreach(var word in WordList)
            {
                if (word.IsSimilar(tree.Root.Value))
                {
                    tree.Root.Children.Add(new TreeNode
                    {
                        Id = id++,
                        ParentId = tree.Root.Id,
                        Generation = tree.Root.Generation + 1,
                        Value = word
                    });
                }
            }

            var dictionary = new Dictionary<int, string>();

        }
    }
}