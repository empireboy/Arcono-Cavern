using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Engine;
using Microsoft.Xna.Framework;

namespace Arcono
{
    class ExplanationBox : SpriteGameObject
    {
        
        public ExplanationBox() : base("dialogue-Box(new)")
        {
           
            position.Y += 128;
            position.X -= 256;
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            isActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
           
        }
    }
}
