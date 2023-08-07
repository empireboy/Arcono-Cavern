using Arcono.Editor.Managers;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Arcono
{


    public class UnlockItemManager : GameObjectList
    {
        public int totalCoins = 0;
        private int unlockCost = 1;
        public int[] coins = new int[5];
        public int[] maxCoinPerLevel = new int[5];

        public bool[] item;
        private string[] allItems = Enum.GetNames(typeof(UnlockableItems));
        public bool canPlace;
        public bool unlockedItem = true;
        public bool check = false;
        private bool canChange;

        Texture2D displayMessage;

        // List of the items that can be unlocked
        public enum UnlockableItems
        {
            Door,
            Key,
            Frog,
            AttachPoint,
            GrapplingHookPart,
            InvisibleWall,
            MovingPlatformVertical,
            MovingPlatformHorizontal,
            FallingSpikes,
            CheckPoint,
            Torch1,
            Torch2,
            TorchOff,
            Rock,
            MushroomRed,
            MushroomBrown,
            PlantPurple,
            SignRight,
            SignLeft,
            BeamDiagonalLeft,
            BeamDiagonalRight,
            Beam,
            BeamBolts,
            BeamBoltsHoles,
            BeamBoltsNarrow,
            BeamHoles,
            BeamNarrow,
            DirtCaveBottomLeft,
            DirtCaveBottomRight,
            DirtCaveUpLeft,
            DirtCaveUpRight,
            DirtCaveBottom,
            DirtCaveTop,
            DirtCaveSpikeBottom,
            DirtCaveSpikeTop,
            Dirt,
            Heart,
        }

        public UnlockItemManager()
        {
            item = new bool[allItems.Length];

            displayMessage = GameEnvironment.AssetManager.Content.Load<Texture2D>("DisplayMessage");
        }

        public override void Reset()
        {
            base.Reset();
            check = false;
        }

        // This function will unlock the items
        public void UnlockItem()
        {
            for (int i = 0; i < item.Length; i++)
            {
                bool canSubtract = false;

                if (totalCoins >= unlockCost)
                {
                    item[i] = true;

                    if (allItems[i].ToString() == "Door")
                    {
                        item[i + 1] = true;
                        canSubtract = false;
                    }
                    else if (allItems[i].ToString() == "AttachPoint")
                    {
                        item[i + 1] = true;
                        canSubtract = false;
                    }
                    else canSubtract = true;

                    if (canSubtract)
                        totalCoins -= unlockCost;
                }
            }
        }

        // This function calculates the total coins collected by the player
        public void CalculateTotalCoinAmount()
        {
            for (int i = 0; i < coins.Length; i++)
            {
                totalCoins += coins[i];
            }

            UnlockItem();
        }


        // This function will check whether or not an item can be placed
        public void CheckItem(ItemTypes itemType)
        {
            canPlace = true;
            int index = 0;

            for (int i = 0; i < allItems.Length; i++)
            {
                if (itemType.ToString() == allItems[i])
                {
                    canPlace = false;
                    index = i;
                }
            }

            if (!canPlace)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    // Check if the item is unlocked with a certain index
                    if (item[index])
                    {
                        canPlace = true;
                    }
                    else canPlace = false;
                }
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.MouseLeftButtonDown())
            {
                if (check && canChange)
                {
                    check = false;
                    canChange = false;
                }
                else if (!check && canChange)
                {
                    check = true;
                    canChange = false;
                }
            }

            if (!inputHelper.MouseLeftButtonDown())
            {
                canChange = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            // Center the display message in the middle of the screen with camera movement
            Vector2 drawPosition = new Vector2(
                GameEnvironment.cameraMover.position.X - displayMessage.Width / 2,
                GameEnvironment.cameraMover.position.Y - displayMessage.Height / 2
            );

            if (!unlockedItem && check)
                spriteBatch.Draw(displayMessage, drawPosition, null, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
        }
    }
}
