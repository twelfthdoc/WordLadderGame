using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
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
             * The method that most appeals to me is to create a B-Tree heirarchy
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

            var rootNode = new TreeNode { Id = startWord, Generation = 0 };

            AttachChildrenNodes(rootNode, endWord);


        }


        internal void AttachChildrenNodes(TreeNode parent, string endWord)
        {
            if (!WordList.Contains(endWord)) return;

            var children = WordList.Where(o => parent.Id.IsSimilar(o)).ToList();

            WordList.RemoveAll(o => children.Any(s => s == o));

            var childNodes = children.Select(o => new TreeNode { ParentId = parent.Id, Generation = parent.Generation + 1, Id = o }).ToList();
            parent.Children = childNodes;

            foreach (var child in childNodes)
            {
                AttachChildrenNodes(child, endWord);
            }
        }
    }
}