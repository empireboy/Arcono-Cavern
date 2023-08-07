using Arcono.Editor;
using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Arcono
{
    public class ExplanationManager : GameObjectList
    {
        private List<SpriteGameObject> explainMans;
        //dialogue
        private Texture2D textBox;
        private Texture2D textBoxLevel1;
        private Texture2D textBoxLevel2;
        private Texture2D textBoxLevel3;
        private Texture2D textBoxLevel4;
        private Texture2D textBoxLevel5;
        private Texture2D texBoxBlank;

        private Player player;
        private Level level;


        public ExplanationManager(List<SpriteGameObject> explainMans, Player player, Level level) : base()
        {
           
            textBoxLevel1 = GameEnvironment.AssetManager.GetSprite("dialogue-Box1");
            textBoxLevel2 = GameEnvironment.AssetManager.GetSprite("dialogue-Box8");
            textBoxLevel3 = GameEnvironment.AssetManager.GetSprite("dialogue-Box11");
            textBoxLevel4 = GameEnvironment.AssetManager.GetSprite("dialogue-Box13");
            textBoxLevel5 = GameEnvironment.AssetManager.GetSprite("dialogue-Box15");
            texBoxBlank = GameEnvironment.AssetManager.GetSprite("dialogue-Box(new)");

            this.explainMans = explainMans;
            this.player = player;
            this.level = level;
            Reset();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            foreach (SpriteGameObject explanationMan in explainMans)
            {
                if (player.CollidesWith(explanationMan))
                {
                    DrawTextBox(spriteBatch);
                }
            }
        }



        private void DrawTextBox(SpriteBatch spriteBatch)
        {

            //Decides dialogue
            foreach (ExplanationMan man in explainMans)
            {
                if ((GameEnvironment.gameStateList[1] as LevelEditor).CurrentLevelName == "level1")
                {
                    textBox = textBoxLevel1;
                }
                else if ((GameEnvironment.gameStateList[1] as LevelEditor).CurrentLevelName == "level2")
                {
                    textBox = textBoxLevel2;
                }
                else if ((GameEnvironment.gameStateList[1] as LevelEditor).CurrentLevelName == "level3")
                {
                    textBox = textBoxLevel3;
                }
                else if ((GameEnvironment.gameStateList[1] as LevelEditor).CurrentLevelName == "level4")
                {
                    textBox = textBoxLevel4;
                }
                else if ((GameEnvironment.gameStateList[1] as LevelEditor).CurrentLevelName == "level5")
                {
                    textBox = textBoxLevel5;
                }
                else textBox = texBoxBlank;

                //Draws the textBox
                Vector2 drawPosition = new Vector2(
                 GameEnvironment.cameraMover.position.X - ArconoEnvironment.ScreenWidth / 4 + 128,
                 GameEnvironment.cameraMover.position.Y + ArconoEnvironment.ScreenWidth / 4 - 70
                );
                spriteBatch.Draw(textBox, drawPosition, Color.White);

            }
        }
    }
}

   
