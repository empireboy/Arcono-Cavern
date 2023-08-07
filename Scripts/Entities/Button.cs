using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arcono.Editor;

namespace Arcono
{
    public class Button : SpriteGameObject
    {
        private SpriteFont font = GameEnvironment.AssetManager.Content.Load<SpriteFont>("ButtonFont");
        private int loadIndex;
        private string levelName;
        private string buttonDescription;
        private bool levelLoader = false;
        LevelEditor levelEditor;

        private float volume;
        public Button() : base("red_button01")
        {
        }

        // Constructor for the buttons in menu
        public Button(string descr, int index, int x, int y) : base(x, y, "red_button01")
        {
            loadIndex = index;
            buttonDescription = descr;
            SetPosition();

            volume = 0.8f;
        }

        // Constructor for the buttons in level select
        public Button(string descr, string level, int x, int y, LevelEditor levelEditor) : base(x, y, "red_button01")
        {
            levelName = level;
            buttonDescription = descr;
            levelLoader = true;
            this.levelEditor = levelEditor;
            SetPosition();
        }

        // This function centers the buttons
        public void SetPosition() 
        {
            position.X -= HalfTextureWidth;
        }

        // This function will switch/load a certain scene
        public void LoadOnClick()
        {
            ButtonSound();

            if (!levelLoader)
                GameEnvironment.SwitchTo(loadIndex, true);

            if (levelLoader)
            {
                GameEnvironment.SwitchTo(1, true);
                levelEditor.Load(levelName);
                levelEditor.Play();
            }
        }

        // This function will play the sound when the button gets clicked
        public void ButtonSound() 
        {
            GameEnvironment.AssetManager.PlaySound("click1", volume);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            Vector2 textOffset = font.MeasureString(buttonDescription);

            spriteBatch.DrawString(font, buttonDescription, new Vector2(position.X + HalfTextureWidth - (textOffset.X / 2), position.Y + HalfTextureHeight - (textOffset.Y / 2)), Color.White);
        }
    }
}
