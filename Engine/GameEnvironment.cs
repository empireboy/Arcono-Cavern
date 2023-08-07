using Arcono;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Engine
{
    public abstract class GameEnvironment : Game
    {
        static protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected static AssetManager assetManager;
        protected static Point screen;
        protected static Random random;
        protected InputHelper inputHelper;
        static public List<GameObject> gameStateList;
        static public GameObject currentGameState;
        public static int ScreenHeight, ScreenWidth;

        public static Camera camera;
        public static CameraMover cameraMover;

        public static Point Screen
        {
            get { return screen; }
        }

        public static Random Random
        {
            get { return random; }
        }

        public static AssetManager AssetManager
        {
            get { return assetManager; }
        }

        public static GraphicsDeviceManager Graphics
        {
            get { return graphics; }
        }

        public GameEnvironment()
        {
            graphics = new GraphicsDeviceManager(this);
            inputHelper = new InputHelper();
            Content.RootDirectory = "Content";
            assetManager = new AssetManager(Content);
            gameStateList = new List<GameObject>();
            random = new Random();
        }

        public void ApplyResolutionSettings()
        {
            graphics.PreferredBackBufferWidth = Screen.X;
            graphics.PreferredBackBufferHeight = Screen.Y;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected void HandleInput()
        {
            inputHelper.Update();
            if (inputHelper.KeyPressed(Keys.Escape))
            {
                if (currentGameState == gameStateList[2])
                {
                    Exit();
                }
            }

            if (currentGameState != null)
                currentGameState.HandleInput(inputHelper);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            cameraMover.HandleInput(inputHelper);
            cameraMover.Update(gameTime);
            camera.Follow(cameraMover);

            if (currentGameState != null)
                currentGameState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(transformMatrix: camera.Transform);

            // Draw the game objects
            if (currentGameState != null)
                currentGameState.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        static public void SwitchTo(int gameStateIndex, bool resetState = false, List<GameObject> gameObjects = null)
        {
            if (gameStateIndex >= 0 && gameStateIndex < gameStateList.Count)
                currentGameState = gameStateList[gameStateIndex];

            if (gameObjects != null)
                (currentGameState as GameObjectList).AddRange(gameObjects);

            if (resetState && currentGameState != null)
                currentGameState.Reset();
        }
    }
}
