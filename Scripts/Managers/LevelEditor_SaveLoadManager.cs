using Engine;
using Microsoft.Xna.Framework.Input;

namespace Arcono.Editor.Managers
{
    public class LevelEditor_SaveLoadManager : GameObject
    {
        public string NextLevelName { get; private set; }
        public string CurrentLevelName { get; private set; }

        private readonly SaveManager saveManager;
        private readonly Coin_LoadSaveManager coin_LoadSaveManager;
        private readonly LoadManager loadManager;
        private readonly LevelEditor_GridManager gridManager;

        private readonly string fileExtension = ".json";

        public LevelEditor_SaveLoadManager(LevelEditor_GridManager gridManager, Coin_LoadSaveManager coin_LoadSaveManager)
        {
            this.gridManager = gridManager;
            this.coin_LoadSaveManager = coin_LoadSaveManager;

            saveManager = new SaveManager();
            loadManager = new LoadManager();

            InitializeEvents();
        }

        public void Save(string name)
        {
            string fileName = GetFileName(name);

			LevelEditor.SaveData saveData = new LevelEditor.SaveData(gridManager.Grid.Items)
			{
				NextLevel = NextLevelName
			};

			saveManager.Save(fileName, saveData);
        }

        public void SaveCoin(string name)
        {
            string fileName = GetFileName(name);

			Coin_LoadSaveManager.SaveData saveData = new Coin_LoadSaveManager.SaveData
			{
				CollectedCoinPercentage = coin_LoadSaveManager.CollectedCoinPercentage,
				IndexLevel = coin_LoadSaveManager.IndexLevel,
				MaxCoinAmount = coin_LoadSaveManager.MaxCoinAmount
			};

			saveManager.Save(fileName, saveData);
        }

        public bool LoadCoin(string name)
        {
            string fileName = GetFileName(name);

            bool isLoaded = loadManager.Load<Coin_LoadSaveManager.SaveData>(fileName);

            if (isLoaded)
                CurrentLevelName = name;

            return isLoaded;
        }

        public bool Load(string name)
        {
            string fileName = GetFileName(name);

            bool isLoaded = loadManager.Load<LevelEditor.SaveData>(fileName);

            if (isLoaded)
                CurrentLevelName = name;

            return isLoaded;
        }

        public void AddSaveListener(SaveManager.SaveEvent receiver)
        {
            saveManager.OnSave += receiver;
        }

        public void AddLoadListener(LoadManager.LoadEvent receiver)
        {
            loadManager.OnLoad += receiver;
        }

        public void RemoveSaveListener(SaveManager.SaveEvent receiver)
        {
            saveManager.OnSave -= receiver;
        }

        public void RemoveLoadListener(LoadManager.LoadEvent receiver)
        {
            loadManager.OnLoad -= receiver;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            // Saving input
            if (inputHelper.KeyPressed(Keys.NumPad1))
            {
                Save("levelData");
            }
            else if (inputHelper.KeyPressed(Keys.NumPad2))
            {
                Save("levelData2");
            }
            else if (inputHelper.KeyPressed(Keys.NumPad3))
            {
                Save("levelData3");
            }
            else if (inputHelper.KeyPressed(Keys.NumPad4))
            {
                Save("levelData4");
            }

            // Loading input
            if (inputHelper.KeyPressed(Keys.F1))
            {
                Load("levelData");
            }
            else if (inputHelper.KeyPressed(Keys.F2))
            {
                Load("levelData2");
            }
            else if (inputHelper.KeyPressed(Keys.F3))
            {
                Load("levelData3");
            }
            else if (inputHelper.KeyPressed(Keys.F4))
            {
                Load("levelData4");
            }
        }

        private string GetFileName(string name)
        {
            return name + fileExtension;
        }

        private void InitializeEvents()
        {
            // Create grid on loading
            AddLoadListener((saveData) =>
                {
                    gridManager.CreateGrid(saveData as LevelEditor.SaveData);
                    NextLevelName = (saveData as LevelEditor.SaveData).NextLevel;
                }
            );
        }
    }
}
