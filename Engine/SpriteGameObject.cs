﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Engine
{
	public class SpriteGameObject : GameObject
    {
        protected Color shade = Color.White;
        protected SpriteSheet sprite;
        protected Vector2 origin;
        protected float scale = 1f;
        public bool PerPixelCollisionDetection = false;
        public Vector2 endPosition;

        public float HalfTextureWidth => Width / 2;
        public float HalfTextureHeight => Height / 2;

        public SpriteGameObject(string assetName, int layer = 0, string id = "", int sheetIndex = 0)
            : base(layer, id)
        {
            if (assetName != "")
            {
                sprite = new SpriteSheet(assetName, sheetIndex);
            }
            else
            {
                sprite = null;
            }
        }

        public SpriteGameObject(int posX, int posY, string assetName, int layer = 0, string id = "", int sheetIndex = 0)
            : base(layer, id)
        {
            position.X = posX;
            position.Y = posY;

            if (assetName != "")
            {
                sprite = new SpriteSheet(assetName, sheetIndex);
            }
            else
            {
                sprite = null;
            }
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!visible || sprite == null || !isActive)
            {
                return;
            }
            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, shade, 0, Origin, scale, SpriteEffects.None, 0);
        }

        public SpriteSheet Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Vector2 Center
        {
            get { return new Vector2(Width, Height) / 2; }
        }

        public int Width
        {
            get
            {
                return sprite.Width;
            }
        }

        public int Height
        {
            get
            {
                return sprite.Height;
            }
        }

        /// <summary>
        /// Returns / sets the scale of the sprite.
        /// </summary>
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Set the shade the sprite will be drawn in.
        /// </summary>
        public Color Shade
        {
            get { return shade; }
            set { shade = value; }
        }

        public bool Mirror
        {
            get { return sprite.Mirror; }
            set { sprite.Mirror = value; }
        }

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public override Rectangle BoundingBox
        {
            get
            {
                int left = (int)(GlobalPosition.X - origin.X);
                int top = (int)(GlobalPosition.Y - origin.Y);
                return new Rectangle(left, top, Width, Height);
            }
        }

        public bool CollidesWith(SpriteGameObject obj)
        {
            if (!visible || !obj.visible || !BoundingBox.Intersects(obj.BoundingBox))
            {
                return false;
            }
            if (!PerPixelCollisionDetection)
            {
                return true;
            }
            if (this == obj)
            {
                return false;
            }
            Rectangle b = Collision.Intersection(BoundingBox, obj.BoundingBox);
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    int thisx = b.X - (int)(GlobalPosition.X - origin.X) + x;
                    int thisy = b.Y - (int)(GlobalPosition.Y - origin.Y) + y;
                    int objx = b.X - (int)(obj.GlobalPosition.X - obj.origin.X) + x;
                    int objy = b.Y - (int)(obj.GlobalPosition.Y - obj.origin.Y) + y;
                    if (sprite.IsTranslucent(thisx, thisy) && obj.sprite.IsTranslucent(objx, objy))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}