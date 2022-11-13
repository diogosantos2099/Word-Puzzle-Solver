namespace Word_Puzzle_Solver
{
    public interface IExplorationAlgorithm
    {
        List<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse);
    }
}