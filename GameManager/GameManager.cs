using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;
using Battlestar;

namespace GameManager
{
    public static class GameManager
    {
        #region Declarations
        public static int Score { get; set; }
        public static int CurrentWave { get; set; }
        public static int CurrentDifficulty { get; set; }
        public static ObservableCollection<int> itemCollection = new ObservableCollection<int>();
        #endregion

        #region Public Methods
        public static void StartNewWave()
        {
            CurrentWave++;
            CurrentDifficulty += 10;
        }

        public static void StartNewGame()
        {
			Battlestar.BattleStar.ResetHullState();
			EnemyManager.EnemyManager.Active = true;
			CurrentDifficulty = 0;
            CurrentWave = 0;
            Score = 0;
            StartNewWave();
        }

        public static void UpdateScore(EnemyManager.Enemy enemy)
        {
            Score += enemy.MaxHealth;
        }

        private async static void EndGame()
        {
			EnemyManager.EnemyManager.Active = false;
			await LoadScores();
            var hasTobeStored = false;
            foreach(int score in itemCollection){
                if(score < Score)
                {
                    hasTobeStored = true;
                    break;
                }
            }
            if (itemCollection.Count <= 10 || hasTobeStored)
            {
                itemCollection.Add(Score);
                await SaveScores();
            }
        }

        public async static Task<ObservableCollection<int>> LoadScores()
        {
            StorageFile localFile;
			try
			{
				localFile = await ApplicationData.Current.LocalFolder.GetFileAsync("localData.xml");
			}
			catch (FileNotFoundException)
			{
				localFile = null;
			}
			if (localFile != null)
			{
				string localData = await FileIO.ReadTextAsync(localFile);
				if (!(localData == ""))
					itemCollection = ObjectSerializer<ObservableCollection<int>>.FromXml(localData);
			}
            return itemCollection;
        }

        public async static Task<ObservableCollection<int>> SaveScores()
        {
            string localData = ObjectSerializer<ObservableCollection<int>>.ToXml(itemCollection);
            if (!string.IsNullOrEmpty(localData))
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("localData.xml", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(localFile, localData);
            }
            return itemCollection;
        }

		public static void Update(GameTime gameTime)
		{
			if (BattleStar.GetHullState() < 1)
			{
				EndGame();
			}
			if (!EnemyManager.EnemyManager.Active)
			{
				StartNewWave();
			}
		}
        #endregion

    }
}
