using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arcono
{
    public class Key : SpriteGameObject
    {

        public Key() : base("keyBlue")
        {

        }

        public override void Reset()
        {
            base.Reset();
            if (isResettable)
            {
                isActive = true;
                Visible = true;
                PerPixelCollisionDetection = true;
            }
        }
    }
}
