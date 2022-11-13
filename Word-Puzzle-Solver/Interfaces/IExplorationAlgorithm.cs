namespace Word_Puzzle_Solver.Interfaces
{
    public interface IExplorationAlgorithm
    {
        List<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse);
    }
}