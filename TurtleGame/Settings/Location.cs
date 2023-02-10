namespace TurtleGame.Settings
{
    public class Location
    {
        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X;

        public int Y;

        public override bool Equals(object? obj)
        {
            return obj is Location location &&
                   X == location.X &&
                   Y == location.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
