using System.Diagnostics.CodeAnalysis;
using Word_Puzzle_Solver.Interfaces;

namespace Word_Puzzle_Solver
{
    /// <summary>
    /// Responsible for getting the user input.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ReadInput : IReadInput
    {
        public virtual string? GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}
