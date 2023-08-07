using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono
{
    public class FrogMoveManager : GameObjectList
    {
        private List<Frog> frogs;
        private SpriteSheet jumping = new SpriteSheet("frog_move");
        private SpriteSheet idle = new SpriteSheet("frog");
        private SpriteSheet idle_left = new SpriteSheet("frog (Left)");
        private SpriteSheet jumping_left = new SpriteSheet("frog_move (Left)");
        private Player player;
        public float proximityDistance;
        private float frogJumpStartVelocity = -50;
        private float frogJumpContinueVelocity = 80;

        private float volume;

        public FrogMoveManager(List<Frog> frogs, Player player) : base()
        {
            this.frogs = frogs;
            this.player = player;

            volume = 0.4f;
            Reset();
        }

        public override void Reset()
        {
            proximityDistance = 2000;
            base.Reset();
            foreach (Frog frog in frogs)
            {
                frog.Sprite = idle;
                frog.flipped = false;
            }
                
        }
        public override void Update(GameTime gameTime)
        {
            foreach(Frog frog in frogs)
            {
                if(frog is Frog)
                {
                         //Start of jumping
                        if (frog.jumpTimer <= 0)
                        {
                            if (frog.State == LivingGameObject.States.Idle && Vector2.Distance(frog.position, player.position) <= proximityDistance)
                            {
                                GameEnvironment.AssetManager.PlaySound("FrogJump", volume);
                            }
                             
                            frog.ChangeState(LivingGameObject.States.Jumping);
                            if (frog.flipped == true)
                                 frog.Sprite = jumping_left;
                             else frog.Sprite = jumping;
                            frog.velocity.Y = frogJumpStartVelocity;
                            frog.velocity.Y -= frogJumpContinueVelocity;
                            frog.velocity.X -= frog.moveSpeed.X;
                            frog.gravityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        //falls back down
                        if (frog.gravityTimer <= 0)
                        {
                            frog.ChangeState(LivingGameObject.States.Idle);
                             if (frog.flipped == true)
                                 frog.Sprite = idle_left;
                             else frog.Sprite = idle;
                            frog.jumpTimer = frog.jumpTime;
                            frog.velocity.X = 0;
                            frog.timesJumped++;
                            frog.velocity.Y = frog.gravity;
                            frog.gravityTimer = frog.gravityTime;
                        }

                    if (frog.timesJumped >= 3)
                    {
                        //changes direction of frog
                        frog.moveSpeed.X = -frog.moveSpeed.X;
                        if (frog.flipped == true)
                            frog.flipped = false;
                        else frog.flipped = true;

                        frog.timesJumped = 0;
                    }
                }
            }         
        }
    }
}
