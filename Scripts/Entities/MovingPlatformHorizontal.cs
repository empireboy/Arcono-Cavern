using Arcono;
using Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Arcono
{
    class MovingPlatformHorizontal : Platform
    {
        protected float speed = 150;
        private bool isMovingHorizontally;

        public MovingPlatformHorizontal() : base()
        {
            isMovingHorizontally = false;
        }
    
        public override void Reset()
        {
            base.Reset();

            //the velocity doesnt reset with this
            if (!isMovingHorizontally)
            {
                velocity.X = speed;
                isMovingHorizontally = true;
            }
        }
    }
}
