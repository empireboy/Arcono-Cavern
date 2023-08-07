using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
	public abstract class GameObject : IGameLoopObject
    {
        public GameObject parent;
        public Vector2 position;
        public Vector2 startPosition;
        public Vector2 velocity;
        public bool isActive;
        public bool isResettable;
        public bool spikeTimer;
        public bool visible;

        protected int layer;
        protected string id;

        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public virtual Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public virtual Vector2 GlobalPosition
        {
            get
            {
                if (parent != null)
                {
                    return parent.GlobalPosition + Position;
                }
                else
                {
                    return Position;
                }
            }
        }

        public GameObject Root
        {
            get
            {
                if (parent != null)
                {
                    return parent.Root;
                }
                else
                {
                    return this;
                }
            }
        }

        public GameObjectList GameWorld
        {
            get
            {
                return Root as GameObjectList;
            }
        }

        public virtual int Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public virtual GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public string Id
        {
            get { return id; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public virtual Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, 0, 0);
            }
        }

        public GameObject(int layer = 0, string id = "")
        {
            this.layer = layer;
            this.id = id;
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            visible = true;

            isActive = true;
            isResettable = true;
        }

        public GameObject(int posX, int posY, int layer = 0, string id = "") 
        {
            this.layer = layer;
            this.id = id;
            position = Vector2.Zero;
            velocity = Vector2.Zero;
            visible = true;

            isActive = true;
            isResettable = true;

            position.X = posX;
            position.Y = posY;

            startPosition = position;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!isActive)
                return;

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public virtual void HandleInput(InputHelper inputHelper)
		{

		}

        public virtual void Reset()
        {
            
        }
    }
}
