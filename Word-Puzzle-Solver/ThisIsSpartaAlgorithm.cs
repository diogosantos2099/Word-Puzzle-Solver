using System.Text.RegularExpressions;

namespace Word_Puzzle_Solver
{
    /// <summary>
    /// First algorithm implemented for the puzzle word solving problem.
    /// 'This is Sparta' because it's mostly brute force.
    /// </summary>
    public class ThisIsSpartaAlgorithm : ExplorationAlgorithm
    {
        public override LinkedList<string> CalculateShortestPath(string startWord, string endWord, string[] wordUniverse)
        {
            StartTimer();

            var shortestPath = new LinkedList<string>();
            shortestPath.AddFirst(startWord);

            var possiblePaths = new List<LinkedList<string>>()
            {
                new(shortestPath)
            };

            ExplorePossiblePaths(possiblePaths, endWord, wordUniverse);

            shortestPath = possiblePaths.First(x => x.Last() == endWord);

            StopTimer();
            return shortestPath;
        }

        private void ExplorePossiblePaths(List<LinkedList<string>> possiblePaths, string endWord, string[] wordUniverse)
        {
            // create copy
            var currentPossiblePaths = new List<LinkedList<string>>(possiblePaths);

            // iteration
            foreach (LinkedList<string> currentPath in currentPossiblePaths)
            {
                // start by removing this path from possiblePaths
                // if currentPath turns out not to be a dead end,
                // it will be added again at the end (accordingly)
                possiblePaths.Remove(currentPath);

                var wordArray = currentPath.Last().ToCharArray();
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

                // If no words exist, currentPath is a dead end
                if (!wordCandidates.Any())
                {
                    break;
                }

                // If one of the words is endWord, currentPath is the solution
                if (wordCandidates.Contains(endWord))
                {
                    currentPath.AddLast(endWord);
                    possiblePaths.Add(currentPath);
                    return;
                }
                else
                {
                    // remove duplicates from candidates
                    var realCandidates = wordCandidates.Except(currentPath).ToList();

                    // add n LinkedLists, one per word found
                    foreach (string word in realCandidates)
                    {
                        // copy currentPath
                        var newLinkedList = new LinkedList<string>(currentPath);
                        // add word candidate
                        newLinkedList.AddLast(word);
                        // add LinkedList
                        possiblePaths.Add(newLinkedList);
                    }
                }
            }

            // if we haven't found endWord yet, keep exploring
            ExplorePossiblePaths(possiblePaths, endWord, wordUniverse);
        }
    }
}
