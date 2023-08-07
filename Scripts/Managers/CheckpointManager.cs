using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono
{
    class CheckpointManager : GameObjectList
    {
        Level level;

        Player player = null;
        List<Coin> coins = new List<Coin>();
        List<Key> keys = new List<Key>();
        List<Doors> doors = new List<Doors>();
        List<CollectibleGameObject> grapplingParts = new List<CollectibleGameObject>();
        List<Checkpoint> checkPoints = new List<Checkpoint>();

        bool hasCheckedItems = false;

        public CheckpointManager(Level level, List<Checkpoint> checkpoint)
        {
            this.level = level;
            checkPoints = checkpoint;

            // Add all objects that get "saved" when a checkpoint is achieved
            foreach (GameObject gameObject in this.level.Children)
            {
                if (gameObject is Player)
                {
                    player = gameObject as Player;
                    continue;
                }
                else if (gameObject is Doors)
                {
                    doors.Add(gameObject as Doors);
                    continue;
                }
                else if (gameObject is Coin)
                {
                    coins.Add(gameObject as Coin);
                    continue;
                }
                else if (gameObject is Key)
                {
                    keys.Add(gameObject as Key);
                    continue;
                }
                else if (gameObject is GrapplingHookPart)
                {
                    grapplingParts.Add(gameObject as GrapplingHookPart);
                    continue;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            HandleCollision();
        }

        // This functions checks which items are collected and which are not
        public void CheckCollectedItems()
        {
            foreach (Coin coin in coins)
            {
                if (!coin.isActive)
                {
                    coin.isResettable = false;
                }
                else
                    coin.isResettable = true;
            }

            foreach (Key key in keys)
            {
                if (!key.isActive)
                {
                    key.isResettable = false;
                }
                else
                    key.isResettable = true;
            }

            foreach (GrapplingHookPart part in grapplingParts)
            {
                if (!part.visible)
                {
                    part.isResettable = false;
                }
                else
                    part.isResettable = true;
            }

            foreach (Doors door in doors)
            {
                if (!door.isActive)
                {
                    door.isResettable = false;
                }
                else
                    door.isResettable = true;
            }

            foreach (Checkpoint checkpoint in checkPoints) 
            {
                if (checkpoint.isAchieved) 
                {
                    isResettable = true;
                }
            }

        }

        public void HandleCollision()
        {
            // Checks to see if player has collision with the checkpoint.
            foreach (Checkpoint checkPoint in checkPoints)
            {
                if (checkPoint.CollidesWith(player) && !hasCheckedItems)
                {

                    CheckCollectedItems();
                    player.startPosition = checkPoint.position;
                    hasCheckedItems = true;
                    checkPoint.AchieveCheckpoint();
                }
                else hasCheckedItems = false;
            }
        }
    }
}
