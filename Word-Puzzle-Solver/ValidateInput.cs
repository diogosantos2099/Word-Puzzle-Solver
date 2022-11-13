namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Responsible for validating the input from the user.
    /// Returns object with list of failed validations, if any occur.
    /// </summary>
    public class ValidateInput : IValidateInput
    {
        private readonly string[] AvailableDictionaries = { "words-english.txt" };
        private const int ArgsExpected = 3;
        private const int WordLengthExpected = 4;
        
        public UserInput ValidateInputFromUser(string? userInput)
        {
            UserInput validatedInput = new();

            if (string.IsNullOrEmpty(userInput))
            {
                validatedInput.Errors.Add("Error: Input not provided");
                return validatedInput;
            }

            string[] args = userInput.Split(" ");
            if (args.Length != ArgsExpected)
            {
                validatedInput.Errors.Add($"Error: Input must be {ArgsExpected} parameters long: DictionaryFile StartWord EndWord");
                return validatedInput;
            }

            validatedInput.DictionaryFile = args[0];
            validatedInput.StartWord = args[1];
            validatedInput.EndWord = args[2];

            if (!AvailableDictionaries.Contains(validatedInput.DictionaryFile))
            {
                validatedInput.Errors.Add($"Error: Input '{validatedInput.DictionaryFile}' invalid. Available dictionaries: {string.Join("\n", AvailableDictionaries)}");
            }

            ValidateWordLength(validatedInput, validatedInput.StartWord);

            ValidateWordLength(validatedInput, validatedInput.EndWord);

            return validatedInput;
        }

        private static void ValidateWordLength(UserInput validatedInput, string word)
        {
            if (word.Length != WordLengthExpected)
            {
                validatedInput.Errors.Add($"Error: Input '{word}.length' must be equal to {WordLengthExpected}");
            }
        }
    }
}
