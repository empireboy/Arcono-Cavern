using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcono
{
	public abstract class LivingGameObject : SpriteGameObject
    {
		public States AllStates { get; protected set; }
		public States State { get; protected set; }

        public Vector2 moveSpeed;
		public float gravity;

		public float swingSpeed;
		public float attachHookRange;
		public int grapplingPartAmount;
		public bool hasGrapplingHook;

#if DEBUG
		private readonly SpriteFont _textFont;
		protected Vector2 debugTextOffset;
#endif

		public LivingGameObject(String assetName) : base(assetName)
        {
#if DEBUG
			_textFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("Font");
			debugTextOffset = new Vector2(0, -20);
#endif
		}

		public LivingGameObject(String assetName, int x, int y) : base(x, y, assetName)
		{
#if DEBUG
			_textFont = GameEnvironment.AssetManager.Content.Load<SpriteFont>("fnt_text");
			debugTextOffset = new Vector2(0, -20);
#endif
		}

		#region State Methods

		public void ChangeState(States state)
		{
			// Check if this LivingGameObject is able to change to this state
			if (!AllStates.HasFlag(state))
				throw new Exception(this + " is not allowed to change his state to " + state + ". You might want to add this state using the AddState method.");

			State = state;
		}

		public void AddState(States state)
		{
			AllStates |= state;
		}

		public void RemoveState(States state)
		{
			AllStates &= ~state;
		}
		
		public bool HasState(States state)
		{
			return AllStates.HasFlag(state);
		}

		#endregion

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

#if DEBUG
			// Draws the current state
			spriteBatch.DrawString(_textFont, State.ToString(), position + debugTextOffset, Color.White);
#endif
		}

		[Flags]
		public enum States
		{
			None = 0,
			Idle = 1,
			Walking = 2,
			Climbing = 4,
			Swinging = 8,
			Jumping = 16
		}
	}
}
