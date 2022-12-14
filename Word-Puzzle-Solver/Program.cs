using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Word_Puzzle_Solver;
using Word_Puzzle_Solver.Algorithms;
using Word_Puzzle_Solver.Interfaces;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
        .AddHostedService<ConsoleService>()
        .AddSingleton<IReadInput, ReadInput>()
        .AddSingleton<ILoadWordDictionary, LoadWordDictionary>()
        .AddSingleton<IValidateInput, ValidateInput>()
        .AddSingleton<IExplorationAlgorithm, ThisIsSpartaAlgorithm>()
        .AddSingleton<IOutputGenerator, OutputGenerator>()
        .AddLogging(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.None)
                   .AddFilter("System", LogLevel.Warning)
                   .AddConsole();
        }))
    .Build();

await host.RunAsync();