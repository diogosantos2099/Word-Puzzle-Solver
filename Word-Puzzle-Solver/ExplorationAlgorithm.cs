namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Purpose: abstract class to allow for multiple algorithms 
    /// that each calculate the shortst path in their own way.
    /// </summary>
    public abstract class ExplorationAlgorithm : IExplorationAlgorithm
    {
        public abstract LinkedList<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse);
    }
}
