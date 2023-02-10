namespace TurtleGame.Console
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHost host;

        public Worker(ILogger<Worker> logger, IHost host)
        {
            _logger = logger;
            this.host = host;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Welcom to Turtle Maze");
            Console.WriteLine("Please specify the settings file location");
            var settingsPath = Console.ReadLine();

            Console.WriteLine("Please specify the moves file location");
            var movesPath = Console.ReadLine();
            Console.Clear();
            host.StopAsync();
        }
    }
}