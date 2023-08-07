using Engine;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;

namespace Arcono.Editor.Managers
{
    public class Coin_LoadSaveManager : GameObject
    {
        public int[] totalCoinPerLevel = new int[5];
        float[] loadedPercantage = new float[5];

        LoadManager loadManager;
        private readonly string fileExtension = ".json";

        public float CollectedCoinPercentage { get; set; }
        public float LoadedCoinPercentage { get; set; }
        public int MaxCoinAmount { get; set; }
        public int IndexLevel { get; set; }

        public int CollectedCoins { get; set; }

        private string CurrentFileName;

        public class SaveData
        {
            public float CollectedCoinPercentage { get; set; }

            public int IndexLevel { get; set; }

            public int MaxCoinAmount { get; set; }

            public SaveData()
            {

            }
        }

        public Coin_LoadSaveManager(LevelEditor levelEditor, UnlockItemManager unlockItemManager)
        {
            loadManager = new LoadManager();

            InitializeEvents();
        }

        // This function loads the data file of every level
        public void LoadCoin()
        {
            Load("level1coins");
            Load("level2coins");
            Load("level3coins");
            Load("level4coins");
            Load("level5coins");
        }

        // This functions loads a file
        public bool Load(string name)
        {
            string fileName = GetFileName(name);

            bool isLoaded = loadManager.Load<SaveData>(fileName);

            if (isLoaded)
                CurrentFileName = name;

            return isLoaded;
        }

        // This function calculates the percentage of total coins collected in a certain level.
        public void CalculateCollectPercentage(CoinManager coinManager, LevelEndManager levelEndManager)
        {
            CollectedCoinPercentage = coinManager.grabbedCoins / coinManager.coins.Count * 100;
            IndexLevel = levelEndManager.levelIndex;
            MaxCoinAmount = coinManager.coins.Count;

            if (CollectedCoinPercentage <= loadedPercantage[IndexLevel])
            {
                CollectedCoinPercentage = loadedPercantage[IndexLevel];
            }

            CalculateCoinAmount(coinManager);
        }

        // This calculates the coins using the completed percentage saved in the file.
        public void CalculateCoinAmount(CoinManager coinManager)
        {
            CollectedCoins = (int)Math.Round(coinManager.coins.Count * (CollectedCoinPercentage / 100));
        }

        // This function sets all collected coins per level.
        public void LoadCoinAmount(UnlockItemManager unlockItemManager)
        {
            unlockItemManager.totalCoins = 0;

            for (int i = 0; i < totalCoinPerLevel.Length; i++) 
            {
                float coinPercentage = loadedPercantage[i] / 100;
                CollectedCoins = (int)Math.Round(totalCoinPerLevel[i] * coinPercentage);
                unlockItemManager.coins[i] = CollectedCoins;
            }

            unlockItemManager.CalculateTotalCoinAmount();
        }

        // This function adds the object that listens to the load event.
        public void AddLoadListener(LoadManager.LoadEvent receiver)
        {
            loadManager.OnLoad += receiver;
        }

        // This function gets the file name 
        private string GetFileName(string name)
        {
            return name + fileExtension;
        }

        // This function gets certain data out of the saved file and sets it to certain variables.
        private void InitializeEvents()
        {          
            AddLoadListener((saveData) =>
            {
                IndexLevel = (saveData as SaveData).IndexLevel;
                loadedPercantage[IndexLevel] = (saveData as SaveData).CollectedCoinPercentage;
                totalCoinPerLevel[IndexLevel] = (saveData as SaveData).MaxCoinAmount;            
            }
            );
        }

    }
}
