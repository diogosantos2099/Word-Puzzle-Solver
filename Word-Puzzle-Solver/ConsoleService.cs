using Microsoft.Extensions.Hosting;
using Word_Puzzle_Solver;

internal class ConsoleService : IHostedService
{
    private readonly ILoadWordDictionary _loadWordDictionary;
    private readonly IValidateInput _validateInput;
    private readonly IExplorationAlgorithm _explorationAlgorithm;
    private readonly int DesiredWordLength = 4;

    public ConsoleService(
    ILoadWordDictionary loadWordDictionary,
    IValidateInput validateInput,
    IExplorationAlgorithm explorationAlgorithm)
    {
        _loadWordDictionary = loadWordDictionary;
        _validateInput = validateInput;
        _explorationAlgorithm = explorationAlgorithm;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            Console.WriteLine("Usage: file startWord endWord\n");
            Console.WriteLine("Options:");
            Console.WriteLine("file - the file name of the dictionary (with file extension)");
            Console.WriteLine($"startWord - {DesiredWordLength} characters long");
            Console.WriteLine($"endWord - {DesiredWordLength} characters long\n");
            Console.WriteLine("Ex: words-english.txt Spin Spot\n");
            Console.WriteLine("Please type input parameters:");

            var userInput = Console.ReadLine();

            var validatedInput = _validateInput.ValidateInputFromUser(userInput);

            if (validatedInput.Errors.Any())
            {
                Array.ForEach(validatedInput.Errors.ToArray(), Console.WriteLine);
                return;
            }

            string[] wordUniverse = await _loadWordDictionary.LoadDictionary(validatedInput.DictionaryFile!, DesiredWordLength);
            
            Console.WriteLine($"Finished loading Dictionary: {wordUniverse.Length} total available words.");
            
            LinkedList<string> result = _explorationAlgorithm.CalculateShortestPath(validatedInput.StartWord.ToLower(), validatedInput.EndWord.ToLower(), wordUniverse);

            Console.WriteLine("Solution:");
            Console.WriteLine(string.Join("->", result.Select(x => x)));
            Console.ReadLine();

        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}