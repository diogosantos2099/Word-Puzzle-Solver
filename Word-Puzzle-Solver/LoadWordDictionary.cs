using System.Text.RegularExpressions;

namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Responsible for delivering the dictionary 
    /// containing only desired words
    /// </summary>
    public class LoadWordDictionary : ILoadWordDictionary
    {
        public async Task<string[]> LoadDictionary(string path, int desiredWordLength)
        {
            var fullDictionary = await File.ReadAllLinesAsync(path);
            return RemoveUndesirables(fullDictionary, desiredWordLength);
        }

        private static string[] RemoveUndesirables(string[] dictionary, int length)
        {
            string[] desiredLengthOnly = RemoveDifferentLength(dictionary, length);
            string[] desiredWordsOnly = RemoveNonWords(desiredLengthOnly);
            return desiredWordsOnly;
        }

        private static string[] RemoveDifferentLength(string[] dictionary, int length)
        {
            return dictionary.Where(x => x.Length == length).ToArray();
        }

        private static string[] RemoveNonWords(string[] dictionary)
        {
            // allow only upper case and lower case letters
            Regex rgx = new("^[a-zA-Z]+$", RegexOptions.Multiline);
            return dictionary.Where(x => rgx.IsMatch(x)).ToArray();
        }
    }
}
