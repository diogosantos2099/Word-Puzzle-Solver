using System.Diagnostics.CodeAnalysis;

namespace Word_Puzzle_Solver.Unit.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class OutputGeneratorTest
    {
        private readonly OutputGenerator _outputGenerator;
        public OutputGeneratorTest()
        {
            _outputGenerator = new OutputGenerator();
        }

        [TestMethod]
        public async Task GenerateOutput_GeneratesFileSuccessfully()
        {
            // arrange
            string path = "testOutput.txt";
            var list = new List<string>() { "test1", "test2", "test3" };

            // act
            _outputGenerator.GenerateOutput(path, list);

            // assert
            var result = await File.ReadAllLinesAsync(path);
            Assert.IsNotNull(result);
            Assert.AreEqual(list.Count, result.Length);
        }
    }
}
