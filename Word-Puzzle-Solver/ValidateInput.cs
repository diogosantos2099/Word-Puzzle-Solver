using Word_Puzzle_Solver.Interfaces;
using Word_Puzzle_Solver.Models;

namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Responsible for validating the input from the user.
    /// Returns object with list of failed validations, if any occur.
    /// </summary>
    public class ValidateInput : IValidateInput
    {      
        public UserInput ValidateInputFromUser(string? userInput)
        {
            UserInput validatedInput = new();

            if (string.IsNullOrEmpty(userInput))
            {
                validatedInput.Errors.Add(Constants.NoInputProvided);
                return validatedInput;
            }

            string[] args = userInput.Split(" ");
            if (args.Length != Constants.ArgsExpected)
            {
                validatedInput.Errors.Add(Constants.WrongNrArgsProvided);
                return validatedInput;
            }

            validatedInput.DictionaryFile = args[0];
            validatedInput.StartWord = args[1];
            validatedInput.EndWord = args[2];

            if (!Constants.AvailableDictionaries.Contains(validatedInput.DictionaryFile))
            {
                validatedInput.Errors.Add($"Error: Input '{validatedInput.DictionaryFile}' invalid. Available dictionaries: {string.Join("\n", Constants.AvailableDictionaries)}");
            }

            ValidateWordLength(validatedInput, validatedInput.StartWord);

            ValidateWordLength(validatedInput, validatedInput.EndWord);

            return validatedInput;
        }

        private static void ValidateWordLength(UserInput validatedInput, string word)
        {
            if (word.Length != Constants.DesiredWordLength)
            {
                validatedInput.Errors.Add($"Error: Input '{word}.length' must be equal to {Constants.DesiredWordLength}");
            }
        }
    }
}
