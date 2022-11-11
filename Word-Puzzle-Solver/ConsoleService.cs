using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Word_Puzzle_Solver;

internal class ConsoleService : IHostedService
{
    private readonly ILogger _logger;
    private readonly ILoadWordDictionary _loadWordDictionary;
    private readonly IValidateInput _validateInput;

    public ConsoleService(
    ILogger<ConsoleService> logger,
    ILoadWordDictionary loadWordDictionary,
    IValidateInput validateInput)
    {
        _logger = logger;
        _loadWordDictionary = loadWordDictionary;
        _validateInput = validateInput;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            _logger.LogTrace("initializing-console-app");

            Console.WriteLine("Usage: DictionaryFile StartWord EndWord\n");
            Console.WriteLine("Options:");
            Console.WriteLine("DictionaryFile - the file name of a text file containing four letter words (with file extension)");
            Console.WriteLine("StartWord - 4 characters long");
            Console.WriteLine("EndWord - 4 characters long\n");
            Console.WriteLine("Ex: words-english.txt Spin Spot\n");
            Console.WriteLine("Please type input parameters:");

            var userInput = Console.ReadLine();

            var validatedInput = _validateInput.ValidateInputFromUser(userInput);

            if (validatedInput.Errors.Any())
            {
                Array.ForEach(validatedInput.Errors.ToArray(), Console.WriteLine);
                return;
            }

            _logger.LogTrace("loading-dictionary");
            var wordUniverse = await _loadWordDictionary.LoadDictionary(validatedInput.DictionaryFile!, 4);
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}