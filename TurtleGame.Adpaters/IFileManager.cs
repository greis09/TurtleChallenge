namespace TurtleGame.Adapters
{
    public interface IFileManager
    {
        T ReadFile<T>(string path);
    }
}
