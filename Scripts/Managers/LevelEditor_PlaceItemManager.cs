using Engine;

namespace Arcono.Editor.Managers
{
    public class LevelEditor_PlaceItemManager : GameObject
    {
        private readonly LevelEditor_ItemManager itemManager;
        private readonly LevelEditor_GridManager gridManager;
        private readonly UnlockItemManager unlockItemManager;

        public LevelEditor_PlaceItemManager(LevelEditor_ItemManager itemManager, LevelEditor_GridManager gridManager, UnlockItemManager unlockItemManager)
        {
            this.itemManager = itemManager;
            this.gridManager = gridManager;
            this.unlockItemManager = unlockItemManager;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.MouseLeftButtonDown())
            {
                unlockItemManager.CheckItem(itemManager.SelectedItem);

                if (unlockItemManager.canPlace)
                {
                    gridManager.CreateItem(itemManager.SelectedItem, gridManager.SelectedTile);
                    unlockItemManager.unlockedItem = true;
                }
                else unlockItemManager.unlockedItem = false;
            }
        }
    }
}
