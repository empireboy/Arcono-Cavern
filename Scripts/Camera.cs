using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Microsoft.Xna.Framework;

namespace Arcono
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(SpriteGameObject target)
        {

            var position = Matrix.CreateTranslation(
                    GameEnvironment.ScreenWidth / 2,
                    GameEnvironment.ScreenHeight / 2,
                    0);


            var offset = Matrix.CreateTranslation(
                -target.position.X - (target.Sprite.Width / 2),
                -target.position.Y - (target.Sprite.Height / 2),
                0);

            Transform = position * offset;
        }
    }
}
