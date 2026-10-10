using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class QuestDefinition
    {
        public string questId = "quest_battle_1";
        public string title = "Battle Rookie";
        public string description = "Win 3 arena battles.";
        public QuestType type = QuestType.WinBattle;
        public int targetAmount = 3;
        public int rewardGold = 100;
        public int rewardGems = 10;
        public bool completed = false;
        public int progress = 0;
    }

    public enum QuestType
    {
        WinBattle,
        ClearDungeon,
        EarnGold,
        EquipItem
    }

    [Serializable]
    public class QuestProgressData
    {
        public List<QuestDefinition> quests = new List<QuestDefinition>();
    }

    public class QuestSystem : MonoBehaviour
    {
        public static QuestSystem Instance { get; private set; }

        public List<QuestDefinition> quests = new List<QuestDefinition>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            CreateDefaultQuests();
        }

        private void CreateDefaultQuests()
        {
            if (quests.Count > 0)
            {
                return;
            }

            quests.Add(new QuestDefinition
            {
                questId = "battle_1",
                title = "Rookie Arena",
                description = "Win 3 arena battles.",
                type = QuestType.WinBattle,
                targetAmount = 3,
                rewardGold = 120,
                rewardGems = 10
            });

            quests.Add(new QuestDefinition
            {
                questId = "dungeon_1",
                title = "Cave Cleaner",
                description = "Clear 2 dungeons.",
                type = QuestType.ClearDungeon,
                targetAmount = 2,
                rewardGold = 180,
                rewardGems = 15
            });

            quests.Add(new QuestDefinition
            {
                questId = "equip_1",
                title = "Gearing Up",
                description = "Equip 2 items.",
                type = QuestType.EquipItem,
                targetAmount = 2,
                rewardGold = 150,
                rewardGems = 20
            });
        }

        public void RegisterProgress(QuestType type, int amount = 1)
        {
            foreach (var quest in quests)
            {
                if (quest.completed || quest.type != type)
                {
                    continue;
                }

                quest.progress += amount;
                if (quest.progress >= quest.targetAmount)
                {
                    quest.progress = quest.targetAmount;
                    quest.completed = true;
                    GrantReward(quest);
                }
            }
        }

        private void GrantReward(QuestDefinition quest)
        {
            if (ProgressionSystem.Instance == null)
            {
                return;
            }

            ProgressionSystem.Instance.gold += quest.rewardGold;
            ProgressionSystem.Instance.gems += quest.rewardGems;
            ProgressionSystem.Instance.SaveProgress();
        }
    }
}
