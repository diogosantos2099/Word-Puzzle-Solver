using System.Text.RegularExpressions;
using Word_Puzzle_Solver.Algorithms.Base;

namespace Word_Puzzle_Solver.Algorithms
{
    /// <summary>
    /// First algorithm implemented for the puzzle word solving problem.
    /// 'This is Sparta' because it's mostly brute force.
    /// </summary>
    public class ThisIsSpartaAlgorithm : ExplorationAlgorithm
    {
        public override List<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse)
        {
            StartTimer();

            var shortestPath = new List<string>
            {
                startWord
            };

            var possiblePaths = new List<List<string>>()
            {
                new(shortestPath)
            };

            ExplorePossiblePaths(possiblePaths, endWord, wordUniverse);

            shortestPath = possiblePaths.First(x => x.Last() == endWord);

            StopTimer();
            return shortestPath;
        }

        private void ExplorePossiblePaths(List<List<string>> possiblePaths, string endWord, string[] wordUniverse)
        {
            // create copy
            var currentPossiblePaths = new List<List<string>>(possiblePaths);

            // iteration
            foreach (List<string> currentPath in currentPossiblePaths)
            {
                // start by removing this path from possiblePaths
                // if currentPath turns out not to be a dead end,
                // it will be added again at the end (accordingly)
                possiblePaths.Remove(currentPath);

                var wordArray = currentPath.Last().ToCharArray();
                List<string> wordCandidates = GetWordCandidates(wordUniverse, wordArray);

                // If no words exist, currentPath is a dead end
                if (!wordCandidates.Any())
                {
                    break;
                }

                // If one of the words is endWord, currentPath is the solution
                if (wordCandidates.Contains(endWord))
                {
                    currentPath.Add(endWord);
                    possiblePaths.Add(currentPath);
                    return;
                }

                // remove duplicates from candidates
                var realCandidates = wordCandidates.Except(currentPath).ToList();

                // add n new paths, one per word found
                foreach (string word in realCandidates)
                {
                    // copy currentPath
                    var newPathFound = new List<string>(currentPath)
                    {
                        // add word candidate
                        word
                    };
                    // add path
                    possiblePaths.Add(newPathFound);
                }
            }

            // check if there are still possible paths
            if (!possiblePaths.Any())
            {
                return; 
            }
            // if we haven't found endWord yet, keep exploring
            ExplorePossiblePaths(possiblePaths, endWord, wordUniverse);
        }

        private static List<string> GetWordCandidates(string[] wordUniverse, char[] wordArray)
        {
            var wordCandidates = new List<string>();

            // for each letter in the word of the last element
            for (int i = 0; i < wordArray.Length; i++)
            {
                string originalWord = new(wordArray);

                string replace = $"[^{wordArray[i]}]";
                string expression = originalWord.Remove(i, 1).Insert(i, replace);

                // Get all words from dictionary that match every character except this one
                Regex rgx = new(expression, RegexOptions.IgnoreCase);

                // add all of these words as candidates
                wordCandidates.AddRange(wordUniverse.Where(x => rgx.IsMatch(x)).ToList());
            }

            return wordCandidates;
        }
    }
}
