using Microsoft.Extensions.Hosting;
using Word_Puzzle_Solver;
using Word_Puzzle_Solver.Interfaces;

public class ConsoleService : IHostedService
{
    private readonly IReadInput _readInput;
    private readonly ILoadWordDictionary _loadWordDictionary;
    private readonly IValidateInput _validateInput;
    private readonly IExplorationAlgorithm _explorationAlgorithm;
    private readonly IOutputGenerator _outputGenerator;

    public ConsoleService(
    IReadInput readInput,
    ILoadWordDictionary loadWordDictionary,
    IValidateInput validateInput,
    IExplorationAlgorithm explorationAlgorithm,
    IOutputGenerator outputGenerator)
    {
        _readInput = readInput;
        _loadWordDictionary = loadWordDictionary;
        _validateInput = validateInput;
        _explorationAlgorithm = explorationAlgorithm;
        _outputGenerator = outputGenerator;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            PrintInstructions();

            var userInput = _readInput.GetUserInput();

            var validatedInput = _validateInput.ValidateInputFromUser(userInput);

            if (validatedInput.Errors.Any())
            {
                Array.ForEach(validatedInput.Errors.ToArray(), Console.WriteLine);
                return;
            }

            string[] wordUniverse = await _loadWordDictionary.LoadDictionary(validatedInput.DictionaryFile, Constants.DesiredWordLength);

            Console.WriteLine($"Finished loading Dictionary: {wordUniverse.Length} total available words.");

            List<string> result = _explorationAlgorithm.CalculateShortestPath(validatedInput.StartWord.ToLower(), validatedInput.EndWord.ToLower(), wordUniverse);

            _outputGenerator.GenerateOutput(Constants.OutputDir, result);

            Console.WriteLine("Solution:");
            Console.WriteLine(string.Join("->", result.Select(x => x)));
            _readInput.GetUserInput();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }

        return;
    }

    private static void PrintInstructions()
    {
        Console.WriteLine("Usage: file startWord endWord\n");
        Console.WriteLine("Options:");
        Console.WriteLine("file - the file name of the dictionary (with file extension)");
        Console.WriteLine($"startWord - {Constants.DesiredWordLength} characters long");
        Console.WriteLine($"endWord - {Constants.DesiredWordLength} characters long\n");
        Console.WriteLine("Ex: words-english.txt Spin Spot\n");
        Console.WriteLine("Please type input parameters:");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}