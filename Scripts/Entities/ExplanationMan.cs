using System;
using System.Collections.Generic;
using System.Text;
using Engine;
using Microsoft.Xna.Framework;

namespace Arcono
{
    class ExplanationMan : SpriteGameObject
    {
        public int explanation;
       
        public ExplanationMan() : base("explanationMan-sprite")
        {
            explanation = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

      
    }
}
