using Microsoft.Xna.Framework;

namespace Arcono
{
    public class Frog : LivingGameObject
    {
        public float jumpTime;
        public float jumpTimer;
        public float gravityTimer;
        public float gravityTime;
        public int timesJumped;
        public bool flipped;

        public override Rectangle BoundingBox
        {
            get
            {
                int threshold = 60;
                int left = (int)(GlobalPosition.X - origin.X + threshold);
                int top = (int)(GlobalPosition.Y - origin.Y + threshold);
                return new Rectangle(left, top, Width - threshold * 2, Height - threshold);
            }
        }

        public Frog() : base("Frog")
        {
            Reset();
        }

        public Frog(int x, int y) : base("Frog", x, y)
        {
            Reset();
        }

        public override void Reset()
        {
            AllStates = States.Idle | States.Jumping;
            ChangeState(States.Idle);
            jumpTime = 4;
            jumpTimer = jumpTime;
            gravity = 340;
            velocity.X = 0;
            velocity.Y = gravity;
            moveSpeed.X = 0.88f;
            position = startPosition;
            gravityTime = 1.5f;
            gravityTimer = gravityTime;
            timesJumped = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            jumpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
           
        }
    }
}
