using Engine;

namespace Arcono
{
	public class Platform : SpriteGameObject
    {
        public Platform() : base("stoneCenter")
        {

        }

        public Platform(int x, int y) : base(x, y, "stoneCenter")
        {

        }
    }
}