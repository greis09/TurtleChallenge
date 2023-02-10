namespace TurtleGame.Adapters
{
    public interface IFileAdapter
    {
        public string RunGame(string pathToSettings, string pathToMoves);
    }
}
