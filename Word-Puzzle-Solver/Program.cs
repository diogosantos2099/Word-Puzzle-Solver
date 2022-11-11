using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Word_Puzzle_Solver;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
        .AddHostedService<ConsoleService>()
        .AddSingleton<ILoadWordDictionary, LoadWordDictionary>()
        .AddSingleton<IValidateInput, ValidateInput>())
    .Build();

await host.RunAsync();