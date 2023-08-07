using Engine;
using Microsoft.Xna.Framework;
using System;

namespace Arcono
{
    public class Coin : SpriteGameObject
    {
        public Coin() : base("coinGold")
        {
            Reset();
        }

        public Coin(int x, int y) : base(x, y, "coinGold")
        {
            Reset();
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

            Scale = (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds * 0.005f) * 0.1f + 1;
        }

		public override void Reset()
        {
            base.Reset();

            Origin = Center;

            if (isResettable)
            {
                isActive = true;
                PerPixelCollisionDetection = true;
            }
        }
    }
}
