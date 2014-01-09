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

namespace GameManager
{
    static class GameManager
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
            CurrentDifficulty = 0;
            CurrentWave = 0;
            Score = 0;
            StartNewWave();
        }

        public static void updateScore(EnemyManager.Enemy enemy)
        {
            Score += enemy.MaxHealth;
        }

        public static void EndGame()
        {
            LoadScores();
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
                SaveScores();
            }
        }

        public async static void LoadScores()
        {
            StorageFile localFile;
            localFile = await ApplicationData.Current.LocalFolder.GetFileAsync("localData.xml");
            if (localFile != null)
            {
                string localData = await FileIO.ReadTextAsync(localFile);
                itemCollection = ObjectSerializer<ObservableCollection<int>>.FromXml(localData);
            }
        }

        public async static void SaveScores()
        {
            string localData = ObjectSerializer<ObservableCollection<int>>.ToXml(itemCollection);
            if (!string.IsNullOrEmpty(localData))
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("localData.xml", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(localFile, localData);
            }
        }
        #endregion

    }
}
