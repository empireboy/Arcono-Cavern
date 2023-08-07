using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Arcono
{
    public class CameraMover : SpriteGameObject
    {
        public Vector2 speed = new Vector2(800, 800);
        public bool IsLevelEditor, IsMenu;

        Player player;
        public CameraMover(Player player) : base("bridgeB")
        {
            this.player = player;

            IsLevelEditor = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsLevelEditor && !IsMenu)
            {
                position = player.position;
            }

            if (position.X < GameEnvironment.ScreenWidth / 2 - 64)
            {
                position.X = GameEnvironment.ScreenWidth / 2 - 64;
            }

            if (position.Y < GameEnvironment.ScreenHeight / 2 - 64)
            {
                position.Y = GameEnvironment.ScreenHeight / 2 - 64;
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (IsMenu)
                return;

            if (IsLevelEditor)
            {
                if (inputHelper.IsKeyDown(Keys.W))
                    velocity.Y = -speed.Y;
                else if (inputHelper.IsKeyDown(Keys.S))
                    velocity.Y = speed.Y;
                else
                    velocity.Y = 0;

                if (inputHelper.IsKeyDown(Keys.A))
                    velocity.X = -speed.X;
                else if (inputHelper.IsKeyDown(Keys.D))
                    velocity.X = speed.X;
                else
                    velocity.X = 0;
            }
        }
    }
}
