using Arcono;
using Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Arcono
{
    class MovingPlatformVertical : Platform
    {
        protected float speed = 150;
        private bool isMovingVerically;
        
        public MovingPlatformVertical() : base()
        {
            isMovingVerically = false;
        }

        public override void Reset()
        {
            base.Reset();

            //the velocity doesnt reset with this
            if (!isMovingVerically)
            {
                velocity.Y = speed;
                isMovingVerically = true;
            }
        }
    }
}
