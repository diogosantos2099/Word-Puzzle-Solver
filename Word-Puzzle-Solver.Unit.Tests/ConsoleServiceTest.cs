using Moq;
using System.Diagnostics.CodeAnalysis;
using Word_Puzzle_Solver.Interfaces;
using Word_Puzzle_Solver.Models;

namespace Word_Puzzle_Solver.Unit.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ConsoleServiceTest
    {
        private readonly Mock<IReadInput> _readInput;
        private readonly Mock<ILoadWordDictionary> _loadWordDictionary;
        private readonly Mock<IValidateInput> _validateInput;
        private readonly Mock<IExplorationAlgorithm> _explorationAlgorithm;
        private readonly Mock<IOutputGenerator> _outputGenerator;
        private readonly ConsoleService _consoleService;

        public ConsoleServiceTest()
        {
            _readInput = new Mock<IReadInput>();
            _loadWordDictionary = new Mock<ILoadWordDictionary>();
            _validateInput = new Mock<IValidateInput>();
            _explorationAlgorithm = new Mock<IExplorationAlgorithm>();
            _outputGenerator = new Mock<IOutputGenerator>();
            _consoleService = new ConsoleService(
                _readInput.Object,
                _loadWordDictionary.Object, 
                _validateInput.Object, 
                _explorationAlgorithm.Object, 
                _outputGenerator.Object);
        }

        [TestMethod]
        public async Task StartAsync_RunsSuccessfully()
        {
            // arrange
            var typedInput = "words-english.txt spin spot";
            var userInput = new UserInput() 
            {
                DictionaryFile = "words-english.txt",
                StartWord = "spin",
                EndWord = "spot"
            };
            string[] wordUniverse = { "spin", "shin", "skin", "spit", "spat", "spun", "spot",
                                        "same", "came", "case", "cast", "cost" };
            var shortestPath = new List<string>()
            {
                "spin", "spit", "spot"
            };
            _readInput
                .Setup(x => x.GetUserInput())
                .Returns(typedInput);
            _validateInput
                .Setup(x => x.ValidateInputFromUser(It.IsAny<string>()))
                .Returns(userInput);
            _loadWordDictionary
                .Setup(x => x.LoadDictionary(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(wordUniverse);
            _explorationAlgorithm
                .Setup(x => x.CalculateShortestPath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns(shortestPath);
            _outputGenerator
                .Setup(x => x.GenerateOutput(It.IsAny<string>(), It.IsAny<List<string>>()));

            // act
            await _consoleService.StartAsync(new CancellationToken());

            // assert
            _readInput.Verify(x => x.GetUserInput(), Times.Exactly(2));
            _validateInput.Verify(x => x.ValidateInputFromUser(typedInput), Times.Once);
            _loadWordDictionary.Verify(x => x.LoadDictionary(userInput.DictionaryFile, Constants.DesiredWordLength), Times.Once);
            _explorationAlgorithm.Verify(x => x.CalculateShortestPath(userInput.StartWord, userInput.EndWord, wordUniverse), Times.Once);
            _outputGenerator.Verify(x => x.GenerateOutput(It.IsAny<string>(), shortestPath), Times.Once);
        }

        [TestMethod]
        public async Task StartAsync_EmptyInput_ReturnsPrematurely()
        {
            // arrange
            var typedInput = string.Empty;
            var userInput = new UserInput()
            {
                DictionaryFile = string.Empty,
                StartWord = string.Empty,
                EndWord = string.Empty,
                Errors = new List<string>() { Constants.NoInputProvided }
            };
            
            _readInput
                .Setup(x => x.GetUserInput())
                .Returns(typedInput);
            _validateInput
                .Setup(x => x.ValidateInputFromUser(It.IsAny<string>()))
                .Returns(userInput);

            // act
            await _consoleService.StartAsync(new CancellationToken());

            // assert
            _readInput.Verify(x => x.GetUserInput(), Times.Once);
            _validateInput.Verify(x => x.ValidateInputFromUser(typedInput), Times.Once);
            _loadWordDictionary.Verify(x => x.LoadDictionary(It.IsAny<string>(), Constants.DesiredWordLength), Times.Never);
            _explorationAlgorithm.Verify(x => x.CalculateShortestPath(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()), Times.Never);
            _outputGenerator.Verify(x => x.GenerateOutput(It.IsAny<string>(), It.IsAny<List<string>>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public async Task StartAsync_SomeComponentThrowsException_Throw()
        {
            // arrange
            var typedInput = "some input";
            var userInput = new UserInput()
            {
                DictionaryFile = string.Empty,
                StartWord = string.Empty,
                EndWord = string.Empty
            };

            _readInput
                .Setup(x => x.GetUserInput())
                .Returns(typedInput);
            _validateInput
                .Setup(x => x.ValidateInputFromUser(It.IsAny<string>()))
                .Returns(userInput);
            _loadWordDictionary
                .Setup(x => x.LoadDictionary(It.IsAny<string>(), It.IsAny<int>()))
                .ThrowsAsync(new FileNotFoundException());

            // act
            await _consoleService.StartAsync(new CancellationToken());
        }

        [TestMethod]
        public async Task StopAsync_RunsSuccessfully()
        {
            await _consoleService.StopAsync(new CancellationToken());
        }
    }
}
