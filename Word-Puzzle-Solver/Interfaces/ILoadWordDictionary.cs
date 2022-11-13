namespace Word_Puzzle_Solver.Interfaces
{
    public interface ILoadWordDictionary
    {
        Task<string[]> LoadDictionary(string path, int desiredWordLength);
    }
}