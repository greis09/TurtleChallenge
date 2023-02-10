namespace TurtleGame.Settings
{
    public static class GameFactory
    {
        public static TurtleGame CreateGame(
            Board board,
            Position startingPosition,
            Location exitLocation,
            List<Location> minesLocations)
        {
            if (board.Height < 1 || board.Width < 1 || board.Height > 10000 || board.Width > 10000)
            {
                throw new ArgumentException("Board sizes must be more then 1 and less then 10000");
            }

            if (startingPosition == null)
            {
                throw new ArgumentException("Error loading the starting position, please verify the data.");
            }

            if (startingPosition.X < 1
                || startingPosition.Y < 1
                || startingPosition.X > board.Width
                || startingPosition.Y > board.Height)
            {
                throw new ArgumentException("Starting position must be inside board dimentions.");
            }

            if (exitLocation == null)
            {
                throw new ArgumentException("Error loading the exit location, please verify the data.");
            }

            if (exitLocation.X < 1 || exitLocation.Y > board.Height || exitLocation.X > board.Width)
            {
                throw new ArgumentException("Exit location must be inside board dimentions.");
            }

            if (minesLocations.Any())
            {
                var failedMines = minesLocations.Where(mine => mine.X < 1 || mine.Y < 1 || mine.X > board.Width || mine.Y > board.Height);
                if(failedMines.Any())
                {
                    throw new ArgumentException("Mines locations must be inside board dimentions");
                }

                if (minesLocations.Any(mine => mine.Equals(exitLocation)))
                {
                    throw new ArgumentException("Exit location must not be equal to a mine location");
                }

                if (minesLocations.Any(mine => mine.Equals(startingPosition)))
                {
                    throw new ArgumentException("Starting position must not be equal to a mine location");
                }
            }

            return new TurtleGame(board, startingPosition, exitLocation, minesLocations);
        }
    }
}
