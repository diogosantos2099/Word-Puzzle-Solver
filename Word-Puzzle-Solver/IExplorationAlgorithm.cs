namespace Word_Puzzle_Solver
{
    public interface IExplorationAlgorithm
    {
        LinkedList<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse);
    }
}