using TurtleGame.Settings;

namespace TurtleGame.Adapters.DTO
{
    public class SettingsFileDTO
    {
        public Board Board { get; set; }

        public Position InitialPosition { get; set; }

        public Location Exit { get; set; }

        public List<Location> Mines { get; set; }
    }
}
