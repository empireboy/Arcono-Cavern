using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcono
{
	public class AttachPoint : SpriteGameObject
	{
		private Texture2D canUseTexture;

		public AttachPoint() : base("spr_attach_point")
		{
			canUseTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("circle");

			Reset();
		}

		public AttachPoint(int x, int y) : base(x, y, "spr_attach_point")
		{
			canUseTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("circle");

			Reset();
		}

		public override void Reset()
		{
			base.Reset();

			origin = Center;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			scale = (float)Math.Sin((float)gameTime.TotalGameTime.TotalMilliseconds * 0.005f) * 0.3f + 1;
		}
	}
}
