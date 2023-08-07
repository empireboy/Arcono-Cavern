using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Arcono
{
    public class DoorManager : GameObject
    {
        public Player player;
        public List<Doors> doors;
        public List<Key> keys;
        public List<Coin> coins;
        public CameraMover camera;
        public Texture2D texture1, texture2;
        public int grabbedKeys;

        private int CoinOffSet;
        private float volume;

        public DoorManager(Player player, List<Doors> doors, List<Key> keys, CameraMover camera, List<Coin> coins)
        {
            this.player = player;
            this.doors = doors;
            this.keys = keys;
            this.camera = camera;
            this.coins = coins;

            
            texture1 = GameEnvironment.AssetManager.Content.Load<Texture2D>("hudKey_blue_empty");
            texture2 = GameEnvironment.AssetManager.Content.Load<Texture2D>("hudKey_blue");

            grabbedKeys = 0;
            CoinOffSet = 0;

            volume = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            HandleCollision(player, doors, keys);

            //If there are more then 10 coins in a level move the position a bit to the right
            if (coins.Count >= 10)
                CoinOffSet = 10;

            position = new Vector2(camera.position.X - ArconoEnvironment.ScreenWidth / 2 + 225 + CoinOffSet, camera.position.Y - ArconoEnvironment.ScreenHeight / 2 + 75);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(texture1, position, null, Color.White, 0, Vector2.Zero, 1.6f, SpriteEffects.None, 1);

            if (grabbedKeys >= 1)
            {
                spriteBatch.Draw(texture2, position, null, Color.White, 0, Vector2.Zero, 1.6f, SpriteEffects.None, 1);
            }
        }

        public override void Reset()
        {
            base.Reset();
            grabbedKeys = 0;

            //checks if a key is resetable
            foreach (Key key in keys)
            {
                if (!key.isResettable)
                {
                    grabbedKeys++;
                }
            }

            //resets the amount of keys you have when you die
            foreach (Doors door in doors)
            {
                if (!door.isResettable)
                {
                    grabbedKeys--;

                    if (grabbedKeys < 0)
                    {
                        grabbedKeys = 0;
                    }
                }
            }
        }

        public void HandleCollision(Player player, List<Doors> doors, List<Key> keys)
        {
            // Checks to see if player has a key and collision with the door
            foreach (Doors door in doors)
            {
                    if (door.CollidesWith(player) && grabbedKeys >= 1)
                    {
                        grabbedKeys -= 1;
                        door.Visible = false;
                        door.isActive = false;
                        GameEnvironment.AssetManager.PlaySound("DoorOpen", volume);
                    }
            }

            // Checks for collision with the key
            foreach (Doors door in doors)
            {
                foreach (Key key in keys)
                {
                    if (key.CollidesWith(player))
                    {
                        GameEnvironment.AssetManager.PlaySound("Key_1_1", volume);
                        grabbedKeys += 1;
                        key.Visible = false;
                        key.isActive = false;
                    }
                }
            }
        }
    }
}
