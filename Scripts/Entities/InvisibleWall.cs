using Arcono.Editor;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arcono
{
    public class InvisibleWall : Platform
    {
        public bool isColliding;

        public InvisibleWall(int posX, int posY) : base(posX, posY) 
        {
            position.X = posX;
            position.Y = posY;

            Reset();
        }

        public InvisibleWall() : base()
        {
            Reset();
        }

		public override void Reset()
		{
			base.Reset();

            if (GameEnvironment.currentGameState is LevelEditor)
                sprite = new SpriteSheet("stoneCenterFake");
            else
                sprite = new SpriteSheet("stoneCenter");
        }

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the invisible wall when the player is outside of it
            if (!isColliding)
            {
                base.Draw(gameTime, spriteBatch);
            }
        }
    }
}
