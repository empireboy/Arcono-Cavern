using Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcono.Managers
{
	public class MovementManager : GameObject
	{
		private LivingGameObject _livingGameObject;
		private SpriteSheet faceLeft = new SpriteSheet("player_idle (Left)");
		private SpriteSheet faceRight = new SpriteSheet("player_idle (2)");


		public MovementManager(LivingGameObject livingGameObject) : base()
		{
			_livingGameObject = livingGameObject;
			
		}

		public void MoveLeft()
		{
			_livingGameObject.velocity.X = -_livingGameObject.moveSpeed.X;
			//changes player sprite
			_livingGameObject.Sprite = faceLeft;
			ChangeState();
		}

		public void MoveRight()
		{
			_livingGameObject.velocity.X = _livingGameObject.moveSpeed.X;
			//changes player sprite
			_livingGameObject.Sprite = faceRight;
			ChangeState();
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			if (!CanMove())
				return;

			if (inputHelper.IsKeyDown(Keys.A) || inputHelper.IsKeyDown(Keys.Left))
			{
				MoveLeft();
			}
			else if (inputHelper.IsKeyDown(Keys.D) || inputHelper.IsKeyDown(Keys.Right))
			{
				MoveRight();
			}
			else
			{
				StopMoving();
			}
		}

		public void StopMoving()
		{
			// Change back to Idle if the LivingGameObject is not moving
			if (_livingGameObject.State != LivingGameObject.States.Climbing)
				_livingGameObject.ChangeState(LivingGameObject.States.Idle);

			_livingGameObject.velocity.X = 0;
		}

		private bool CanMove()
		{
			// Checks if the LivingGameObject has the Walking state
			if (!_livingGameObject.HasState(LivingGameObject.States.Walking))
				return false;

			// Checks if the LivingGameObject is inside the Idle, Walking or Climbing state
			if (
				_livingGameObject.State != LivingGameObject.States.Idle &&
				_livingGameObject.State != LivingGameObject.States.Walking &&
				_livingGameObject.State != LivingGameObject.States.Climbing
			)
				return false;

			return true;
		}

		private void ChangeState()
		{
			if (_livingGameObject.State == LivingGameObject.States.Idle)
				_livingGameObject.ChangeState(LivingGameObject.States.Walking);
		}
	}
}
