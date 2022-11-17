using System.Diagnostics.CodeAnalysis;

namespace Word_Puzzle_Solver.Unit.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LoadWordDictionaryTest
    {
        private readonly LoadWordDictionary _loadWordDictionary;

        public LoadWordDictionaryTest()
        {
            _loadWordDictionary = new LoadWordDictionary();
        }

        [TestMethod]
        public async Task LoadDictionary_ValidPath_ReturnsStringArray()
        {
            // arrange
            string path = "words-english.txt";

            // act
            var response = await _loadWordDictionary.LoadDictionary(path, Constants.DesiredWordLength);

            // assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public async Task LoadDictionary_InvalidPath_ThrowsException()
        {
            // arrange
            string path = "invalidPath";

            // act
            await _loadWordDictionary.LoadDictionary(path, Constants.DesiredWordLength);
        }
    }
}
