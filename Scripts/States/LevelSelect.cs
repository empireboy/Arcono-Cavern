using Arcono.Editor;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Arcono
{
    public class LevelSelect : GameObjectList
    {
        private Texture2D background = GameEnvironment.AssetManager.Content.Load<Texture2D>("BackGround (2)");

        public List<Button> buttons = new List<Button>();

        public int levelIndex = 0;

        public LevelSelect(LevelEditor levelEditor)
        {
            // Add the buttons to the list buttons
            buttons.Add(new Button("Menu", 2, 190, 0));

            buttons.Add(new Button("Level 1", "level1", GameEnvironment.Screen.X / 2, 200, levelEditor));
            buttons.Add(new Button("Level 2", "level2", GameEnvironment.Screen.X / 2, 350, levelEditor));
            buttons.Add(new Button("Level 3", "level3", GameEnvironment.Screen.X / 2, 500, levelEditor));
            buttons.Add(new Button("Level 4", "level4", GameEnvironment.Screen.X / 2, 650, levelEditor));

            // Add every button in buttons to level select gameobjectlist so that it can be drawn
            foreach (GameObject gameObject in buttons)
            {
                Add(gameObject);
            }

            Add(new ButtonManager(buttons));
        }

		public override void Reset()
		{
			base.Reset();

            //Camera Management
            GameEnvironment.camera = new Camera();
            GameEnvironment.cameraMover = new CameraMover(null)
            {
                IsMenu = true
            };
        }

        // Draw the objects that need to be drawn
		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, position, Color.White);

            base.Draw(gameTime, spriteBatch);
        }
    }
}
