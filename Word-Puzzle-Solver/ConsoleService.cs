using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Word_Puzzle_Solver;

internal class ConsoleService : IHostedService
{
    private readonly ILogger _logger;
    private readonly ILoadWordDictionary _loadWordDictionary;

    public ConsoleService(
    ILogger<ConsoleService> logger,
    ILoadWordDictionary loadWordDictionary)
    {
        _logger = logger;
        _loadWordDictionary = loadWordDictionary;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            _logger.LogTrace("initializing-console-app");

            Console.WriteLine("Usage: DictionaryFile StartWord EndWord");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("DictionaryFile - the file name of a text file containing four letter words (with file extension)");
            Console.WriteLine("StartWord - 4 characters long");
            Console.WriteLine("EndWord - 4 characters long");
            Console.WriteLine();
            Console.WriteLine("Ex: words-english.txt Spin Spot");
            Console.WriteLine();
            Console.WriteLine("Please type input parameters:");

            var userInput = Console.ReadLine();

            var errors = ValidateInputFromUser(userInput);

            if (errors.Any())
            {
                Array.ForEach(errors.ToArray(), Console.WriteLine);
                return;
            }

            _logger.LogTrace("loading-dictionary");
            //TODO: replace "words-english.txt" with constant from input
            var wordUniverse = await _loadWordDictionary.LoadDictionary(@"words-english.txt", 4);
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    static List<string> ValidateInputFromUser(string? userInput)
    {
        List<string> errors = new();

        if (string.IsNullOrEmpty(userInput))
        {
            errors.Add("Error: Input not provided");
            return errors;
        }

        string[] args = userInput.Split(" ");
        if (args.Length != 3)
        {
            errors.Add("Error: Input must be 3 parameters long: DictionaryFile StartWord EndWord");
            return errors;
        }

        string[] availableDictionaries = { "words-english.txt" };
        string dictionary = args[0];
        string startWord = args[1];
        string endWord = args[2];

        if (!availableDictionaries.Contains(dictionary))
        {
            errors.Add($"Error: Input '{dictionary}' invalid. Available dictionaries: {string.Join("\n", availableDictionaries)}");
        }

        ValidateWordLength(errors, startWord);

        ValidateWordLength(errors, endWord);

        return errors;
    }

    static void ValidateWordLength(List<string> errors, string word)
    {
        if (word.Length != 4)
        {
            errors.Add($"Error: Input '{word}.length' must be equal to 4");
        }
    }
}