using System;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class EventRewardData
    {
        public string eventName = "Storm of Valor";
        public string description = "Win battles and dungeons to earn rewards.";
        public int progress = 0;
        public int target = 8;
        public int rewardGold = 200;
        public int rewardGems = 40;
        public bool claimed = false;
        public string endDate = "";
    }

    public class EventSystem : MonoBehaviour
    {
        public static EventSystem Instance { get; private set; }

        public EventRewardData eventData = new EventRewardData();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadEvent();
        }

        private void LoadEvent()
        {
            string json = PlayerPrefs.GetString("event_data", "");
            if (string.IsNullOrEmpty(json))
            {
                CreateDefaultEvent();
                return;
            }

            eventData = JsonUtility.FromJson<EventRewardData>(json);
            if (eventData == null)
            {
                CreateDefaultEvent();
            }
        }

        private void CreateDefaultEvent()
        {
            eventData = new EventRewardData
            {
                eventName = "Storm of Valor",
                description = "Wygrywaj bitwy i lochy, aby zdobyć nagrody.",
                progress = 0,
                target = 8,
                rewardGold = 200,
                rewardGems = 40,
                claimed = false,
                endDate = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd")
            };
            SaveEvent();
        }

        public void AddProgress(int amount)
        {
            if (eventData.claimed)
            {
                return;
            }

            eventData.progress = Mathf.Min(eventData.progress + amount, eventData.target);
            SaveEvent();
        }

        public bool CanClaimReward()
        {
            return eventData.progress >= eventData.target && !eventData.claimed;
        }

        public void ClaimReward()
        {
            if (!CanClaimReward())
            {
                return;
            }

            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.gold += eventData.rewardGold;
                ProgressionSystem.Instance.gems += eventData.rewardGems;
                ProgressionSystem.Instance.SaveProgress();
            }

            eventData.claimed = true;
            SaveEvent();
        }

        public void SaveEvent()
        {
            PlayerPrefs.SetString("event_data", JsonUtility.ToJson(eventData, true));
            PlayerPrefs.Save();
        }
    }
}
