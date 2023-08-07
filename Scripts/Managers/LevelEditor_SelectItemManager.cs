using Engine;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_SelectItemManager : GameObject
	{
		private readonly LevelEditor_ItemManager itemManager;
		private readonly int itemTypesLength;

		public LevelEditor_SelectItemManager(LevelEditor_ItemManager itemManager)
		{
			this.itemManager = itemManager;

			itemTypesLength = Enum.GetNames(typeof(ItemTypes)).Length - 1;
		}

		public override void HandleInput(InputHelper inputHelper)
		{
			if (inputHelper.KeyPressed(Keys.Q) || inputHelper.MouseScrollUp())
			{
				itemManager.SelectedItem = (int)itemManager.SelectedItem - (ItemTypes)1;
			}
			else if (inputHelper.KeyPressed(Keys.E) || inputHelper.MouseScrollDown())
			{
				itemManager.SelectedItem = (int)itemManager.SelectedItem + (ItemTypes)1;
			}

			// Go to last item if scrolling left
			if ((int)itemManager.SelectedItem <= 0)
			{
				itemManager.SelectedItem = (ItemTypes)itemTypesLength;
			}
			// Go to first item if scrolling right
			else if ((int)itemManager.SelectedItem > itemTypesLength)
			{
				itemManager.SelectedItem = (ItemTypes)1;
			}
		}
	}
}