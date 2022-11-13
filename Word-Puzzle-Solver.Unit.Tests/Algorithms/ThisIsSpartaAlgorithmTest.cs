using Word_Puzzle_Solver.Algorithms;

namespace Word_Puzzle_Solver.Unit.Tests.Algorithms
{
    [TestClass]
    public class ThisIsSpartaAlgorithmTest
    {
        private readonly ThisIsSpartaAlgorithm _explorationAlgorithm;

        public ThisIsSpartaAlgorithmTest()
        {
            _explorationAlgorithm = new ThisIsSpartaAlgorithm();
        }

        [TestMethod]
        public void CalculateShortestPath_Spin_Spot_ReturnsShortestPath()
        {
            // arrange
            string startWord = "spin";
            string endWord = "spot";
            string[] wordUniverse = { "spin", "shin", "skin", "spit", "spat", "spun", "spot" };

            // act
            var response = _explorationAlgorithm.CalculateShortestPath(startWord, endWord, wordUniverse);

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(3, response.Count);
            Assert.IsTrue(response.First().Equals(startWord));
            Assert.IsTrue(response.Last().Equals(endWord));
        }

        [TestMethod]
        public void CalculateShortestPath_Same_Cost_ReturnsShortestPath()
        {
            // arrange
            string startWord = "same";
            string endWord = "cost";
            string[] wordUniverse = { "spin", "shin", "skin", "spit", "spat", "spun", "spot",
                                        "same", "came", "case", "cast", "cost"};

            // act
            var response = _explorationAlgorithm.CalculateShortestPath(startWord, endWord, wordUniverse);

            // assert
            Assert.IsNotNull(response);
            Assert.AreEqual(5, response.Count);
            Assert.IsTrue(response.First().Equals(startWord));
            Assert.IsTrue(response.Last().Equals(endWord));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateShortestPath_Spin_Spot_WhenNoSolution_ThrowsException()
        {
            // arrange
            string startWord = "spin";
            string endWord = "spot";
            // there is no way to reach spot with this dictionary
            string[] wordUniverse = { "spin", "shin", "skin", "spit", "spat", "spun" };

            // act
            _explorationAlgorithm.CalculateShortestPath(startWord, endWord, wordUniverse);
        }
    }
}
