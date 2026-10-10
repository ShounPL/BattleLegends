using System;
using System.IO;
using UnityEngine;

namespace BattleLegends
{
    public static class SaveSystem
    {
        private const string SaveFileName = "/battlelegends.save.json";

        public static void Save(PlayerProgress progress)
        {
            string json = JsonUtility.ToJson(progress, true);
            File.WriteAllText(Application.persistentDataPath + SaveFileName, json);
        }

        public static PlayerProgress Load()
        {
            string path = Application.persistentDataPath + SaveFileName;
            if (!File.Exists(path))
            {
                return new PlayerProgress();
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerProgress>(json);
        }
    }

    [Serializable]
    public class PlayerProgress
    {
        public int gold = 1200;
        public int gems = 150;
        public int accountLevel = 1;
        public int xp = 0;
        public int requiredXp = 100;
        public int heroLevel = 1;
    }
}
