using Arcono.Editor;
using Arcono.Editor.Managers;
using Arcono.Managers;
using Engine;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arcono
{
    public class Level : GameObjectList
    {
        public CoinManager coinManager = new CoinManager();

        public Level() : base()
        {

        }

        public override void Reset()
        {
            base.Reset();

            /*
            Rope rope = new Rope
            {
                isActive = false
            };

            //list of all enemies
            List<Frog> frogs = new List<Frog>
            {
               new Frog(1536, 512)
            };

            List<LivingGameObject> collidingObjects = new List<LivingGameObject>
            {
            };
            collidingObjects.AddRange(frogs);
            collidingObjects.Add(player);

            List<AttachPoint> attachPoints = new List<AttachPoint>
            {
                new AttachPoint(1088, 50)
            };

            //List of all the ladders in a level
            List<Ladder> ladders = new List<Ladder>
            {
                new Ladder(640, 128),
                new Ladder(640, 256),
                new Ladder(640, 384),
                new Ladder(640, 512),
            };

            //List of all the coins in a level
            List<Coin> coins = new List<Coin>
            {
                new Coin(256,0),
                new Coin(384,0),
                new Coin(512,0),
            };

            List<Spikes> spikes = new List<Spikes>
            {
                new Spikes(1024, 322),
                new LeftSpikes(0, 256),
                new LeftSpikes(0, 512),
                //By adding right spikes, use the standard values, then add 66 on the x
                new RightSpikes(1090, 512),
                new RightSpikes(1218, 0),
                new RoofSpikes(1152, 0),
                new FallingSpikes(768, 0, 192),
                //new FallingSpikes(896, 0, 192)
            };

           

            //List of killing objects
            List<SpriteGameObject> killingObjects = new List<SpriteGameObject>
            {
               
            };
            killingObjects.AddRange(frogs);
            killingObjects.AddRange(spikes);
            
            List<CollectibleGameObject> collectibleGameObjects = new List<CollectibleGameObject>
            {
                // Miscellaneous Objects
                new GrapplingHookPart(0, 256),
                new GrapplingHookPart(0, 512)
            };

            List<Doors> doors = new List<Doors>
            {
                new Doors(256, 256),
            };

            List<Key> keys = new List<Key>
            {
                new Key(1024, 512),
            };

            List<Platform> platforms = new List<Platform>
            {
               
                new MovingPlatform(1280, 256, 1792, 256, "horizontal"),

               

                new Platform(0, 128),
                new Platform(128,128),
                new Platform(256, 128),
                new Platform(384, 128),
                new Platform(512, 128),

                new Platform(768, 256),
                new Platform(768, 384),
                new Platform(896, 256),
                new Platform(896, 384),
                new Platform(1024, 384),

                new Platform(1152, 256),
                new Platform(1152, 384),
                new Platform(1152, 512),

                new Platform(0, 384),
                new Platform(128, 384),
                new Platform(256, 384),
                new Platform(384, 384),
                new Platform(512, 384),

                new Platform(0, 640),
                new Platform(128, 640),
                new Platform(256, 640),
                new Platform(384, 640),
                new Platform(512, 640),
                new Platform(640, 640),
                new Platform(768, 640),
                new Platform(896, 640),
                new Platform(1024, 640),
                new Platform(1152, 640),

                new Platform(1280,0),

                new Platform(1280, 640),
                new Platform(1408, 640),
                new Platform(1536, 640),
            };

            List<Platform> invisibleWalls = new List<Platform>
            {
                new InvisibleWall(768, 512)
            };

            HUDKeyBlueEmpty hudKeyBlueEmpty = new HUDKeyBlueEmpty(0, 0);
            HUDKeyBlue hudKeyBlue = new HUDKeyBlue(0, 0);
            HUDGrapplingHookP1 hudGrapplingHookP1 = new HUDGrapplingHookP1(55, 32);
            HUDGrapplingHookP2 hudGrapplingHookP2 = new HUDGrapplingHookP2(64, 0);


            //Camera Managment
            GameEnvironment.camera = new Camera();
            GameEnvironment.cameraMover  = new CameraMover(player);


            Add(player);

            AddRange(attachPoints);
            AddRange(ladders);
            AddRange(coins);
            AddRange(frogs);
            AddRange(killingObjects);
            AddRange(platforms);
            AddRange(collectibleGameObjects);
            AddRange(invisibleWalls);
            AddRange(doors);
            AddRange(keys);
            Add(rope);
            

            //HUD elements
            Add(hudKeyBlueEmpty);
            Add(hudKeyBlue);
            Add(hudGrapplingHookP1);
            Add(hudGrapplingHookP2);

            // Managers
            Add(new CollisionManager(collidingObjects, platforms));
            Add(new InvisibleWallManager(player, invisibleWalls));
            Add(new MovementManager(player));
            Add(new SwingingManager(player, rope, attachPoints));
            Add(new ClimbingManager(player, ladders));
            Add(new CoinManager(player, coins, GameEnvironment.cameraMover));
            Add(new FrogMoveManager(frogs));
            Add(new DeathManager(player, killingObjects, this));
            Add(doorManager = new DoorManager(player, doors, keys));
            Add(new CollectibleManager(player, collectibleGameObjects));

            */

            Player player = null;
            List<Ladder> ladders = new List<Ladder>();
            List<SpriteGameObject> killingObjects = new List<SpriteGameObject>();
            List<Platform> platforms = new List<Platform>();
            List<SpriteGameObject> platformsAsGameObject = new List<SpriteGameObject>();
            List<Doors> doors = new List<Doors>();
            List<SpriteGameObject> doorsAsGameObject = new List<SpriteGameObject>();
            List<SpriteGameObject> collisionObjects = new List<SpriteGameObject>();
            List<LivingGameObject> collidingObjects = new List<LivingGameObject>();
            List<Coin> coins = new List<Coin>();
            List<Key> keys = new List<Key>();
            List<Frog> frogs = new List<Frog>();
            List<AttachPoint> attachPoints = new List<AttachPoint>();
            List<MovingPlatformVertical> movingPlatformVerticals = new List<MovingPlatformVertical>();
            List<MovingPlatformHorizontal> movingPlatformHorizontals = new List<MovingPlatformHorizontal>();
            List<SpriteGameObject> movingPlatformsAsGameObjects = new List<SpriteGameObject>();
            List<CollectibleGameObject> collectibleGameObjects = new List<CollectibleGameObject>();
            List<InvisibleWall> invisibleWalls = new List<InvisibleWall>();
            List<EndPoint> endPoint = new List<EndPoint>();
            List<SpriteGameObject> explainMans = new List<SpriteGameObject>();
            List<Checkpoint> checkpoints = new List<Checkpoint>();

            Rope rope = new Rope();

            foreach (GameObject gameObject in Children)
            {

                if (gameObject is Ladder)
                {
                    ladders.Add(gameObject as Ladder);
                    continue;
                }
                else if (gameObject is Spikes)
                {
                    killingObjects.Add(gameObject as Spikes);
                    continue;
                }
                else if (gameObject is InvisibleWall)
                {
                    // InvisibleWall must be checked before Platform otherwise the invisible wall won't be added properly
                    invisibleWalls.Add(gameObject as InvisibleWall);
                    continue;
                }
                else if (gameObject is Platform)
                {
                    platforms.Add(gameObject as Platform);
                    platformsAsGameObject.Add(gameObject as SpriteGameObject);
                    continue;
                }
                else if (gameObject is Doors)
                {
                    doors.Add(gameObject as Doors);
                    doorsAsGameObject.Add(gameObject as SpriteGameObject);
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
                    collectibleGameObjects.Add(gameObject as GrapplingHookPart);
                    continue;
                }
                else if (gameObject is Frog)
                {
                    killingObjects.Add(gameObject as Frog);
                    frogs.Add(gameObject as Frog);
                    continue;
                }
                else if (gameObject is AttachPoint)
                {
                    attachPoints.Add(gameObject as AttachPoint);
                    continue;
                }
                else if (gameObject is MovingPlatformVertical)
                {
                    movingPlatformVerticals.Add(gameObject as MovingPlatformVertical);
                    movingPlatformsAsGameObjects.Add(gameObject as MovingPlatformVertical);
                    continue;
                }
                else if (gameObject is MovingPlatformHorizontal)
                {
                    movingPlatformHorizontals.Add(gameObject as MovingPlatformHorizontal);
                    movingPlatformsAsGameObjects.Add(gameObject as MovingPlatformHorizontal);
                    continue;
                }
                else if (gameObject is MovementManager)
                {
                    return;
                }
                else if (gameObject is EndPoint)
                {
                    endPoint.Add(gameObject as EndPoint);
                    continue;
                }

                else if (gameObject is ExplanationMan)
                {
                    explainMans.Add(gameObject as ExplanationMan);
                    continue;
                }

                else if (gameObject is Player)
                {
                    player = gameObject as Player;
                    continue;
                }

                else if (gameObject is Checkpoint)
                {
                    checkpoints.Add(gameObject as Checkpoint);
                }
            }

            for (int i = 0; i < 20; i++)
			{
                for (int j = 0; j < 30;  j++)
				{
					SpriteGameObject background = new SpriteGameObject("Background", -2)
					{
						position = new Vector2(827 * i, 585 * j)
					};
					Add(background);
                }
            }

            collisionObjects.AddRange(platformsAsGameObject);
            collisionObjects.AddRange(doorsAsGameObject);
            collisionObjects.AddRange(movingPlatformsAsGameObjects);

            collidingObjects.Add(player);
            collidingObjects.AddRange(frogs);

            //Camera Managment
            if (player != null)
            {
                GameEnvironment.camera = new Camera();
                GameEnvironment.cameraMover = new CameraMover(player);
            }

            // Managers
            if (collisionObjects.Count > 0 && collidingObjects.Count > 0)
                Add(new CollisionManager(collidingObjects, collisionObjects));

            if (player != null)
                Add(new MovementManager(player));

            if(frogs.Count > 0)
                Add(new FrogMoveManager(frogs, player));

            //Add(new SwingingManager(player, rope, attachPoints));
            if (player != null && attachPoints.Count > 0)
                Add(new SwingingManager(player, rope, attachPoints));

            if (player != null && ladders.Count > 0)
                Add(new ClimbingManager(player, ladders));

            if (player != null && coins.Count > 0 && GameEnvironment.cameraMover != null)
            {
                coinManager = new CoinManager(player, coins, GameEnvironment.cameraMover);
                Add(coinManager);
            }

            if (player != null && killingObjects.Count > 0)
                Add(new DeathManager(player, killingObjects, collisionObjects, this));

            if (player != null && doors.Count > 0 && keys.Count > 0)
                Add(new DoorManager(player, doors, keys, GameEnvironment.cameraMover, coins));

            if (player != null && collectibleGameObjects.Count > 0)
                Add(new CollectibleManager(player, collectibleGameObjects, GameEnvironment.cameraMover, coins));

            if (player != null && invisibleWalls.Count > 0)
                Add(new InvisibleWallManager(player, invisibleWalls));

            if (player != null && endPoint.Count > 0)
                Add(new LevelEndManager(player, endPoint, this));

            if (player != null && explainMans.Count > 0)
                Add(new ExplanationManager(explainMans, player, this));

            if (player != null && checkpoints.Count > 0)
                Add(new CheckpointManager(this, checkpoints));           
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Children.Clear();

                GameEnvironment.SwitchTo(3, true);
            }
        }
    }
}

