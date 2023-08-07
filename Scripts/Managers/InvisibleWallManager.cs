using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono.Managers
{
	class InvisibleWallManager : GameObject
    {
        private Player player;
        private List<InvisibleWall> invisibleWalls;

        public InvisibleWallManager(Player player, List<InvisibleWall> invisibleWalls) : base()
        {
            this.player = player;
            this.invisibleWalls = invisibleWalls;
        }

        public override void Update(GameTime gametime)
        {
            foreach (InvisibleWall invisibleWall in invisibleWalls)
            {
                // Detect whether the player is inside of the invisible wall or not
                if (invisibleWall.CollidesWith(player))
                {
                    invisibleWall.isColliding = true;
                }
                else invisibleWall.isColliding = false;
            }
        }
    }
}
