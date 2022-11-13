using System.Diagnostics;
using Word_Puzzle_Solver.Interfaces;

namespace Word_Puzzle_Solver.Algorithms.Base
{
    /// <summary>
    /// Purpose: abstract class to allow for multiple algorithms 
    /// that each calculate the shortest path in their own way.
    /// </summary>
    public abstract class ExplorationAlgorithm : IExplorationAlgorithm
    {
        private readonly Stopwatch _timer = new();

        public abstract List<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse);

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
