using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arcono
{
    class CollectibleManager : GameObject
    {
        public Player player;
        public List<CollectibleGameObject> collectableItems;
        public CameraMover camera;
        public List<Coin> coins;
        public float grabbedParts;

        private readonly SpriteFont font;
        private readonly Texture2D grapplingPartPlaceTexture;
        private readonly Texture2D grapplingPartTexture;
        private readonly Texture2D grapplingPartTexture2;
        private readonly Texture2D grapplingHookTexture;

        private int CoinOffset;

        private float volume;

        public CollectibleManager(Player player, List<CollectibleGameObject> collectableItem, CameraMover camera, List<Coin> coins)
        {
            this.player = player;
            this.collectableItems = collectableItem;
            this.camera = camera;
            this.coins = coins;
            font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("fnt_text");
            grapplingPartPlaceTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("spr_grappling_part_ui");
            grapplingPartTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("gear");
            grapplingPartTexture2 = GameEnvironment.AssetManager.Content.Load<Texture2D>("spr_grappling_part");
            grapplingHookTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("wireBottomLeft");

            volume = 0.1f;
            Reset();
        }

        public override void Reset()
        {
            base.Reset();
            grabbedParts = 0;
            CoinOffset = 0;

            foreach (GrapplingHookPart part in collectableItems)
            {
                if (!part.isResettable)
                {
                    player.grapplingPartAmount++;
                    grabbedParts++;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Positioning for the grappling hook counter
            if (coins.Count >= 10)
                CoinOffset = 10;

            position.X = camera.position.X - ArconoEnvironment.ScreenWidth / 2 + 322 + CoinOffset;
            position.Y = camera.position.Y - ArconoEnvironment.ScreenHeight / 2 + 84;

            if (visible)
            {
                base.Update(gameTime);

                CollectItem();
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            //Drawing of the grappling hook counter 
            spriteBatch.DrawString(font, grabbedParts + " / 2", new Vector2(position.X + 110, position.Y + 30), Color.White);
            GetGrapplingHook(spriteBatch);
        }

        public void CollectItem()
        {
            // Reset player position if spike hit
            foreach (CollectibleGameObject collectableItem in collectableItems)
            {
                if (collectableItem.CollidesWith(player))
                {
                    collectableItem.CollectObject(player);
                    grabbedParts += 1;
                    GameEnvironment.AssetManager.PlaySound("GrapplingPart_2_1", volume);
                } 
            }
        }

        public void GetGrapplingHook(SpriteBatch spriteBatch)
        {
            // Get grappling hook icon in HUD after collecting all parts
            if (grabbedParts < 2)
            {
                //The transparant icons when you don't have a part
                spriteBatch.Draw(grapplingPartPlaceTexture, new Vector2(position.X + 5, position.Y), null, Color.White, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 1.0f);
            }
            //the icons when you pick up parts
            if (grabbedParts >= 1)
            {
                spriteBatch.Draw(grapplingPartTexture, new Vector2(position.X + 35, position.Y + 28), null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
            }
            if (grabbedParts >= 2)
            {
                spriteBatch.Draw(grapplingPartTexture2, new Vector2(position.X + 5, position.Y), null, Color.White, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 1.0f);
            }
            if (grabbedParts >= 2)
            {
                spriteBatch.Draw(grapplingHookTexture, new Vector2(position.X + 160, position.Y), null, Color.White, 0, Vector2.Zero, 1.4f, SpriteEffects.None, 1.0f);
            }
        }
    }
}
