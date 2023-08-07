
namespace Arcono
{
    public class RoofSpikes : Spikes
    {
        public RoofSpikes() : base("spr_spikes_roof")
        {
            
        }

        public RoofSpikes(int x, int y) : base( "spr_spikes_roof")
        {
            position.X = x;
            position.Y = y;
        }
    }
}
