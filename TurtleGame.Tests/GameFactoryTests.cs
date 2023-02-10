using NUnit;
using TurtleGame.Settings;

namespace TurtleGame.Tests
{
    [TestFixture]
    public class GameFactoryTests
    {
        private const int BIGGER_THAN_MAXIMUM_SIZE = 10001;

        [Test]
        public void CreateGame_ValidArgs_ReturnGame()
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(1, 2, (char)DirectionEnum.North);
            var exitLocation = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(2,2),
                new Location(3,2)
            };

            // Act
            var game = GameFactory.CreateGame(board, startingPosition, exitLocation, mines);

            // Assert
            Assert.IsNotNull(game);

        }

        [TestCase(5, BIGGER_THAN_MAXIMUM_SIZE)]
        [TestCase(BIGGER_THAN_MAXIMUM_SIZE, 5)]
        public void CreateGame_InvalidBoardSize_InvalidArgsException(int boardHeight, int boardWidth)
        {
            // Arrange
            var board = new Board(boardHeight, boardWidth);
            var startingPosition = new Position(1, 2, (char)DirectionEnum.North);
            var exitLocation = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(2,2),
                new Location(3,2)
            };

            // Act Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => GameFactory.CreateGame(board, startingPosition, exitLocation, mines));
            Assert.That(ex.Message, Is.EqualTo("Board sizes must be more then 1 and less then 10000"));
        }

        [TestCase(6, 3)]
        [TestCase(2, 7)]
        public void CreateGame_InvalidStartingPosition_InvalidArgsException(int heightStart, int widthStart)
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(heightStart, widthStart, (char)DirectionEnum.North);
            var exitLocation = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(2,2),
                new Location(3,2)
            };

            // Act Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => GameFactory.CreateGame(board, startingPosition, exitLocation, mines));
            Assert.That(ex.Message, Is.EqualTo("Starting position must be inside board dimentions."));
        }

        [TestCase(6, 3)]
        [TestCase(2, 7)]
        public void CreateGame_InvalidExitPosition_InvalidArgsException(int heightExit, int widthExit)
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(2, 3, (char)DirectionEnum.North);
            var exitLocation = new Location(heightExit, widthExit);
            var mines = new List<Location>
            {
                new Location(2,2),
                new Location(3,2)
            };

            // Act Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => GameFactory.CreateGame(board, startingPosition, exitLocation, mines));
            Assert.That(ex.Message, Is.EqualTo("Exit location must be inside board dimentions."));
        }


        [TestCase(6, 3, "Mines locations must be inside board dimentions")]
        [TestCase(5, 5, "Exit location must not be equal to a mine location")]
        [TestCase(2, 3, "Starting position must not be equal to a mine location")]
        public void CreateGame_MineOutsideBoard_InvalidArgsException(int heightMine, int widthMine, string errorMessage)
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(2, 3, (char)DirectionEnum.North);
            var exitLocation = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(heightMine,widthMine),
                new Location(3,2)
            };

            // Act Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => GameFactory.CreateGame(board, startingPosition, exitLocation, mines));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
        }
    }
}
