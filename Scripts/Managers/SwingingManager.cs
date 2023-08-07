using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Arcono.Managers
{
    public class SwingingManager : GameObjectList
    {
        private LivingGameObject _livingGameObject;

        private List<AttachPoint> _attachPoints;
        private Rope _rope;

        private SpriteSheet swinging = new SpriteSheet("Player_swing");
        private SpriteSheet standing = new SpriteSheet("player_climb (1)");

        private AttachPoint closestAttachPoint;
        private float closestAttachPointDistance;

        private readonly Texture2D canUseAttachPointTexture;

        private float volume;

        public SwingingManager(LivingGameObject livingGameObject, Rope rope, List<AttachPoint> attachPoints) : base()
        {
            canUseAttachPointTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("circle");

            _livingGameObject = livingGameObject;
            _rope = rope;
            _attachPoints = attachPoints;

            volume = 0.15f;

            Add(_rope);
        }

        public void SwingLeft()
        {
            if (!IsSwinging())
                return;

            // Left side
            if (_rope.AngleInDegrees < 270 && _rope.AngleInDegrees > 90)
                return;

            _rope.SwingLeft(_livingGameObject.swingSpeed);
        }

        public void SwingRight()
        {
            if (!IsSwinging())
                return;

            // Right side
            if (
                (_rope.AngleInDegrees > 270 && _rope.AngleInDegrees <= 360) ||
                (_rope.AngleInDegrees >= 0 && _rope.AngleInDegrees < 90)
            )
                return;

            _rope.SwingRight(_livingGameObject.swingSpeed);
        }

		public void AttachHook()
		{
			if (!CanSwing())
				return;

			if (_rope.isActive)
				return;

            // Find closest attach point
            AttachPoint closestAttachPoint = null;
            float closestRange = -1;

            foreach (AttachPoint attachPoint in _attachPoints)
            {
                float distance = Vector2.Distance(attachPoint.position, _livingGameObject.position);

                if (distance < closestRange || closestRange == -1)
                {
                    closestRange = distance;
                    closestAttachPoint = attachPoint;
                }
            }

            // Attach closest AttachPoint if in range
            if (closestRange <= _livingGameObject.attachHookRange)
            {
                GameEnvironment.AssetManager.PlaySound("PlayerAttachPoint", volume);

                _rope.Reset();
                _rope.isActive = true;

                Vector2 attachPosition = closestAttachPoint.position;

                _rope.Attach(_livingGameObject as Player, attachPosition);

                ChangeStateToSwinging();
            }
        }

        public void DetachHook()
        {
            if (!_rope.isActive)
                return;

            _rope.isActive = false;

            ChangeStateToIdle();
        }

        public AttachPoint GetClosestAttachPoint(out float distance)
		{
            // Find closest attach point
            AttachPoint closestAttachPoint = null;
            float closestRange = -1;
            distance = closestRange;

            foreach (AttachPoint attachPoint in _attachPoints)
            {
                distance = Vector2.Distance(attachPoint.position, _livingGameObject.position);

                if (distance < closestRange || closestRange == -1)
                {
                    closestRange = distance;
                    closestAttachPoint = attachPoint;
                }
            }

            distance = closestRange;

            return closestAttachPoint;
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

            closestAttachPoint = GetClosestAttachPoint(out closestAttachPointDistance);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            Color drawColor;

            if (closestAttachPoint != null)
			{
                Vector2 drawOffset = Vector2.Zero;
                drawColor = Color.LightGray;

                if (_livingGameObject.hasGrapplingHook)
				{
                    // Smoothly move the attach point visual around the attach point
                    drawOffset = new Vector2(
                        (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 10f,
                        (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2f) * 10f
                    );

                    if (closestAttachPointDistance <= _livingGameObject.attachHookRange)
                        drawColor = Color.Aqua;
                }

                spriteBatch.Draw(canUseAttachPointTexture, closestAttachPoint.position - new Vector2(canUseAttachPointTexture.Width / 2, canUseAttachPointTexture.Height / 2) + drawOffset, drawColor);
			}
        }

        public override void HandleInput(InputHelper inputHelper)
		{
			if (inputHelper.IsKeyDown(Keys.A) || inputHelper.IsKeyDown(Keys.Left))
			{
				SwingLeft();
			}
			else if (inputHelper.IsKeyDown(Keys.D) || inputHelper.IsKeyDown(Keys.Right))
			{
				SwingRight();
			}

            if (inputHelper.IsKeyDown(Keys.Space))
            {
                AttachHook();
            }
            else
            {
                DetachHook();
            }
        }

        private bool CanSwing()
        {
            // Checks if the LivingGameObject has the Walking state
            if (!_livingGameObject.HasState(LivingGameObject.States.Swinging))
                return false;

            // Checks if the LivingGameObject is inside the Idle or Walking state
            if (
                _livingGameObject.State != LivingGameObject.States.Idle &&
                _livingGameObject.State != LivingGameObject.States.Walking
            )
                return false;

            if (!_livingGameObject.hasGrapplingHook)
                return false;

            return true;
        }

        private bool IsSwinging()
        {
            return _livingGameObject.State == LivingGameObject.States.Swinging;
        }

        private void ChangeStateToSwinging()
        {
            if (
                _livingGameObject.State == LivingGameObject.States.Idle ||
                _livingGameObject.State == LivingGameObject.States.Walking
            )
                _livingGameObject.ChangeState(LivingGameObject.States.Swinging);
                _livingGameObject.Sprite = swinging;
        }

        private void ChangeStateToIdle()
        {
            if (_livingGameObject.State == LivingGameObject.States.Swinging)
                _livingGameObject.ChangeState(LivingGameObject.States.Idle);
                _livingGameObject.Sprite = standing;
        }
    }
}
