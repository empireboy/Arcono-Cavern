using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Engine
{
	public abstract class GameState
    {
        protected List<GameObject> gameObjectList;

        public GameState()
        {
            gameObjectList = new List<GameObject>();
        }

        public virtual void Reset()
        {
            foreach (GameObject gameObject in gameObjectList)
                gameObject.Reset();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in gameObjectList)
                gameObject.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in gameObjectList)
                gameObject.Draw(gameTime, spriteBatch);
        }

    }
}
