using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcono
{
	public class Rope : SpriteGameObject
	{
		public float RopeWidth { get; set; }
		public float Radius { get; set; }
		public float MaxRadius { get; set; }
		public float RadiusIncreaseValue { get; set; }
		public float MaxVelocity { get; set; }
		public float AngleVelocity { get; private set; }
		public float DownwardsMomentum { get; set; }

		public float AngleInRadians
		{
			get { return angle; }
			set { angle = value; }
		}

		public float AngleInDegrees
		{
			get { return MathHelper.ToDegrees(angle); }
			set { angle = MathHelper.ToRadians(value); }
		}

		private float angle;

		private Player hangingObject;
		private Vector2 hookPosition;

		public Rope() : base("spr_rope")
		{
			Reset();
		}

		public void Attach(Player hangingObject, Vector2 hookPosition)
		{
			this.hangingObject = hangingObject;
			this.hookPosition = hookPosition;
			position = hookPosition;

			// Set angle to the angle between the player and hook positions
			angle = Vector2Helper.GetAngle(this.hookPosition, this.hangingObject.position);

			// Set the length of the rope to the distance between the player and hook position
			Radius = Vector2.Distance(this.hangingObject.position, this.hookPosition) - 40;
		}

		public void SwingLeft(float speed)
		{
			AngleVelocity += speed;
		}

		public void SwingRight(float speed)
		{
			AngleVelocity -= speed;
		}

		public override void Update(GameTime gameTime)
		{
			if (!isActive)
				return;

			if (hangingObject == null)
				return;

			hangingObject.position = GetRopePosition() - hangingObject.RightHandPosition;

			// Increase the radius of the rope, so that you will always get the max rope length
			if (Radius < MaxRadius)
			{
				Radius += RadiusIncreaseValue;
			}
			else
				Radius = MaxRadius;

			// Left side downwards momentum
			if (AngleInDegrees < 270 && AngleInDegrees > 90)
				AngleVelocity -= DownwardsMomentum;
			// Right side downwards momentum
			else if ((AngleInDegrees > 270 && AngleInDegrees <= 360) || (AngleInDegrees >= 0 && AngleInDegrees < 90))
				AngleVelocity += DownwardsMomentum;

			// Clamp angle between 0 and 360 degrees
			AngleInDegrees = CM_MathHelper.ClampAngleBetween360Degrees(AngleInDegrees);

			// Clamp velocity, so you don't swing to fast
			AngleVelocity = Math.Clamp(AngleVelocity, -MaxVelocity, MaxVelocity);

			angle += AngleVelocity;
		}

		public override void Reset()
		{
			RopeWidth = 0.5f;
			MaxVelocity = 0.055f;
			MaxRadius = 250f;
			RadiusIncreaseValue = 3f;
			DownwardsMomentum = 0.001f;
			angle = 0;
			AngleVelocity = 0;
			hangingObject = null;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (!isActive)
				return;

			if (hangingObject == null)
				return;

			// Draws the rope between the player and hook positions
			spriteBatch.Draw(
				sprite.Sprite,
				position,
				null,
				Color.White,
				rotation: Vector2Helper.GetAngle(hookPosition, GetRopePosition()),
				origin: new Vector2(0, HalfTextureHeight),
				scale: new Vector2(Vector2.Distance(GetRopePosition(), hookPosition) / sprite.Sprite.Width, RopeWidth),
				SpriteEffects.None,
				layerDepth: 0
			);
		}

		private Vector2 GetRopePosition()
		{
			// Calculates the end point position of the rope
			Vector2 position = new Vector2(
				hookPosition.X + Radius * (float)Math.Cos(angle),
				hookPosition.Y + Radius * (float)Math.Sin(angle)
			);

			return position;
		}
	}
}
