using System;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class DailyRewardState
    {
        public bool isClaimedToday = false;
        public string lastClaimDate = "";
        public int streak = 0;
    }

    public class DailyRewardSystem : MonoBehaviour
    {
        public static DailyRewardSystem Instance { get; private set; }

        public DailyRewardState state = new DailyRewardState();

        private const int RewardGold = 100;
        private const int RewardGems = 25;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadState();
        }

        public bool CanClaimReward()
        {
            if (string.IsNullOrEmpty(state.lastClaimDate))
            {
                return true;
            }

            var today = DateTime.Now.ToString("yyyy-MM-dd");
            return state.lastClaimDate != today;
        }

        public bool ClaimReward()
        {
            if (!CanClaimReward())
            {
                return false;
            }

            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.gold += RewardGold;
                ProgressionSystem.Instance.gems += RewardGems;
                ProgressionSystem.Instance.SaveProgress();
            }

            state.isClaimedToday = true;
            state.lastClaimDate = DateTime.Now.ToString("yyyy-MM-dd");
            state.streak += 1;
            SaveState();
            return true;
        }

        public void LoadState()
        {
            var progress = SaveSystem.Load();
            if (string.IsNullOrEmpty(progress.inventoryJson) && string.IsNullOrEmpty(progress.heroRosterJson))
            {
                state = new DailyRewardState();
                return;
            }

            var json = PlayerPrefs.GetString("daily_rewards_state", JsonUtility.ToJson(state));
            state = JsonUtility.FromJson<DailyRewardState>(json);
        }

        public void SaveState()
        {
            PlayerPrefs.SetString("daily_rewards_state", JsonUtility.ToJson(state));
            PlayerPrefs.Save();
        }
    }
}
