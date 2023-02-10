namespace TurtleGame.Adapters.DTO
{
    public class MovesFileDTO
    {
        public List<MoveSequence> MovesSequences { get; set; }
    }

    public class MoveSequence
    {
        public List<char> Moves { get; set; }
    }

}
