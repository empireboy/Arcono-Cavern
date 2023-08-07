
namespace Arcono
{
    public class RightSpikes : Spikes
    {
        public RightSpikes() : base("spikesRightWall")
        {
            
        }

        public RightSpikes(int x, int y) : base( "spikesRightWall")
        {
            position.X = x;
            position.Y = y;
        }
    }
}