
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcono
{
    public class FallingSpikes : Spikes
    {
        private readonly float spikeIdleTime;
        private float spikeIdleTimer;
        private readonly Texture2D spikeRespawn;

        private float volume = 0.1f;

        public FallingSpikes() : base("spr_spikes_roof")
        {
            spikeIdleTime = 2;
            spikeIdleTimer = spikeIdleTime;
            spikeTimer = false;
            spikeRespawn = GameEnvironment.AssetManager.Content.Load<Texture2D>("spr_spikes_respawn");
        }

        public override void Reset()
        {
            base.Reset();
            position = startPosition;
            velocity.Y = 0;
            spikeIdleTimer = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if the spike is at its end spot, reset it to the start position after # seconds
            if (spikeTimer == true)
            {
                spikeIdleTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (spikeIdleTimer < 0.02 && spikeIdleTimer > 0)
            {
                GameEnvironment.AssetManager.PlaySound("SpikeRespawn", volume);
            }
            if (spikeIdleTimer <= 0)
            {
                Reset();
                spikeIdleTimer = spikeIdleTime;
                spikeTimer = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            //blinking of the fallen spike
            if (spikeIdleTimer < 1 && spikeIdleTimer > 0.9)
            {
                spriteBatch.Draw(spikeRespawn, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
            if (spikeIdleTimer < 0.6 && spikeIdleTimer > 0.5)
            {
                spriteBatch.Draw(spikeRespawn, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
            if (spikeIdleTimer < 0.3 && spikeIdleTimer > 0.2)
            {
                spriteBatch.Draw(spikeRespawn, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
            if (spikeIdleTimer < 0.1 && spikeIdleTimer > 0)
            {
                spriteBatch.Draw(spikeRespawn, position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }
        }
    }
}