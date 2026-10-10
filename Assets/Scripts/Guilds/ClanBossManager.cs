using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    public class ClanBossManager : MonoBehaviour
    {
        public static ClanBossManager Instance { get; private set; }

        public int bossHealth = 1200;
        public int bossDamage = 30;
        public bool battleActive;
        public HeroController clanHero;
        public EnemyController bossEnemy;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void StartClanBossFight()
        {
            if (ClanSystem.Instance == null)
            {
                return;
            }

            bossHealth = ClanSystem.Instance.clan.clanBossHealth;
            bossDamage = ClanSystem.Instance.clan.clanBossDamage;
            battleActive = true;
            ClearBossFight();
            SpawnClanHero();
            SpawnBoss();
        }

        public void ClearBossFight()
        {
            if (clanHero != null)
            {
                Destroy(clanHero.gameObject);
            }

            if (bossEnemy != null)
            {
                Destroy(bossEnemy.gameObject);
            }

            clanHero = null;
            bossEnemy = null;
        }

        private void Update()
        {
            if (!battleActive)
            {
                return;
            }

            if (clanHero == null || !clanHero.gameObject.activeSelf)
            {
                EndFight(false);
                return;
            }

            if (bossEnemy == null || !bossEnemy.gameObject.activeSelf)
            {
                EndFight(true);
                return;
            }
        }

        private void SpawnClanHero()
        {
            if (BattleManager.Instance == null)
            {
                return;
            }

            var heroDefinition = BattleManager.Instance.CreateSelectedHeroDefinition(BattleManager.Instance.selectedHeroIndex);
            GameObject heroObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            heroObject.name = heroDefinition.heroName + " (Clan)";
            heroObject.transform.position = new Vector3(-4f, 0f, 0f);
            heroObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            clanHero = heroObject.AddComponent<HeroController>();
            clanHero.Configure(heroDefinition);
            var renderer = clanHero.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.blue;
            }
        }

        private void SpawnBoss()
        {
            GameObject bossObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            bossObject.name = "Clan Boss";
            bossObject.transform.position = new Vector3(4f, 0f, 0f);
            bossObject.transform.localScale = new Vector3(1.8f, 2.2f, 1.8f);

            bossEnemy = bossObject.AddComponent<EnemyController>();
            bossEnemy.Configure("Clan Boss", bossHealth, bossDamage, 2.2f);

            var renderer = bossEnemy.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }
        }

        private void EndFight(bool victory)
        {
            battleActive = false;

            if (victory && ClanSystem.Instance != null)
            {
                ClanSystem.Instance.AddMemberContribution(20);
                if (ProgressionSystem.Instance != null)
                {
                    ProgressionSystem.Instance.gold += 250;
                    ProgressionSystem.Instance.gems += 30;
                    ProgressionSystem.Instance.SaveProgress();
                }
            }

            ClearBossFight();
        }
    }
}
