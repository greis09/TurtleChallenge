namespace TurtleGame.Settings
{
    public struct Board
    {
        public Board(int height, int width)
        {
            Height = height;
            Width = width;
        }
        public int Height { get; set; }
        public int Width { get; set; }

        
    }
}
