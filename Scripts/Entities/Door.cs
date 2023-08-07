using Microsoft.Xna.Framework.Graphics;
using Engine;
using Microsoft.Xna.Framework;

namespace Arcono
{
	public class Doors : SpriteGameObject
    {
        public bool doorIsOpen;

        public override Rectangle BoundingBox
        {
            get
            {
                int threshold = -1;
                int left = (int)(GlobalPosition.X - origin.X + threshold);
                int top = (int)(GlobalPosition.Y - origin.Y);
                return new Rectangle(left, top, Width - threshold * 2, Height);
            }
        }

        public Doors() : base("lockBlue")
        {
            
        }

        public override void Reset()
        {
            base.Reset();
            if (isResettable)
            {
                isActive = true;
                visible = true;
            }
        }
    }
}
