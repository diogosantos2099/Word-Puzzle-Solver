using Word_Puzzle_Solver.Models;

namespace Word_Puzzle_Solver.Interfaces
{
    public interface IValidateInput
    {
        UserInput ValidateInputFromUser(string? userInput);
    }
}