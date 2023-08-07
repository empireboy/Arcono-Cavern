using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcono.Editor.Managers
{
	public class LevelEditor_GridRenderer : GameObject
	{
		private readonly LevelEditor_GridManager gridManager;

		private readonly Texture2D tileTexture;
		private readonly Texture2D selectedTileTexture;

		public LevelEditor_GridRenderer(LevelEditor_GridManager gridManager) : base()
		{
			this.gridManager = gridManager;

			tileTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("tempEmptyGrid");
			selectedTileTexture = GameEnvironment.AssetManager.Content.Load<Texture2D>("tempFilledGrid");
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			for (int i = 0; i < gridManager.Grid.Width; i++)
			{
				for (int j = 0; j < gridManager.Grid.Height; j++)
				{
					if (gridManager.SelectedTile.X == i && gridManager.SelectedTile.Y == j)
					{
						// Draw Selected Tile if hovered
						spriteBatch.Draw(selectedTileTexture, new Rectangle(i * gridManager.tileSize, j * gridManager.tileSize, gridManager.tileSize, gridManager.tileSize), Color.White);
					}
					else
					{
						// Draw Default Tile
						spriteBatch.Draw(tileTexture, new Rectangle(i * gridManager.tileSize, j * gridManager.tileSize, gridManager.tileSize, gridManager.tileSize), Color.White);
					}
				}
			}
		}
	}
}
