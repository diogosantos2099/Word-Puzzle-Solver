namespace Word_Puzzle_Solver
{
    public interface ILoadWordDictionary
    {
        Task<string[]> LoadDictionary(string path, int wordLength);
    }
}