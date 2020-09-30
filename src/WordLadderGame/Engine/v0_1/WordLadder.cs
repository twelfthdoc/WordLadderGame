using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordLadderGame.Common;
using WordLadderGame.Interfaces;

namespace WordLadderGame.Engine.v0_1
{
    public class WordLadder : IWordLadder
    {
        #region Constructor
        public WordLadder()
        {
            WordList = Startup.WordList.ToList();   // Create a copy of the Word List at the start of the game
            NodeQueue = new Queue<TreeNode>();      // Initialize the Queue
        }
        #endregion

        #region Properties
        public List<string> WordList { get; set; }
        public Queue<TreeNode> NodeQueue { get; set; }
        #endregion

        public void FindSolution(string startWord, string endWord)
        {
            #region
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
            
            var rootNode = new TreeNode {Id = 1, ParentId = 0, Value = startWord, Parent = null};           
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

        #region Internal Helper Methods
        internal void FindChildrenNodes(TreeNode parent, string target)
        {
            if (!WordList.Contains(target)) return;

            var children = WordList.Where(o => parent.Value.IsSimilar(o)).ToList();

            WordList.RemoveAll(o => children.Any(s => s == o));

            var childNodes = children.Select(o => new TreeNode {ParentId = parent.Id, Parent = parent, Value = o}).ToList();

            foreach (var child in childNodes)
            {
                child.Id = NodeQueue.Last().Id + 1;
                NodeQueue.Enqueue(child);
            }
        }

        internal TreeNode ProcessQueue(string target)
        {
            TreeNode node;

            // Process the queue while the Queue is not empty
            while (NodeQueue.Any())
            {
                // Peek next node from the Queue
                node = NodeQueue.Peek();

                // If we have found the node we are looking for, return it.
                if (node.Value == target) return node;

                // Find Children nodes and assign this node as their parent.
                FindChildrenNodes(node, target);

                // Remove node from the Queue
                NodeQueue.Dequeue();
            }

            // If the Queue is empty, the target cannot be found.
            return null;
        }
        
        internal void PrintResultToConsole(IList<string> list)
        {
            foreach (var word in list)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine($@"Number of Steps: {list.Count - 1}");
            Console.WriteLine();
        }

        internal void SaveResultToFile(IList<string> list)
        {
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
            file.WriteLine($@"Number of Steps: {list.Count - 1}");
        }
        #endregion
    }
}