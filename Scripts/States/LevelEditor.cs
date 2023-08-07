using Arcono.Editor.Managers;
using Engine;
using Microsoft.Xna.Framework;

namespace Arcono.Editor
{
    public class LevelEditor : GameObjectList
    {
        public int MinimumGridWidth => gridManager.minimumGridWidth;
        public int MinimumGridHeight => gridManager.minimumGridHeight;
        public int TileSize => gridManager.tileSize;
        public Vector2 SelectedTile => gridManager.SelectedTile;
        public Vector2 SelectedTilePosition => gridManager.SelectedTilePosition;
        public ItemTypes SelectedItem => itemManager.SelectedItem;
        public string NextLevelName => saveLoadManager.NextLevelName;
        public string CurrentLevelName => saveLoadManager.CurrentLevelName;
        public float CollectedCoinPercentage => coin_LoadSaveManager.CollectedCoinPercentage;

        public readonly int backgroundLengthX = 20;
        public readonly int backgroundLengthY = 30;

        private readonly LevelEditor_PlayManager playManager;
        private readonly LevelEditor_SaveLoadManager saveLoadManager;
        private readonly LevelEditor_GridManager gridManager;
        private readonly LevelEditor_ItemManager itemManager;
        private readonly UnlockItemManager unlockItemManager;
        private readonly Coin_LoadSaveManager coin_LoadSaveManager;

        private readonly Game game;

        public class SaveData
        {
            public ItemTypes[,] Tiles { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string NextLevel { get; set; }

            public SaveData()
            {

            }

            public SaveData(Tile[,] grid)
            {
                int gridWidth = grid.GetLength(0);
                int gridHeight = grid.GetLength(1);

                Width = gridWidth;
                Height = gridHeight;

                Tiles = new ItemTypes[gridWidth, gridHeight];

                // Sets the Tiles grid
                for (int i = 0; i < gridWidth; i++)
                {
                    for (int j = 0; j < gridHeight; j++)
                    {
                        Tiles[i, j] = grid[i, j].Type;
                    }
                }
            }
        }

        public class Tile
        {
            public ItemTypes Type { get; set; }
            public GameObject Item { get; set; }
        }

        public LevelEditor(Game game)
        {
            this.game = game;

            gridManager = new LevelEditor_GridManager();
            playManager = new LevelEditor_PlayManager(gridManager);
            coin_LoadSaveManager = new Coin_LoadSaveManager(this, unlockItemManager);
            unlockItemManager = new UnlockItemManager();
            saveLoadManager = new LevelEditor_SaveLoadManager(gridManager, coin_LoadSaveManager);

            itemManager = new LevelEditor_ItemManager(gridManager, unlockItemManager);
            itemManager.Reset();

            backgroundLengthX = 20;
            backgroundLengthY = 30;

            GenerateBackground();

            Add(itemManager);
            Add(new LevelEditor_ResizeGridManager(gridManager));
            Add(new LevelEditor_GridRenderer(gridManager));
            Add(new CameraSpeedManager());
            Add(saveLoadManager);
            Add(playManager);
            Add(gridManager);
            Add(new LevelEditor_InfoRenderer(itemManager));
            Add(unlockItemManager);
            Add(coin_LoadSaveManager);

            Reset();
        }

        #region Grid Methods

        public void CreateDefaultGrid()
        {
            gridManager.CreateDefaultGrid();
        }

        public void CreateGrid(int width, int height)
        {
            gridManager.CreateGrid(width, height);
        }

        public void ClearGrid()
        {
            gridManager.Clear();
        }

        public void CreateItem(ItemTypes itemType, int x, int y)
        {
            gridManager.CreateItem(itemType, x, y);
        }

        public void RemoveItem(int x, int y)
        {
            gridManager.RemoveItem(x, y);
        }

        public bool ItemExists(int x, int y)
        {
            return gridManager.ItemExists(x, y);
        }

        public Vector2 GetTilePosition(int x, int y)
        {
            return gridManager.GetTilePosition(x, y);
        }

        public void Play()
        {
            playManager.Play();
        }

        #endregion

        #region Saving/Loading Methods

        public void Save(string name)
        {
            saveLoadManager.Save(name);
        }

        public void SaveCoins(string name) 
        {
            saveLoadManager.SaveCoin(name);
        }

        public bool LoadCoin(string name) 
        {
            return saveLoadManager.LoadCoin(name);
        }

        public bool Load(string name)
        {
            return saveLoadManager.Load(name);
        }

        public void AddSaveListener(SaveManager.SaveEvent receiver)
        {
            saveLoadManager.AddSaveListener(receiver);
        }

        public void AddLoadListener(LoadManager.LoadEvent receiver)
        {
            saveLoadManager.AddLoadListener(receiver);
        }

        public void RemoveSaveListener(SaveManager.SaveEvent receiver)
        {
            saveLoadManager.RemoveSaveListener(receiver);
        }

        public void RemoveLoadListener(LoadManager.LoadEvent receiver)
        {
            saveLoadManager.RemoveLoadListener(receiver);
        }

        public void CalculateCollectPercentage(CoinManager coinManager, LevelEndManager levelEndManager)
        {
            coin_LoadSaveManager.CalculateCollectPercentage(coinManager, levelEndManager);
        }

        #endregion

        public override void Reset()
        {
            base.Reset();

            game.IsMouseVisible = true;

            //Camera Management
            GameEnvironment.camera = new Camera();
            GameEnvironment.cameraMover = new CameraMover(null)
            {
                IsLevelEditor = true
            };

            gridManager.Clear();
            gridManager.CreateDefaultGrid();

            coin_LoadSaveManager.LoadCoin();
            coin_LoadSaveManager.LoadCoinAmount(unlockItemManager);
        }

        private void GenerateBackground()
        {
            for (int i = 0; i < backgroundLengthX; i++)
            {
                for (int j = 0; j < backgroundLengthY; j++)
                {
                    SpriteGameObject background = new SpriteGameObject("Background", -1);
                    background.position = new Vector2(background.Width * i, background.Height * j);

                    Add(background);
                }
            }
        }
    }
}
