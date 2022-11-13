using System.Diagnostics;

namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Purpose: abstract class to allow for multiple algorithms 
    /// that each calculate the shortst path in their own way.
    /// </summary>
    public abstract class ExplorationAlgorithm : IExplorationAlgorithm
    {
        private readonly Stopwatch _timer = new();

        public abstract LinkedList<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse);

        public void StartTimer()
        {
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();

            TimeSpan timeTaken = _timer.Elapsed;
            Console.WriteLine($"Time taken: {timeTaken:m\\:ss\\.fff}");
        }
    }
}
