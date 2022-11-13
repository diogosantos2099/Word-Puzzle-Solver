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

            //^[a-zA-Z]+$: allow only upper case and lower case letters
            string[] desiredWordsOnly = ApplyRegex(desiredLengthOnly, "^[a-zA-Z]+$");
            
            return desiredWordsOnly;
        }

        private static string[] RemoveDifferentLength(string[] dictionary, int length)
        {
            return dictionary.Where(x => x.Length == length).ToArray();
        }

        private static string[] ApplyRegex(string[] dictionary, string regexExpression)
        {
            Regex rgx = new(regexExpression, RegexOptions.Multiline);
            return dictionary.Where(x => rgx.IsMatch(x)).ToArray();
        }
    }
}
