using Microsoft.Extensions.DependencyInjection;
using TurtleGame.Adapters;

namespace TurtleChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var serviceProvider = new ServiceCollection()
             .AddSingleton<IFileManager, FileManager>()
             .AddSingleton<IFileAdapter, FileAdapter>()
             .BuildServiceProvider();

            var fileAdapter = serviceProvider.GetService<IFileAdapter>();

            try
            {
                Console.WriteLine("Welcom to Turtle Maze");
                Console.WriteLine("Please specify the settings file location");
                var settingsPath = Console.ReadLine();

                Console.WriteLine("Please specify the moves file location");
                var movesPath = Console.ReadLine();
                Console.Clear();

                var result = fileAdapter.RunGame(settingsPath, movesPath);
                Console.WriteLine(result);
                Console.WriteLine("");
                Console.WriteLine("Press anything to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}