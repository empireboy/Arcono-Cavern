using Arcono.Editor;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Arcono
{
    public class Menu : GameObjectList
    {
        public List<Button> buttons = new List<Button>();
        private Texture2D background = GameEnvironment.AssetManager.Content.Load<Texture2D>("Background (2)");

        private SpriteFont font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("TitleFont");
        private string gameTitle = "Arcono Cavern";

        public Menu()
        {
            // Add the buttons to the list buttons
            buttons.Add(new Button("Level Editor", 1, GameEnvironment.Screen.X / 2, 350));
            buttons.Add(new Button("Level Select", 3, GameEnvironment.Screen.X / 2, 500));


            // Add every button in buttons to level select gameobjectlist so that it can be drawn
            foreach (GameObject gameObject in buttons) 
            {
                Add(gameObject);
            }

            Add(new ButtonManager(buttons));

            Vector2 textOffset = font.MeasureString(gameTitle);
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

            // Get the font size so the text can be centered in a button 
            Vector2 textOffset = font.MeasureString(gameTitle);
                    
            spriteBatch.DrawString(font, gameTitle, new Vector2(GameEnvironment.Screen.X / 2 - textOffset.X / 2, 80), Color.White);
        }
    }
}
