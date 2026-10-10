using UnityEngine;

namespace BattleLegends
{
    public class ProgressionSystem : MonoBehaviour
    {
        public static ProgressionSystem Instance { get; private set; }

        public int gold = 1200;
        public int gems = 150;
        public int heroLevel = 1;
        public int accountLevel = 1;
        public int xp = 0;
        public int requiredXp = 100;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }

        private void OnApplicationQuit()
        {
            SaveProgress();
        }

        public void AwardBattleRewards(int goldReward, int enemyCount)
        {
            gold += goldReward + enemyCount * 10;
            gems += 5;
            AddExperience(35 + enemyCount * 12);
            if (InventorySystem.Instance != null)
            {
                InventorySystem.Instance.AddRandomLoot(1);
            }
            SaveProgress();
        }

        public void AddExperience(int amount)
        {
            xp += amount;

            while (xp >= requiredXp)
            {
                xp -= requiredXp;
                accountLevel++;
                requiredXp = Mathf.RoundToInt(requiredXp * 1.3f);
                gold += 60;
                gems += 10;
            }
        }

        public void SaveProgress()
        {
            if (InventorySystem.Instance != null)
            {
                InventorySystem.Instance.SaveInventory();
            }

            SaveSystem.Save(new PlayerProgress
            {
                gold = gold,
                gems = gems,
                accountLevel = accountLevel,
                xp = xp,
                requiredXp = requiredXp,
                heroLevel = heroLevel,
                inventoryJson = InventorySystem.Instance != null ? JsonUtility.ToJson(new InventorySaveData { items = InventorySystem.Instance.items }, true) : ""
            });
        }

        public void LoadProgress()
        {
            var progress = SaveSystem.Load();
            gold = progress.gold;
            gems = progress.gems;
            accountLevel = progress.accountLevel;
            xp = progress.xp;
            requiredXp = progress.requiredXp;
            heroLevel = progress.heroLevel;

            if (InventorySystem.Instance != null)
            {
                InventorySystem.Instance.LoadInventory();
            }
        }
    }
}
