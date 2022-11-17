using System.Diagnostics.CodeAnalysis;

namespace Word_Puzzle_Solver.Unit.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateInputTest
    {
        private readonly ValidateInput _validateInput;
        public ValidateInputTest()
        {
            _validateInput = new ValidateInput();
        }

        [TestMethod]
        public void ValidateInputFromUser_ValidInput_ReturnsNoErrors()
        {
            // arrange
            string userInput = "words-english.txt spin spot";

            // act
            var response = _validateInput.ValidateInputFromUser(userInput);

            // assert
            Assert.IsNotNull(response);
            Assert.IsFalse(response.Errors.Any());
        }

        [TestMethod]
        public void ValidateInputFromUser_InvalidInput_ReturnsErrors()
        {
            // arrange
            string userInput = "words-english spinner spotter";

            // act
            var response = _validateInput.ValidateInputFromUser(userInput);

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(3, response.Errors.Count);
        }

        [TestMethod]
        public void ValidateInputFromUser_InvalidArgsNumber_ReturnsError()
        {
            // arrange
            string userInput = "arg1 arg2";
            string expectedMessage = Constants.WrongNrArgsProvided;

            // act
            var response = _validateInput.ValidateInputFromUser(userInput);

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual(expectedMessage, response.Errors.FirstOrDefault());
        }

        [TestMethod]
        public void ValidateInputFromUser_EmptyInput_ReturnsError()
        {
            // arrange
            string userInput = string.Empty;
            string expectedMessage = Constants.NoInputProvided;

            // act
            var response = _validateInput.ValidateInputFromUser(userInput);

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual(expectedMessage, response.Errors.FirstOrDefault());
        }

        [TestMethod]
        public void ValidateInputFromUser_NullInput_ReturnsError()
        {
            // arrange
            string? userInput = null;
            string expectedMessage = Constants.NoInputProvided;

            // act
            var response = _validateInput.ValidateInputFromUser(userInput);

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual(expectedMessage, response.Errors.FirstOrDefault());
        }
    }
}
