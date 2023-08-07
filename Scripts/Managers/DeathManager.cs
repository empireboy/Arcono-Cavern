using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono
{
    public class DeathManager : GameObject
    {
        private SpriteGameObject dyingObject;
        private List<SpriteGameObject> killingObjects;
        private List<SpriteGameObject> collisionObjects;
        private GameObjectList gameState;

        private readonly float fallingSpeed;
        private readonly int activationProximity;

        private float volume;


       
        public DeathManager(SpriteGameObject dyingObject, List<SpriteGameObject> killingObjects, List<SpriteGameObject> collisionObjects, GameObjectList gameState)
        {
            this.dyingObject = dyingObject;
            this.killingObjects = killingObjects;
            this.collisionObjects = collisionObjects;
            this.gameState = gameState;
            fallingSpeed = 200;
            activationProximity = 1000;

            volume = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            KillObject();
        }

        private void KillObject()
        {
            // Reset dyingObject if hit by a killingObject
            foreach (SpriteGameObject killingObject in killingObjects)
            {
                if (killingObject.CollidesWith(dyingObject))
                {
                    gameState.Reset();
                    GameEnvironment.AssetManager.PlaySound("Death_1_1", volume);
                }

                if (killingObject is FallingSpikes)
                {
                    //collision check of the falling spikes, they will start to fall when there is collision
                    if (Vector2.Distance(killingObject.position, dyingObject.position) < activationProximity) {
                        if (dyingObject.position.X + dyingObject.Sprite.Width > killingObject.position.X &&
                            dyingObject.position.X < killingObject.position.X + killingObject.Sprite.Width &&
                            dyingObject.position.Y > killingObject.position.Y)
                        {
                            killingObject.velocity.Y = fallingSpeed;
                        }
                    }

                    foreach (SpriteGameObject collisionObjects in collisionObjects)
                    {
                        //if the spike is at its end spot, reset it to the start position after # seconds
                        if (killingObject.CollidesWith(collisionObjects))
                        {
                            killingObject.velocity.Y = 0;
                            killingObject.spikeTimer = true;
                        }
                    }
                }
            }
        }
    }
}
