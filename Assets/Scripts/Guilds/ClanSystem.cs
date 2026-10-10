using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    [Serializable]
    public class ClanMember
    {
        public string playerId = "";
        public string playerName = "";
        public int role = 0; // 0 = Member, 1 = Officer, 2 = Leader
        public int contribution = 0;
    }

    [Serializable]
    public class ClanData
    {
        public string clanId = "clan_1";
        public string clanName = "Dragon Guard";
        public int level = 1;
        public int xp = 0;
        public int requiredXp = 100;
        public int clanBossHealth = 1200;
        public int clanBossDamage = 30;
        public int bossLevel = 1;
        public List<ClanMember> members = new List<ClanMember>();
    }

    public class ClanSystem : MonoBehaviour
    {
        public static ClanSystem Instance { get; private set; }

        public ClanData clan = new ClanData();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadClan();
        }

        private void LoadClan()
        {
            string json = PlayerPrefs.GetString("clan_data", "");
            if (!string.IsNullOrEmpty(json))
            {
                clan = JsonUtility.FromJson<ClanData>(json);
                return;
            }

            clan = new ClanData
            {
                clanId = "clan_1",
                clanName = "Dragon Guard",
                level = 1,
                xp = 0,
                requiredXp = 100,
                clanBossHealth = 1200,
                clanBossDamage = 30,
                bossLevel = 1,
                members = new List<ClanMember>
                {
                    new ClanMember { playerId = "player_1", playerName = "You", role = 2, contribution = 20 },
                    new ClanMember { playerId = "player_2", playerName = "Astra", role = 1, contribution = 15 },
                    new ClanMember { playerId = "player_3", playerName = "Varek", role = 0, contribution = 10 }
                }
            };

            SaveClan();
        }

        public void SaveClan()
        {
            PlayerPrefs.SetString("clan_data", JsonUtility.ToJson(clan, true));
            PlayerPrefs.Save();
        }

        public void AddClanXp(int amount)
        {
            clan.xp += amount;

            while (clan.xp >= clan.requiredXp)
            {
                clan.xp -= clan.requiredXp;
                clan.level++;
                clan.requiredXp = Mathf.RoundToInt(clan.requiredXp * 1.35f);
                clan.clanBossHealth += 150;
                clan.clanBossDamage += 10;
            }

            SaveClan();
        }

        public void AddMemberContribution(int amount)
        {
            foreach (var member in clan.members)
            {
                if (member.playerName == "You")
                {
                    member.contribution += amount;
                    break;
                }
            }

            AddClanXp(amount * 10);
            SaveClan();
        }
    }
}
