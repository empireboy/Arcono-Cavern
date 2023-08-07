using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arcono
{
    public class ClimbingManager : GameObject
    {
        private Player player;
        private List<Ladder> ladders;

        private bool isClimbingUp;
        private bool isClimbing;

        private readonly float topLadderTreshold;
        private readonly float climbSpeed;

        private SpriteSheet climbing = new SpriteSheet("player_climb (1)");

        private Ladder activeLadder;

        public ClimbingManager(Player player, List<Ladder> ladders) : base()
        {
            this.player = player;
            this.ladders = ladders;
            topLadderTreshold = 10;
            climbSpeed = 330;
        }

        //Downwards movement on a ladder
        private void ClimbDown(Ladder ladder)
        {
            player.position.X = ladder.position.X + ladder.HalfTextureWidth- player.HalfTextureWidth;
            player.velocity.Y = climbSpeed;
        }

        //Upwards movement on a ladder
        private void ClimbUp(Ladder ladder)
        {
            player.position.X = ladder.position.X + ladder.HalfTextureWidth - player.HalfTextureWidth;
            player.velocity.Y = -climbSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Ladder ladder in ladders)
            {
                if (ladder.CollidesWith(player))
                {
                    player.velocity.Y = 0;

                    //Checks the climbing boolean and lets the player climb up and down
                    if (isClimbing == true)
                    {
                        //changes player sprite
                        player.Sprite = climbing;

                        if (isClimbingUp == false && isClimbing == true)
                        {
                            ClimbDown(ladder);
                        }
                        else if (isClimbingUp && isClimbing == true && player.position.Y + player.Sprite.Height > ladder.position.Y + topLadderTreshold)
                        {
                            ClimbUp(ladder);
                        }
                        else if (player.position.Y + player.Sprite.Height <= ladder.position.Y + topLadderTreshold)
                        {
                            player.ChangeState(Player.States.Idle);
                            break;
                        }
                        //Check for climbing a ladder and changing the PlayerState
                        player.ChangeState(Player.States.Climbing);

                        if (activeLadder != ladder)
                        {
                            activeLadder = ladder;
                        }
                    }
                    break;
                }
                else if (activeLadder == ladder && player.State == Player.States.Climbing)
                {
                    player.ChangeState(Player.States.Idle);
                }
            }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            //Input for when the S button is pressed
            if (inputHelper.IsKeyDown(Keys.S) || inputHelper.IsKeyDown(Keys.Down))
            {
                isClimbingUp = false;
                isClimbing = true;
            }
            //Input for when the W button is pressed
            else if (inputHelper.IsKeyDown(Keys.W) || inputHelper.IsKeyDown(Keys.Up))
            {
                isClimbingUp = true;
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
    }
}
