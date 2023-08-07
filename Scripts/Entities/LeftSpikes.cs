
namespace Arcono
{
    public class LeftSpikes : Spikes
    {
        public LeftSpikes() : base("spikesLeftWall")
        {
            
        }

        public LeftSpikes(int x, int y) : base("spikesLeftWall")
        {
            position.X = x;
            position.Y = y;
        }
    }
}

