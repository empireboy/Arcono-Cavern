using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;
using Arcono.Editor.Managers;
using Engine;
using Microsoft.Xna.Framework;

namespace Arcono
{
    public class LevelEndManager : GameObject
    {
        public Player player;
        public List<EndPoint> endPoint;
        public Level level;
        public int levelIndex;
        private float volume;
        Button button = new Button();
        Editor.LevelEditor levelEditor = GameEnvironment.gameStateList[1] as Editor.LevelEditor;
        LevelSelect levelSelect = GameEnvironment.gameStateList[3] as LevelSelect;

        //public string CurrentLevelName => levelEditor.saveLoadManager.CurrentLevelName;

        public LevelEndManager(Player player, List<EndPoint> endPoint, Level level)
        {
            this.player = player;
            this.endPoint = endPoint;
            this.level = level;
            volume = 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (EndPoint endPoint in endPoint)
            {
                if (player.CollidesWith(endPoint))
                {
                    GameEnvironment.AssetManager.PlaySound("EndingLevel_01", volume);
                    level.Children.Clear();
                    EndLevelHitYay();
                }
            }
        }

        public void CheckLevelIndex()
        {
            if (levelEditor.CurrentLevelName == "level1")
            {
                levelIndex = 1;
            }
            else if (levelEditor.CurrentLevelName == "level2")
            {
                levelIndex = 2;
            }
            else if (levelEditor.CurrentLevelName == "level3")
            {
                levelIndex = 3;
            }
            else if (levelEditor.CurrentLevelName == "level4")
            {
                levelIndex = 4;
            }
            else if (levelEditor.CurrentLevelName == "level5")
            {
                levelIndex = 5;
            }
        }

        public void EndLevelHitYay()
        {
            CheckLevelIndex();

            GameEnvironment.SwitchTo(1, true);
            Editor.LevelEditor levelEditor = GameEnvironment.currentGameState as Editor.LevelEditor;

            levelEditor.CalculateCollectPercentage(level.coinManager, this);
            levelEditor.SaveCoins("level" + levelIndex + "coins");

            if (levelEditor.NextLevelName != null)
            {
                levelEditor.Load(levelEditor.NextLevelName);
                levelEditor.Play();
            }
            else GameEnvironment.SwitchTo(2, true);
        }
    }
}
