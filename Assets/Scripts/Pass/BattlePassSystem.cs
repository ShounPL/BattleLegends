using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class BattlePassReward
    {
        public int tier = 1;
        public string rewardTitle = "Tier Reward";
        public int goldReward = 0;
        public int gemReward = 0;
        public string itemName = "";
        public bool claimed = false;
    }

    [Serializable]
    public class BattlePassData
    {
        public int level = 1;
        public int xp = 0;
        public int xpToNext = 100;
        public List<BattlePassReward> rewards = new List<BattlePassReward>();
    }

    public class BattlePassSystem : MonoBehaviour
    {
        public static BattlePassSystem Instance { get; private set; }

        public BattlePassData data = new BattlePassData();
        public int maxLevel = 25;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }

        private void LoadData()
        {
            string json = PlayerPrefs.GetString("battlepass_data", "");
            if (string.IsNullOrEmpty(json))
            {
                CreateDefaultData();
                SaveData();
                return;
            }

            data = JsonUtility.FromJson<BattlePassData>(json);
            if (data == null || data.rewards == null || data.rewards.Count == 0)
            {
                CreateDefaultData();
            }
        }

        private void CreateDefaultData()
        {
            data = new BattlePassData
            {
                level = 1,
                xp = 0,
                xpToNext = 100,
                rewards = new List<BattlePassReward>()
            };

            for (int i = 1; i <= 10; i++)
            {
                int goldValue = 50 + i * 30;
                int gemValue = 5 + i * 3;
                data.rewards.Add(new BattlePassReward
                {
                    tier = i,
                    rewardTitle = "Tier " + i,
                    goldReward = goldValue,
                    gemReward = gemValue,
                    itemName = i % 2 == 0 ? "Rare Relic" : "Hero Token",
                    claimed = false
                });
            }
        }

        public void AddXp(int amount)
        {
            if (data.level >= maxLevel)
            {
                return;
            }

            data.xp += amount;

            while (data.xp >= data.xpToNext && data.level < maxLevel)
            {
                data.xp -= data.xpToNext;
                data.level++;
                data.xpToNext = Mathf.RoundToInt(data.xpToNext * 1.25f);
            }

            SaveData();
        }

        public bool ClaimReward(int tier)
        {
            var reward = data.rewards.Find(r => r.tier == tier);
            if (reward == null || reward.claimed || tier > data.level)
            {
                return false;
            }

            reward.claimed = true;

            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.gold += reward.goldReward;
                ProgressionSystem.Instance.gems += reward.gemReward;
                ProgressionSystem.Instance.SaveProgress();
            }

            SaveData();
            return true;
        }

        public void SaveData()
        {
            PlayerPrefs.SetString("battlepass_data", JsonUtility.ToJson(data, true));
            PlayerPrefs.Save();
        }
    }
}
