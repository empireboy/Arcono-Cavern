using Engine;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_ItemManager : GameObjectList
	{
		public ItemTypes SelectedItem { get; set; }
		
		public LevelEditor_ItemManager(LevelEditor_GridManager gridManager, UnlockItemManager unlockItemManager)
		{
			Add(new LevelEditor_PlaceItemManager(this, gridManager, unlockItemManager));
			Add(new LevelEditor_SelectItemManager(this));
			Add(new LevelEditor_RemoveItemManager(gridManager));
		}

		public override void Reset()
		{
			base.Reset();

			SelectedItem = ItemTypes.Player;
		}
	}
}
