using Engine;
using Microsoft.Xna.Framework.Input;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_ResizeGridManager : GameObject
	{
        public readonly LevelEditor_GridManager gridManager;
        public readonly int resizeStrength = 1;

        public LevelEditor_ResizeGridManager(LevelEditor_GridManager gridManager)
		{
            this.gridManager = gridManager;
		}

		public override void HandleInput(InputHelper inputHelper)
		{
            if (inputHelper.KeyPressed(Keys.Right))
            {
                gridManager.CreateGrid(gridManager.Grid.Width + resizeStrength, gridManager.Grid.Height);
            }
            else if (inputHelper.KeyPressed(Keys.Down))
            {
                gridManager.CreateGrid(gridManager.Grid.Width, gridManager.Grid.Height + resizeStrength);
            }
            else if (inputHelper.KeyPressed(Keys.Left))
            {
                if (gridManager.Grid.Width <= gridManager.minimumGridWidth)
                    return;

                gridManager.CreateGrid(gridManager.Grid.Width - resizeStrength, gridManager.Grid.Height);
            }
            else if (inputHelper.KeyPressed(Keys.Up))
            {
                if (gridManager.Grid.Height <= gridManager.minimumGridHeight)
                    return;

                gridManager.CreateGrid(gridManager.Grid.Width, gridManager.Grid.Height - resizeStrength);
            }
        }
	}
}
