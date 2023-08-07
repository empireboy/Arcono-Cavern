using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcono
{
	public class Player : LivingGameObject
	{
		public Vector2 LeftHandPosition => new Vector2(-28, 22);
		public Vector2 RightHandPosition => new Vector2(28, 22);

		

		public Player() : base("player_climb (1)")
		{
			Reset();
		}

		public Player(int x, int y) : base("player_climb (1)", x, y)
		{
			Reset();
		}

		public override void Reset()
		{
			AllStates = States.Idle | States.Walking | States.Climbing | States.Swinging;
			ChangeState(States.Idle);
			moveSpeed.X = 500;
			moveSpeed.Y = 175;
			gravity = 550;
			swingSpeed = 0.001f;
			velocity.Y = 200;
			attachHookRange = 360;
			position = startPosition;
			grapplingPartAmount = 0;
			hasGrapplingHook = false;
		}

		public override void Update(GameTime gameTime)
		{
			// Only update position if the player isn't climbing or swinging
			if (State != States.Swinging)
			{ 
				base.Update(gameTime);
            }

			GetGrapplingHook();
		}

		public void GetGrapplingHook()
		{
			// Get grappling hook after collecting all parts
			if (grapplingPartAmount >= 2)
			{
				hasGrapplingHook = true;
				grapplingPartAmount = 0;
			}
		}
	}	
}
