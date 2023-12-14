using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordLadderGame.Common;
using WordLadderGame.Interfaces;

namespace WordLadderGame.Engine.v0_1
{
    /// <summary>
    /// Class that contains the core functionality for the program
    /// </summary>
    public class WordLadder : IWordLadder
    {
        #region Constructor
        /// <summary>
        /// Creates a WordLadder engine object, which finds and returns solutions to given problems.
        /// </summary>
        static WordLadder()
        {
            WordList = Startup.WordList.ToList();   // Create a copy of the Word List at the start of the game
            NodeQueue = new Queue<TreeNode>();      // Initialize the Queue
        }
        #endregion

        #region Destructor
        // Not strictly necessary, as most C# applications have built-in garbage collectors that handle destroyed objects.
        ~WordLadder()
        {
            WordList = null;
            NodeQueue = null;
        }
        #endregion

        #region Properties
        public static List<string> WordList { get; set; }
        public static Queue<TreeNode> NodeQueue { get; set; }
        #endregion

        /// <summary>
        /// Returns the shortest possible word ladder from the starting word to the ending word
        /// </summary>
        /// <param name="startWord">
        /// The starting word
        /// </param>
        /// <param name="endWord">
        /// The ending word
        /// </param>
        public void FindSolution(string startWord, string endWord)
        {
            #region Comments and Findings
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

            /*
             * 30/09/2020:
             * 
             * I have found a simpler approach to the one I was envisaging.
             * The method I was previously using relied on recursion and as such could not
             * guarantee the most optimal result. I realised by switching to using a Queue
             * and processing each node by iteration instead of recursion, that it might take
             * more time to process, however it should always return the most optimal result.
             */

            /*
             * 20/12/2021:
             * 
             * Having a glance over old code. Made the helper methods static as well as the
             * List and Queue. Also added a destructor to make sure those objects are disposed
             * properly. I think this lends itself to thorough and rigourous unit testing of the methods,
             * to make sure I haven't broken anything by slipping empty strings to them!
             * The solution hasn't changed however, I think it's pretty slick.
             * I should look into B-Trees again at some point and see if there's an even
             * better solution to the game, especially with the latest versions of C#.
             */

            /*
             * 14/12/2023:
             *
             * Made some small tweaks here and there, including adding XML comments on classes and methods.
             * Updated main framework from netcoreapp3.1 to net8.0.
             * 
             */
            #endregion

            // Create the Word Ladder
            var wordLadder = new List<string>();

            //-- Handle trivial cases --//
            // If starting word and ending word are the same
            if (startWord == endWord)
            {
                wordLadder.Add(startWord);
                PrintResultToConsole(wordLadder);
                SaveResultToFile(wordLadder);
                return;
            }

            // If starting word and ending word are one change away
            if (startWord.IsSimilar(endWord))
            {
                wordLadder.Add(startWord);
                wordLadder.Add(endWord);
                PrintResultToConsole(wordLadder);
                SaveResultToFile(wordLadder);
                return;
            }

            // N.B. Recursion will work but will not give the most accurate answer - add nodes to a Queue and process via iteration instead.
            var rootNode = new TreeNode { Id = 1, ParentId = 0, Value = startWord, Parent = null };
            NodeQueue.Enqueue(rootNode);

            var targetNode = ProcessQueue(endWord);

            // If a word ladder cannot be found, inform the user and end the game.
            if (targetNode == null)
            {
                Console.WriteLine(@$"Cannot find a path between {startWord} and {endWord}.");
                Console.WriteLine();
                return;
            }

            wordLadder.Add(targetNode.Value);

            // Traverse each node and add value to Word Ladder
            do
            {
                // Add words to the Word Ladder, starting with the last word
                targetNode = targetNode.Parent;
                wordLadder.Add(targetNode.Value);
            }
            while (targetNode.ParentId != 0);

            // Reverse the list to make it start with the first word
            wordLadder.Reverse();

            // Print Word Ladder to Console
            PrintResultToConsole(wordLadder);

            // Save Word Ladder to File
            SaveResultToFile(wordLadder);
        }

        #region Internal Static Helper Methods
        /// <summary>
        /// Processes the Queue to return the target node
        /// </summary>
        /// <param name="target"></param>
        /// <returns>
        /// Returns the target node if it is found, else returns <see langword="null"/>
        /// </returns>
        internal static TreeNode ProcessQueue(string target)
        {
            // Process the queue while the Queue is not empty
            while (NodeQueue.Any())
            {
                // Peek next node from the Queue
                var node = NodeQueue.Peek();

                // If we have found the node we are looking for, return it.
                if (node.Value == target) return node;

                // Find Children nodes and assign this node as their parent.
                FindChildrenNodes(node, target);

                // Remove node from the Queue
                _ = NodeQueue.Dequeue();
            }

            // If the Queue is empty, the target cannot be found.
            return null;
        }

        /// <summary>
        /// Finds and attaches all children nodes to the parent node
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="target"></param>
        internal static void FindChildrenNodes(TreeNode parent, string target)
        {
            if (!WordList.Contains(target)) return; // Sanity check that target is still in word list

            var children = WordList.Where(o => parent.Value.IsSimilar(o)).ToList();

            _ = WordList.RemoveAll(o => children.Any(s => s == o));

            children.Select(o => new TreeNode { Id = parent.Id + 1, ParentId = parent.Id, Parent = parent, Value = o })
                    .ToList()
                    .ForEach(o => NodeQueue.Enqueue(o));
        }

        /// <summary>
        /// Writes the strings in the enumerable to the Console.
        /// </summary>
        /// <param name="list"></param>
        internal static void PrintResultToConsole(IEnumerable<string> list)
        {
            list ??= new List<string>();

            foreach (var word in list)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine($@"Number of Steps: {list.Count() - 1}");
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the strings in the enumerable to a file, for later access.
        /// </summary>
        /// <param name="list"></param>
        internal static void SaveResultToFile(IEnumerable<string> list)
        {
            list ??= new List<string>();

            var targetPath = Startup.ResultsFile;

            // If file exists, delete it before creating new one
            if (File.Exists(targetPath))
            {
                File.Delete(targetPath);
            }

            using var file = File.CreateText(targetPath);

            foreach (var word in list)
            {
                file.WriteLine(word);
            }

            file.WriteLine(@"--------------------------------------------------");
            file.WriteLine($@"Number of Steps: {list.Count() - 1}");
        }
        #endregion
    }
}