using System.Text;
using TurtleGame.Adapters.DTO;
using TurtleGame.Settings;

namespace TurtleGame.Adapters
{
    public class FileAdapter : IFileAdapter
    {
        private readonly IFileManager fileManager;

        public FileAdapter(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        public string RunGame(string pathToSettings, string pathToMoves)
        {
            var result = new StringBuilder();
            int sequenceNumber = 1;

            var moves = fileManager.ReadFile<MovesFileDTO>(pathToMoves);
            if(moves == null)
            {
                throw new ArgumentException("Error loading moves file.");
            }

            foreach (var moveSequence in moves.MovesSequences)
            {
                var settings = fileManager.ReadFile<SettingsFileDTO>(pathToSettings);
                if (settings == null)
                {
                    throw new ArgumentException("Error loading settings file.");
                }

                var game = GameFactory.CreateGame(settings.Board, settings.InitialPosition, settings.Exit, settings.Mines);
                int currentMove = 0;

                while (game.GameStatus != StatusEnum.Success 
                    && game.GameStatus != StatusEnum.Failure 
                    && currentMove < moveSequence.Moves.Count)
                {
                    if (IsValidMove(moveSequence.Moves[currentMove]))
                    {
                        if (IsRotate(moveSequence.Moves[currentMove]))
                        {
                            game.Rotate();
                            currentMove++;
                            continue;
                        }

                        if (IsGoFoward(moveSequence.Moves[currentMove]))
                        {
                            game.Move();
                            currentMove++;
                            continue;
                        }
                    }
                    currentMove++;
                }

                result.AppendLine($"Sequence {sequenceNumber}: {game.LastMoveResult}");
                sequenceNumber++;
            }
            return result.ToString();
        }

        private static bool IsValidMove(char move)
        {
            return move == 'M' || move == 'R' || move == 'm' || move == 'r';
        }

        private static bool IsRotate(char move)
        {
            return move == 'R' || move == 'r';
        }

        private static bool IsGoFoward(char move)
        {
            return move == 'M' || move == 'm';
        }
    }
}
