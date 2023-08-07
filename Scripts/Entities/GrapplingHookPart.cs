using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcono
{
	public class GrapplingHookPart : CollectibleGameObject
    {
        public GrapplingHookPart() : base("spr_grappling_part")
        {
            Reset();
        }

        public override void Reset()
        {
            base.Reset();

            if (isResettable)
            {
                isCollected = false;
                visible = true;
            }
        }

        public override void CollectObject(Player player)
        {
            base.CollectObject(player);

            // Increase the part amount collected by the player.
            player.grapplingPartAmount++;
        }
    }
}
