using Engine;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arcono
{
    public class CoinManager : GameObject
    {
        public Player player;
        public List<Coin> coins;
        public CameraMover camera;
        public float grabbedCoins;

        private SpriteFont font;
        private Texture2D texture;

        private float volume;
        private float coinCounterXOffSet;
        private float coinCounterYOffSet;

        public CoinManager(Player player,  List<Coin> coins, CameraMover camera) : base()
        {
            this.player = player;
            this.coins = coins;
            this.camera = camera;

            font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("fnt_text");
            texture = GameEnvironment.AssetManager.Content.Load<Texture2D>("coinGold");

            volume = 0.1f;
            coinCounterXOffSet = 110;
            coinCounterYOffSet = 50;
            Reset();
        }

        public CoinManager() 
        {
        }

        public override void Reset()
        {
            base.Reset();
            grabbedCoins = 0;

            foreach (Coin coin in coins) 
            {
                if (!coin.isResettable) 
                {
                    grabbedCoins++;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            //Positioning for the coincounter
            position.X = camera.position.X - ArconoEnvironment.ScreenWidth / 2 + 64;
            position.Y = camera.position.Y - ArconoEnvironment.ScreenHeight / 2 + 64;

            foreach (Coin coin in coins)
            {
                if (!coin.isActive)
                    continue;

                //Coin player collision
                if (coin.CollidesWith(player))
                {
                    coin.isActive = false; 
                    grabbedCoins += 1;
                    GameEnvironment.AssetManager.PlaySound("Collect_Coin_1_1", volume);

                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            //Drawing of the coin counter 
            spriteBatch.DrawString(font, grabbedCoins + " / " + coins.Count, new Vector2(position.X + coinCounterXOffSet, position.Y + coinCounterYOffSet), Color.White);
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}