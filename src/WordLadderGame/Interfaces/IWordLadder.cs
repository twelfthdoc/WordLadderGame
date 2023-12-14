namespace WordLadderGame.Interfaces
{
    /// <summary>
    /// Interface for Word Ladder engine
    /// </summary>
    public interface IWordLadder
    {
        public void FindSolution(string startWord, string endWord);
    }
}