using UnityEngine;

namespace BattleLegends
{
    public class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectOfType<ProgressionSystem>() == null)
            {
                var progression = new GameObject("ProgressionSystem");
                progression.AddComponent<ProgressionSystem>();
            }

            if (FindObjectOfType<InventorySystem>() == null)
            {
                var inventory = new GameObject("InventorySystem");
                inventory.AddComponent<InventorySystem>();
            }

            if (FindObjectOfType<HeroRosterSystem>() == null)
            {
                var roster = new GameObject("HeroRosterSystem");
                roster.AddComponent<HeroRosterSystem>();
            }

            if (FindObjectOfType<ShopSystem>() == null)
            {
                var shop = new GameObject("ShopSystem");
                shop.AddComponent<ShopSystem>();
            }

            if (FindObjectOfType<QuestSystem>() == null)
            {
                var quest = new GameObject("QuestSystem");
                quest.AddComponent<QuestSystem>();
            }

            if (FindObjectOfType<DailyRewardSystem>() == null)
            {
                var daily = new GameObject("DailyRewardSystem");
                daily.AddComponent<DailyRewardSystem>();
            }

            if (FindObjectOfType<LeaderboardSystem>() == null)
            {
                var leaderboard = new GameObject("LeaderboardSystem");
                leaderboard.AddComponent<LeaderboardSystem>();
            }

            if (FindObjectOfType<ArenaMatchmaker>() == null)
            {
                var matchmaker = new GameObject("ArenaMatchmaker");
                matchmaker.AddComponent<ArenaMatchmaker>();
            }

            if (FindObjectOfType<ClanSystem>() == null)
            {
                var clan = new GameObject("ClanSystem");
                clan.AddComponent<ClanSystem>();
            }

            if (FindObjectOfType<ClanBossManager>() == null)
            {
                var clanBoss = new GameObject("ClanBossManager");
                clanBoss.AddComponent<ClanBossManager>();
            }

            if (FindObjectOfType<BattlePassSystem>() == null)
            {
                var pass = new GameObject("BattlePassSystem");
                pass.AddComponent<BattlePassSystem>();
            }

            if (FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
            }

            if (FindObjectOfType<BattleManager>() == null)
            {
                var battle = new GameObject("BattleManager");
                battle.AddComponent<BattleManager>();
            }

            if (FindObjectOfType<MainMenuController>() == null)
            {
                var menu = new GameObject("MainMenuController");
                menu.AddComponent<MainMenuController>();
            }
        }

        private void Start()
        {
            SceneLoader.LoadScene("MainMenu");
        }
    }
}
