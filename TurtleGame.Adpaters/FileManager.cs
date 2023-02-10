using Newtonsoft.Json;

namespace TurtleGame.Adapters
{
    public class FileManager : IFileManager
    {
        public T ReadFile<T>(string path)
        {
            try
            {
                using (var stream = new StreamReader(path))
                {
                    var text = stream.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(text);
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
