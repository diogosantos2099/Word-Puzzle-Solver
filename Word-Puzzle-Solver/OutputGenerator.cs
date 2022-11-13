namespace Word_Puzzle_Solver
{
    public class OutputGenerator : IOutputGenerator
    {
        public async void GenerateOutput(string path, List<string> output)
        {
            await File.WriteAllLinesAsync(path, output);
        }
    }
}
