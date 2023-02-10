using TurtleGame.Settings;

namespace TurtleGame.Tests
{
    [TestFixture]
    public class TurtleGameTests
    {
        [Test]
        public void Move_ValidMoveIntoExit_SuccessResponse()
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(4, 5, (char)DirectionEnum.East);
            var exit = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(1,2),
                new Location(3,3),
            };

            var game = GameFactory.CreateGame(board, startingPosition, exit, mines);

            // Act
            game.Move();

            // Assert
            Assert.That(game.GameStatus, Is.EqualTo(StatusEnum.Success));
            StringAssert.AreEqualIgnoringCase("Success!! The turtle escaped!", game.LastMoveResult);
        }

        [Test]
        public void Move_ValidMoveIntoMine_ExplosionResponse()
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(3, 2, (char)DirectionEnum.North);
            var exit = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(1,2),
                new Location(3,3),
            };

            var game = GameFactory.CreateGame(board, startingPosition, exit, mines);

            // Act
            game.Move();

            // Assert
            Assert.That(game.GameStatus, Is.EqualTo(StatusEnum.Failure));
            StringAssert.AreEqualIgnoringCase("KABUMM!! The turtle blow up!!", game.LastMoveResult);
        }

        [Test]
        public void Move_ValidMoveIntoNothing_EmptyResponse()
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(2, 2, (char)DirectionEnum.South);
            var exit = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(1,2),
                new Location(3,3),
            };

            var game = GameFactory.CreateGame(board, startingPosition, exit, mines);

            // Act
            game.Move();

            // Assert
            Assert.That(game.GameStatus, Is.EqualTo(StatusEnum.Ongoing));
        }

        [TestCase(5, 5, DirectionEnum.North)]
        [TestCase(5, 5, DirectionEnum.East)]
        [TestCase(1, 5, DirectionEnum.West)]
        [TestCase(1, 1, DirectionEnum.South)]
        public void Move_InvalidMove_InvalidMoveResponse(int startingX, int startingY, DirectionEnum direction)
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(startingX, startingY, (char)direction);
            var exit = new Location(2, 5);
            var mines = new List<Location>
            {
                new Location(1,2),
                new Location(3,3),
            };

            var game = GameFactory.CreateGame(board, startingPosition, exit, mines);

            // Act
            game.Move();

            // Assert
            Assert.That(game.GameStatus, Is.EqualTo(StatusEnum.Started));
            StringAssert.AreEqualIgnoringCase("Turtle still in the maze!", game.LastMoveResult);
        }

        [Test]
        public void MultipleMoves_TurtleEscapes_SuccessResponse()
        {
            // Arrange
            var board = new Board(5, 5);
            var startingPosition = new Position(1, 1, (char)DirectionEnum.North);
            var exit = new Location(5, 5);
            var mines = new List<Location>
            {
                new Location(1,2),
                new Location(3,3),
            };

            var game = GameFactory.CreateGame(board, startingPosition, exit, mines);

            // Act
            game.Rotate();
            game.Move();
            game.Move();
            game.Move();
            game.Move();
            game.Rotate();
            game.Rotate();
            game.Rotate();
            game.Move();
            game.Move();
            game.Move();
            game.Move();

            // Assert
            Assert.That(game.GameStatus, Is.EqualTo(StatusEnum.Success));
            StringAssert.AreEqualIgnoringCase("Success!! The turtle escaped!", game.LastMoveResult);
        }
    }
}
