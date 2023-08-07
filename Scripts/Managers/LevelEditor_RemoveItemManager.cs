using Engine;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_RemoveItemManager : GameObject
	{
		private readonly LevelEditor_GridManager gridManager;

		public LevelEditor_RemoveItemManager(LevelEditor_GridManager gridManager)
		{
			this.gridManager = gridManager;
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			if (inputHelper.MouseRightButtonDown())
			{
				int x = (int)gridManager.SelectedTile.X;
				int y = (int)gridManager.SelectedTile.Y;

				if (gridManager.ItemExists(x, y))
					gridManager.RemoveItem(x, y);
			}
		}
	}
}