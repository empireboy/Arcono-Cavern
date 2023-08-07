using Engine;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_PlayManager : GameObject
	{
		private readonly LevelEditor_GridManager gridManager;

		public LevelEditor_PlayManager(LevelEditor_GridManager gridManager)
		{
			this.gridManager = gridManager;
		}

        public void Play()
		{
            List<GameObject> gameObjects = new List<GameObject>();

            // Add all items to a list to send it towards the level scene
            for (int i = 0; i < gridManager.Grid.Width; i++)
            {
                for (int j = 0; j < gridManager.Grid.Height; j++)
                {
                    if (!gridManager.ItemExists(i, j))
                        continue;

                    gameObjects.Add(gridManager.Grid.GetItem(i, j).Item);
                }
            }

            gridManager.Clear();

            GameEnvironment.SwitchTo(0, true, gameObjects);
        }

		public override void HandleInput(InputHelper inputHelper)
		{
            if (inputHelper.KeyPressed(Keys.P))
            {
                Play();
            }

            if (inputHelper.KeyPressed(Keys.Escape))
            {
                GameEnvironment.SwitchTo(2, true);
            }
        }
    }
}
