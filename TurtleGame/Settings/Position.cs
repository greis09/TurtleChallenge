namespace TurtleGame.Settings
{
    public class Position : Location
    {
        public Position(int x, int y, char direction) :
            base(x, y)
        {
            if (direction != 'N'
                && direction != 'S'
                && direction != 'E'
                && direction != 'W')
            {
                throw new ArgumentException("Direction must be either 'N', 'E', 'S' or 'W'");
            }
            this.Direction = (DirectionEnum)direction;
        }
        public DirectionEnum Direction;
    }
}
