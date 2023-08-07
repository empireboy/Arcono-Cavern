using Engine;
using Microsoft.Xna.Framework;

namespace Arcono
{
    public class Spikes : SpriteGameObject
    {
        public override Rectangle BoundingBox
        {
            get
            {
                int threshold = 25;
                int left = (int)(GlobalPosition.X - origin.X + threshold);
                int top = (int)(GlobalPosition.Y - origin.Y + threshold);
                return new Rectangle(left, top, Width - threshold * 2, Height - threshold * 2);
            }
        }

        public Spikes(string assetName) : base(assetName)
        {

        }

        public Spikes() : base("spikes")
        {

        }

        public Spikes(int x, int y) : base(x, y, "spikes")
        {

        }
    }
}
