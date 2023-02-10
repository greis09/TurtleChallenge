using System.Text;
using TurtleGame.Settings;

namespace TurtleGame
{
    public class TurtleGame
    {
        private Board board;

        private Position currentPosition;

        private Location exitLocation;

        private List<Location> minesLocations;

        public TurtleGame(
            Board board,
            Position startingPosition,
            Location exitLocation,
            List<Location> minesLocations)
        {
            this.board = board;
            this.exitLocation = exitLocation;
            this.minesLocations = minesLocations;
            currentPosition = startingPosition;
            GameStatus = StatusEnum.Started;
            LastMoveResult = "Turtle still in the maze!";
        }

        public StatusEnum GameStatus { get; private set; }

        public string LastMoveResult { get; private set; }

        public void Move()
        {
            switch (currentPosition.Direction)
            {
                case DirectionEnum.North:
                    if (currentPosition.Y == board.Height)
                    {
                        return;
                    }
                    currentPosition.Y++;
                    break;

                case DirectionEnum.East:
                    if (currentPosition.X == board.Width)
                    {
                        return;
                    }
                    currentPosition.X++;
                    break;

                case DirectionEnum.South:
                    if (currentPosition.Y == 1)
                    {
                        return;
                    }
                    currentPosition.Y--;
                    break;

                case DirectionEnum.West:
                    if (currentPosition.X == 1)
                    {
                        return;
                    }
                    currentPosition.X--;
                    break;
            }

            if (FoundTheExit())
            {
                GameStatus = StatusEnum.Success;
                LastMoveResult = "Success!! The turtle escaped!";
                return;
            }
            if (FoundAMine())
            {
                GameStatus = StatusEnum.Failure;
                LastMoveResult = "KABUMM!! The turtle blow up!!";
                return;
            }
            GameStatus = StatusEnum.Ongoing;
            LastMoveResult = "Turtle still in the maze!";
        }

        public void Rotate()
        {
            switch (currentPosition.Direction)
            {
                case DirectionEnum.North:
                    currentPosition.Direction = DirectionEnum.East;
                    GameStatus = StatusEnum.Ongoing;
                    break;

                case DirectionEnum.East:
                    currentPosition.Direction = DirectionEnum.South;
                    GameStatus = StatusEnum.Ongoing;
                    break;

                case DirectionEnum.South:
                    currentPosition.Direction = DirectionEnum.West;
                    GameStatus = StatusEnum.Ongoing;
                    break;

                case DirectionEnum.West:
                    currentPosition.Direction = DirectionEnum.North;
                    GameStatus = StatusEnum.Ongoing;
                    break;
            }
            LastMoveResult = "Turtle still in the maze!";
        }

        private bool FoundTheExit()
        {
            return currentPosition.Equals(exitLocation);
        }

        private bool FoundAMine()
        {
            return minesLocations.Any(mine => mine.Equals(currentPosition));
        }
    }
}
