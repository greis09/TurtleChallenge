using Moq;
using TurtleGame.Adapters.DTO;

namespace TurtleGame.Adapters.Tests
{
    [TestFixture]
    public class FileAdapterTests
    {
        private readonly Mock<IFileManager> fileManager;

        public FileAdapterTests()
        {
            fileManager = new Mock<IFileManager>();
        }
        [Test]
        public void RunGame_ValidSettings_SuccessResult()
        {
            // Arrange
            var movesPath = "moves.json";
            var settingsPath = "settings.json";

            var movesFile = new MovesFileDTO
            {
                MovesSequences = new List<MoveSequence>
                {
                    new MoveSequence
                    {
                        Moves = new List<char> { 'R', 'M', 'M', 'M', 'M', 'R', 'R', 'R', 'M', 'M', 'M', 'M' }
                    }
                }
            };

            var settingsFile = new SettingsFileDTO
            {
                Board = new Settings.Board(5, 5),
                InitialPosition = new Settings.Position(1, 1, (char)Settings.DirectionEnum.North),
                Exit = new Settings.Location(5, 5),
                Mines = new List<Settings.Location>
                {
                    new Settings.Location(1,2),
                    new Settings.Location(3,3),
                }
            };
            fileManager.Setup(fm => fm.ReadFile<MovesFileDTO>(movesPath)).Returns(movesFile);
            fileManager.Setup(fm => fm.ReadFile<SettingsFileDTO>(settingsPath)).Returns(settingsFile);

            var fileAdpter = new FileAdapter(fileManager.Object);

            // Act
            var actual = fileAdpter.RunGame(settingsPath, movesPath);

            // Assert
            StringAssert.AreEqualIgnoringCase("Sequence 1: Success!! The turtle escaped!\r\n", actual);
        }

        [Test]
        public void RunGame_InvalidMovesFile_ThrowsException()
        {
            // Arrange
            var movesPath = "moves.json";
            var settingsPath = "settings.json";

            var settingsFile = new SettingsFileDTO
            {
                Board = new Settings.Board(5, 5),
                InitialPosition = new Settings.Position(1, 1, (char)Settings.DirectionEnum.North),
                Exit = new Settings.Location(5, 5),
                Mines = new List<Settings.Location>
                {
                    new Settings.Location(1,2),
                    new Settings.Location(3,3),
                }
            };
            fileManager.Setup(fm => fm.ReadFile<MovesFileDTO>(movesPath)).Returns(default(MovesFileDTO));
            fileManager.Setup(fm => fm.ReadFile<SettingsFileDTO>(settingsPath)).Returns(settingsFile);

            var fileAdpter = new FileAdapter(fileManager.Object);

            // Act
            // Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => fileAdpter.RunGame(settingsPath, movesPath));
            Assert.That(ex.Message, Is.EqualTo("Error loading moves file."));
        }

        [Test]
        public void RunGame_InvalidSettingsFile_ThrowsException()
        {
            // Arrange
            var movesPath = "moves.json";
            var settingsPath = "settings.json";

            var movesFile = new MovesFileDTO
            {
                MovesSequences = new List<MoveSequence>
                {
                    new MoveSequence
                    {
                        Moves = new List<char> { 'R', 'M', 'M', 'M', 'M', 'R', 'R', 'R', 'M', 'M', 'M', 'M' }
                    }
                }
            };
            fileManager.Setup(fm => fm.ReadFile<MovesFileDTO>(movesPath)).Returns(movesFile);
            fileManager.Setup(fm => fm.ReadFile<SettingsFileDTO>(settingsPath)).Returns(default(SettingsFileDTO));

            var fileAdpter = new FileAdapter(fileManager.Object);

            // Act
            // Assert
            ArgumentException ex = Assert.Throws<ArgumentException>(() => fileAdpter.RunGame(settingsPath, movesPath));
            Assert.That(ex.Message, Is.EqualTo("Error loading settings file."));
        }

        [Test]
        public void RunGame_ValidSettingsWithExtrMoves_SuccessResult()
        {
            // Arrange
            var movesPath = "moves.json";
            var settingsPath = "settings.json";

            var movesFile = new MovesFileDTO
            {
                MovesSequences = new List<MoveSequence>
                {
                    new MoveSequence
                    {
                        Moves = new List<char> { 'R', 'M', 'M', 'M', 'M', 'X', 'R', 'R', 'R', 'M', 'M', 'M', 'M', 'R', 'M' }
                    }
                }
            };

            var settingsFile = new SettingsFileDTO
            {
                Board = new Settings.Board(5, 5),
                InitialPosition = new Settings.Position(1, 1, (char)Settings.DirectionEnum.North),
                Exit = new Settings.Location(5, 5),
                Mines = new List<Settings.Location>
                {
                    new Settings.Location(1,2),
                    new Settings.Location(3,3),
                }
            };
            fileManager.Setup(fm => fm.ReadFile<MovesFileDTO>(movesPath)).Returns(movesFile);
            fileManager.Setup(fm => fm.ReadFile<SettingsFileDTO>(settingsPath)).Returns(settingsFile);

            var fileAdpter = new FileAdapter(fileManager.Object);

            // Act
            var actual = fileAdpter.RunGame(settingsPath, movesPath);

            // Assert
            StringAssert.AreEqualIgnoringCase("Sequence 1: Success!! The turtle escaped!\r\n", actual);
        }
    }
}
