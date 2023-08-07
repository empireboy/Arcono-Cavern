using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcono
{
    class Checkpoint : SpriteGameObject
    {
        public bool isAchieved = false;
        public SpriteSheet textureFlagUp;

        private float volume;
        public Checkpoint() : base("flagGreen_down")
        {
            textureFlagUp = new SpriteSheet("flagGreen2");
            volume = 0.5f;
        }

        public override void Reset()
        {
            base.Reset();

            if (isResettable)
            {
                isAchieved = false;
            }
        }

        // This function will detect when an checkpoint is achieved
        public void AchieveCheckpoint() 
        {
            if(!isAchieved)
            {
                GameEnvironment.AssetManager.PlaySound("Checkpoint", volume);
            }

            isAchieved = true;
            sprite = textureFlagUp;
        }
    }
}
