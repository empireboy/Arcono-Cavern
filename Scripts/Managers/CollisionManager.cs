using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono
{
    class CollisionManager : GameObject
    {
        private List<LivingGameObject> collidingObjects;
        private List<SpriteGameObject> collisionObjects;

        public CollisionManager(List<LivingGameObject> collidingObjects, List<SpriteGameObject> collisionObjects) : base()
        {
            this.collidingObjects = collidingObjects;
            this.collisionObjects = collisionObjects;
        }

        public override void Update(GameTime gametime)
        {
            foreach (SpriteGameObject collisionObject in collisionObjects)
            {
                if (!collisionObject.isActive)
                    continue;

                if (collisionObject is MovingPlatformHorizontal)
                {
                    foreach (SpriteGameObject platform in collisionObjects)
                    {
                        if (collisionObject.CollidesWith(platform) && platform is Platform)
                        {
                            //movingPlatform X platform collision
                            if (collisionObject.position.X + collisionObject.Sprite.Width > platform.position.X && collisionObject.position.X < platform.position.X
                                || collisionObject.position.X < platform.position.X + platform.Sprite.Width && collisionObject.position.X + collisionObject.Sprite.Width > platform.position.X + platform.Sprite.Width)
                            {
                                //negates adjacent platforms
                                if (collisionObject.position.Y + collisionObject.HalfTextureHeight == platform.position.Y + platform.HalfTextureHeight)
                                    collisionObject.velocity = -collisionObject.velocity;
                            }
                            //movingPlatform Y platform collision
                            if (collisionObject.position.Y + collisionObject.Sprite.Height > platform.position.Y && collisionObject.position.Y < platform.position.Y
                                || collisionObject.position.Y < platform.position.Y + platform.Sprite.Height && collisionObject.position.Y + collisionObject.Sprite.Height > platform.position.Y + platform.Sprite.Height)
                            {
                                //negates adjacent platforms
                                if (collisionObject.position.X + collisionObject.HalfTextureWidth == platform.position.X + platform.HalfTextureWidth)
                                    collisionObject.velocity = -collisionObject.velocity;
                            }
                        }
                    }


                    //entities collision
                    foreach (LivingGameObject other in collidingObjects)

                        if (collisionObject.CollidesWith(other))
                        {
                            other.velocity.Y = 0;

                            // Teleports player on top of platform if inside
                            if (other.position.X < collisionObject.position.X + collisionObject.Sprite.Width && other.position.X > collisionObject.position.X - other.Sprite.Width && other.position.Y + other.Sprite.Height / 2 < collisionObject.position.Y)
                            {
                                other.position.Y = collisionObject.position.Y - other.Sprite.Height;

                                other.position += collisionObject.velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
                            }
                        }
                        else other.velocity.Y = other.gravity;
                }

                else
                {
                    foreach (LivingGameObject other in collidingObjects)
                    {
                        if (collisionObject.CollidesWith(other))
                        {
                            bool bottom = false;
                            other.velocity.Y = 0;
                            // Bottom collision
                            if (other.position.X < collisionObject.position.X + collisionObject.Sprite.Width && other.position.X > collisionObject.position.X - other.Sprite.Width && other.position.Y + other.HalfTextureHeight + 5 > collisionObject.position.Y + collisionObject.Sprite.Height)
                            {
                                other.position.Y = collisionObject.position.Y + collisionObject.Sprite.Height;
                                bottom = true;
                            }

                            if (!bottom)
                            {
                                // Player check for being above or under a platform
                                if (other.position.Y + other.HalfTextureHeight > collisionObject.position.Y && other.position.Y - other.HalfTextureHeight + 50 < collisionObject.position.Y + collisionObject.Sprite.Height)
                                {
                                    // Left side collision
                                    if (other.position.X < collisionObject.position.X)
                                        other.position.X = collisionObject.position.X - other.Sprite.Width;

                                    // Right side collision
                                    if (other.position.X > collisionObject.position.X)
                                        other.position.X = collisionObject.position.X + collisionObject.Sprite.Width;
                                }
                                // Teleports player on top of platform if inside

                                if (other.position.X < collisionObject.position.X + collisionObject.Sprite.Width && other.position.X > collisionObject.position.X - other.Sprite.Width && other.position.Y < collisionObject.position.Y + collisionObject.HalfTextureHeight)
                                    other.position.Y = collisionObject.position.Y - other.Sprite.Height;
                            }

                        }
                        else other.velocity.Y = other.gravity;
                    }
                }
                if (collisionObject is MovingPlatformVertical)
                {
                    foreach (SpriteGameObject platform in collisionObjects)
                    {
                        if (collisionObject.CollidesWith(platform) && platform is Platform)
                        {
                            //movingPlatform X platform collision
                            if (collisionObject.position.X + collisionObject.Sprite.Width > platform.position.X && collisionObject.position.X < platform.position.X
                                || collisionObject.position.X < platform.position.X + platform.Sprite.Width && collisionObject.position.X + collisionObject.Sprite.Width > platform.position.X + platform.Sprite.Width)
                            {
                                //negates adjacent platforms
                                if (collisionObject.position.Y + collisionObject.HalfTextureHeight == platform.position.Y + platform.HalfTextureHeight)
                                    collisionObject.velocity = -collisionObject.velocity;
                            }
                            //movingPlatform Y platform collision
                            if (collisionObject.position.Y + collisionObject.Sprite.Height > platform.position.Y && collisionObject.position.Y < platform.position.Y
                                || collisionObject.position.Y < platform.position.Y + platform.Sprite.Height && collisionObject.position.Y + collisionObject.Sprite.Height > platform.position.Y + platform.Sprite.Height)
                            {
                                //negates adjacent platforms
                                if (collisionObject.position.X + collisionObject.HalfTextureWidth == platform.position.X + platform.HalfTextureWidth)
                                    collisionObject.velocity = -collisionObject.velocity;
                            }
                        }
                    }
                }
            }
        }
    }
}
