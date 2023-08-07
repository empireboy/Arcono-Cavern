using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcono.Editor.Managers
{
    public class LevelEditor_GridManager : GameObjectList
    {
        public readonly int minimumGridWidth;
        public readonly int minimumGridHeight;
        public readonly int tileSize;

        public Grid<LevelEditor.Tile> Grid { get; set; }

        public Vector2 SelectedTile
        {
            get
            {
                return new Vector2(
                    Math.Clamp((int)mousePosition.X / tileSize, 0, Grid.Width - 1),
                    Math.Clamp((int)mousePosition.Y / tileSize, 0, Grid.Height - 1)
                );
            }
        }

        public Vector2 SelectedTilePosition
        {
            get
            {
                return new Vector2(
                    Math.Clamp((int)mousePosition.X / tileSize * tileSize, 0, (Grid.Width - 1) * tileSize),
                    Math.Clamp((int)mousePosition.Y / tileSize * tileSize, 0, (Grid.Height - 1) * tileSize)
                );
            }
        }

        private LevelEditor.Tile playerTile;

        private Vector2 mousePosition;

        public LevelEditor_GridManager()
        {
            tileSize = 128;
            minimumGridWidth = 7;
            minimumGridHeight = 3;
        }

        public override void Reset()
        {
            base.Reset();

            playerTile = null;
        }

        public void CreateDefaultGrid()
        {
            int width = 9;
            int height = 5;

            CreateGrid(width, height);
        }

        public void CreateGrid(int width, int height)
        {
            LevelEditor.Tile[,] tempGrid = null;

            if (Grid != null)
                tempGrid = Grid.Items;

            Grid = new Grid<LevelEditor.Tile>(width, height);

            // Remove the items outside of the grid for if the grid gets resized
            if (tempGrid != null)
            {
                for (int i = 0; i < tempGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < tempGrid.GetLength(1); j++)
                    {
                        if ((i >= Grid.Width || j >= Grid.Height) && tempGrid[i, j].Item != null)
                            Remove(tempGrid[i, j].Item);
                    }
                }
            }

            for (int i = 0; i < Grid.Width; i++)
            {
                for (int j = 0; j < Grid.Height; j++)
                {
                    if (tempGrid != null && i <= tempGrid.GetLength(0) - 1 && j <= tempGrid.GetLength(1) - 1 && tempGrid[i, j] != null)
                        Grid.SetItem(i, j, tempGrid[i, j]);
                }
            }
        }

        public void CreateGrid(LevelEditor.SaveData saveData)
        {
            Clear();

            Grid = new Grid<LevelEditor.Tile>(saveData.Width, saveData.Height);

            for (int i = 0; i < Grid.Width; i++)
            {
                for (int j = 0; j < Grid.Height; j++)
                {
                    CreateItem(saveData.Tiles[i, j], new Vector2(i, j));
                }
            }
        }

        public void Clear()
        {
            RemoveAllItems();

            playerTile = null;
        }

        public void CreateItem(ItemTypes itemType, int x, int y)
		{
            Vector2 tileCords = new Vector2(x, y);

            CreateItem(itemType, tileCords);
        }

        public void CreateItem(ItemTypes itemType, Vector2 tileCords)
        {
            int x = (int)tileCords.X;
            int y = (int)tileCords.Y;
            LevelEditor.Tile tile = Grid.GetItem(x, y);

            // Return if the same item is already placed on the same tile
            if (tile.Type != ItemTypes.None && tile.Type == itemType)
                return;

            // Remove overlapping items
            if (tile.Type != ItemTypes.None && tile.Type != itemType)
            {
                RemoveItem(x, y);

                if (tile == playerTile)
                    playerTile = null;
            }

            Vector2 tilePosition = GetTilePosition(tileCords);

            SpriteGameObject item = CreateItemByType(itemType);

            if (item == null)
                return;

            InitializeItemByType(item, itemType, tilePosition, tile);

            // Default item settings
            item.velocity = Vector2.Zero;

            Add(item);

            tile.Type = itemType;
            tile.Item = item;
            Grid.SetItem((int)tileCords.X, (int)tileCords.Y, tile);
        }

        public void RemoveItem(int x, int y)
        {
            if (!ItemExists(x, y))
                throw new NullReferenceException("Can't remove Item because it is null. You might want to check if the Item exists using the ItemExists() method");

            Remove(Grid.GetItem(x, y).Item);
            Grid.ClearItem(x, y);
        }


        public bool ItemExists(int x, int y)
        {
            return Grid.GetItem(x, y).Item != null;
        }

        public Vector2 GetRelativeTilePosition(Vector2 tilePosition, Texture2D texture)
        {
            return new Vector2(
                tilePosition.X + tileSize / 2 - texture.Width / 2,
                tilePosition.Y + tileSize / 2 - texture.Height / 2
            );
        }

        public Vector2 GetTilePosition(int x, int y)
        {
            Vector2 tileCords = new Vector2(x, y);

            return GetTilePosition(tileCords);
        }

        public Vector2 GetTilePosition(Vector2 tileCords)
        {
            return tileCords * tileSize;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            mousePosition = GameEnvironment.cameraMover.position - new Vector2(GameEnvironment.Screen.X - tileSize, GameEnvironment.Screen.Y - tileSize) / 2 + inputHelper.MousePosition;
        }

        private void RemoveAllItems()
        {
            if (Grid == null)
                return;

            for (int i = 0; i < Grid.Width; i++)
            {
                for (int j = 0; j < Grid.Height; j++)
                {
                    if (ItemExists(i, j))
                        RemoveItem(i, j);
                }
            }
        }

        private SpriteGameObject CreateItemByType(ItemTypes itemType)
        {
            SpriteGameObject item = null;

            switch (itemType)
            {
                case ItemTypes.Player:

                    if (playerTile != null)
                    {
                        playerTile.Type = ItemTypes.None;
                        Remove(playerTile.Item);
                        playerTile.Item = null;
                    }

                    item = new Player();

                    break;

                case ItemTypes.Ladder:

                    item = new Ladder();

                    break;

                case ItemTypes.Platform:

                    item = new Platform();

                    break;

                case ItemTypes.Spikes:

                    item = new Spikes();

                    break;

                case ItemTypes.SpikesLeft:

                    item = new LeftSpikes();

                    break;

                case ItemTypes.SpikesRight:

                    item = new RightSpikes();

                    break;

                case ItemTypes.SpikesRoof:

                    item = new RoofSpikes();

                    break;

                case ItemTypes.Door:

                    item = new Doors();

                    break;

                case ItemTypes.Key:

                    item = new Key();

                    break;

                case ItemTypes.Coin:

                    item = new Coin();

                    break;

                case ItemTypes.Frog:

                    item = new Frog();

                    break;

                case ItemTypes.AttachPoint:

                    item = new AttachPoint();

                    break;

                case ItemTypes.GrapplingHookPart:

                    item = new GrapplingHookPart();

                    break;

                case ItemTypes.InvisibleWall:

                    item = new InvisibleWall();

                    break;

                case ItemTypes.FallingSpikes:

                    item = new FallingSpikes();

                    break;

                case ItemTypes.EndPoint:

                    item = new EndPoint();

                    break;

                case ItemTypes.MovingPlatformVertical:

                    item = new MovingPlatformVertical();

                    break;

                case ItemTypes.MovingPlatformHorizontal:

                    item = new MovingPlatformHorizontal();

                    break;

                case ItemTypes.ExplanationMan:

                    item = new ExplanationMan();

                    break;

                case ItemTypes.CheckPoint:

                    item = new Checkpoint();

                    break;

                case ItemTypes.Torch1:

                    item = new SpriteGameObject("torch1", -1);

                    break;

                case ItemTypes.Torch2:

                    item = new SpriteGameObject("torch2", -1);

                    break;

                case ItemTypes.TorchOff:

                    item = new SpriteGameObject("torchOff", -1);

                    break;

                case ItemTypes.Rock:

                    item = new SpriteGameObject("rock", -1);

                    break;

                case ItemTypes.MushroomRed:

                    item = new SpriteGameObject("mushroomRed", -1);

                    break;

                case ItemTypes.MushroomBrown:

                    item = new SpriteGameObject("mushroomBrown", -1);

                    break;

                case ItemTypes.PlantPurple:

                    item = new SpriteGameObject("plantPurple", -1);

                    break;

                case ItemTypes.SignRight:

                    item = new SpriteGameObject("signRight", -1);

                    break;

                case ItemTypes.SignLeft:

                    item = new SpriteGameObject("signLeft", -1);

                    break;

                case ItemTypes.BeamDiagonalLeft:

                    item = new SpriteGameObject("beamDiagonalLeft", -1);

                    break;

                case ItemTypes.BeamDiagonalRight:

                    item = new SpriteGameObject("beamDiagonalRight", -1);

                    break;

                case ItemTypes.Beam:

                    item = new SpriteGameObject("beam", -1);

                    break;

                case ItemTypes.BeamBolts:

                    item = new SpriteGameObject("beamBolts", -1);

                    break;

                case ItemTypes.BeamBoltsHoles:

                    item = new SpriteGameObject("beamBoltsHoles", -1);

                    break;

                case ItemTypes.BeamBoltsNarrow:

                    item = new SpriteGameObject("beamBoltsNarrow", -1);

                    break;

                case ItemTypes.BeamHoles:

                    item = new SpriteGameObject("beamHoles", -1);

                    break;

                case ItemTypes.BeamNarrow:

                    item = new SpriteGameObject("beamNarrow", -1);

                    break;

                case ItemTypes.DirtCaveBottomLeft:

                    item = new SpriteGameObject("dirtCaveBL", -1);

                    break;

                case ItemTypes.DirtCaveBottomRight:

                    item = new SpriteGameObject("dirtCaveBR", -1);

                    break;

                case ItemTypes.DirtCaveUpLeft:

                    item = new SpriteGameObject("dirtCaveUL", -1);

                    break;

                case ItemTypes.DirtCaveUpRight:

                    item = new SpriteGameObject("dirtCaveUR", -1);

                    break;

                case ItemTypes.DirtCaveBottom:

                    item = new SpriteGameObject("dirtCaveBottom", -1);

                    break;

                case ItemTypes.DirtCaveTop:

                    item = new SpriteGameObject("dirtCaveTop", -1);

                    break;

                case ItemTypes.DirtCaveSpikeBottom:

                    item = new SpriteGameObject("dirtCaveSpikeBottom", -1);

                    break;

                case ItemTypes.DirtCaveSpikeTop:

                    item = new SpriteGameObject("dirtCaveSpikeTop", -1);

                    break;

                case ItemTypes.Dirt:

                    item = new SpriteGameObject("slice27_27", -1);

                    break;

                case ItemTypes.CanePink:

                    item = new SpriteGameObject("canePink", -1);

                    break;

                case ItemTypes.CanePinkSmall:

                    item = new SpriteGameObject("canePinkSmall", -1);

                    break;

                case ItemTypes.CanePinkTop:

                    item = new SpriteGameObject("canePinkTop", -1);

                    break;

                case ItemTypes.CanePinkTop2:

                    item = new SpriteGameObject("canePinkTopAlt", -1);

					break;

                case ItemTypes.Heart:

                    item = new SpriteGameObject("heart", -1);

                    break;

                case ItemTypes.HillCaneChoco:

                    item = new SpriteGameObject("hillCaneChoco", -1);

                    break;

                case ItemTypes.HillCaneChocoTop:

                    item = new SpriteGameObject("hillCaneChocoTop", -1);

                    break;

                case ItemTypes.HillCaneGreen:

                    item = new SpriteGameObject("hillCaneGreen", -1);

                    break;

                case ItemTypes.HillCaneGreenTop:

                    item = new SpriteGameObject("hillCaneGreenTop", -1);

                    break;

                case ItemTypes.HillCanePink:

                    item = new SpriteGameObject("hillCanePink", -1);

                    break;

                case ItemTypes.HillCanePinkTop:

                    item = new SpriteGameObject("hillCanePinkTop", -1);

                    break;

                case ItemTypes.HillCaneRed:

                    item = new SpriteGameObject("hillCaneRed", -1);

                    break;

                case ItemTypes.HillCaneRedTop:

                    item = new SpriteGameObject("hillCaneRedTop", -1);

                    break;

                case ItemTypes.LollipopBase:

                    item = new SpriteGameObject("lollipopBase", -1);

                    break;

                case ItemTypes.LollipopBaseBeige:

                    item = new SpriteGameObject("lollipopBaseBeige", -1);

                    break;

                case ItemTypes.LollipopBaseBrown:

                    item = new SpriteGameObject("lollipopBaseBrown", -1);

                    break;

                case ItemTypes.LollipopBaseCake:

                    item = new SpriteGameObject("lollipopBaseCake", -1);

                    break;

                case ItemTypes.LollipopBasePink:

                    item = new SpriteGameObject("lollipopBasePink", -1);

                    break;

                case ItemTypes.LollipopFruitGreen:

                    item = new SpriteGameObject("lollipopFruitGreen", -1);

                    break;

                case ItemTypes.LollipopFruitRed:

                    item = new SpriteGameObject("lollipopFruitRed", -1);

                    break;

                case ItemTypes.LollipopFruitYellow:

                    item = new SpriteGameObject("lollipopFruitYellow", -1);

                    break;

                case ItemTypes.LollipopGreen:

                    item = new SpriteGameObject("lollipopGreen", -1);

                    break;

                case ItemTypes.LollipopRed:

                    item = new SpriteGameObject("lollipopRed", -1);

                    break;

                case ItemTypes.LollipopWhiteGreen:

                    item = new SpriteGameObject("lollipopWhiteGreen", -1);

                    break;

                case ItemTypes.LollipopWhiteRed:

                    item = new SpriteGameObject("lollipopWhiteRed", -1);

                    break;
            }

            return item;
        }

        private void InitializeItemByType(SpriteGameObject item, ItemTypes itemType, Vector2 tilePosition, LevelEditor.Tile tile)
        {
            switch (itemType)
            {
                case ItemTypes.Player:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite);
                    item.startPosition = item.position;

                    playerTile = tile;

                    break;

                case ItemTypes.Spikes:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite) + new Vector2(0, item.HalfTextureHeight + 2);

                    break;

                case ItemTypes.SpikesLeft:

                    item.position = tilePosition;

                    break;

                case ItemTypes.SpikesRight:

                    item.position = tilePosition + new Vector2(item.Sprite.Sprite.Width + 4, 0);

                    break;

                case ItemTypes.SpikesRoof:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite) - new Vector2(0, item.HalfTextureHeight + 2);

                    break;

                case ItemTypes.FallingSpikes:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite) - new Vector2(0, item.HalfTextureHeight + 1.99f);
                    item.startPosition = item.position;

                    break;

                case ItemTypes.Frog:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite);
                    item.startPosition = item.position;

					break;

                case ItemTypes.ExplanationMan:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite);
                    item.startPosition = item.position;

                    break;

                default:

                    item.position = GetRelativeTilePosition(tilePosition, item.Sprite.Sprite) + item.Origin;

                    break;
            }
        }
    }
}
