using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance { get; private set; }

        public DungeonDefinition currentDungeon;
        public bool isRunning;

        private HeroController dungeonHero;
        private readonly List<EnemyController> dungeonEnemies = new List<EnemyController>();
        private int waveIndex;
        private bool bossSpawned;

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

        private void Update()
        {
            if (!isRunning || currentDungeon == null)
            {
                return;
            }

            if (dungeonHero == null || !dungeonHero.gameObject.activeSelf)
            {
                FinishDungeon(false);
                return;
            }

            if (dungeonEnemies.Count == 0 && !bossSpawned)
            {
                if (waveIndex < currentDungeon.enemyCount)
                {
                    SpawnWave();
                }
                else
                {
                    SpawnBoss();
                    bossSpawned = true;
                }
            }

            if (bossSpawned && AreAllEnemiesDead())
            {
                FinishDungeon(true);
            }
        }

        public void StartDungeon(DungeonDefinition dungeon)
        {
            if (dungeon == null)
            {
                return;
            }

            currentDungeon = dungeon;
            isRunning = true;
            waveIndex = 0;
            bossSpawned = false;

            ClearDungeonField();
            SpawnDungeonHero();
            SpawnWave();
        }

        private void SpawnDungeonHero()
        {
            var heroDefinition = CreateDungeonHeroDefinition();
            GameObject heroObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            heroObject.name = heroDefinition.heroName;
            heroObject.transform.position = new Vector3(-3f, 0f, 0f);

            dungeonHero = heroObject.AddComponent<HeroController>();
            dungeonHero.Configure(heroDefinition);
            dungeonHero.transform.localScale = new Vector3(1f, 1.2f, 1f);

            var renderer = dungeonHero.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.cyan;
            }
        }

        private HeroDefinition CreateDungeonHeroDefinition()
        {
            var def = ScriptableObject.CreateInstance<HeroDefinition>();
            int selectedIndex = BattleManager.Instance != null ? BattleManager.Instance.selectedHeroIndex : 0;
            string[] names = { "Astra", "Varek", "Lyra" };
            HeroClass[] classes = { HeroClass.Warrior, HeroClass.Assassin, HeroClass.Mage };
            string[] roles = { "Frontline Guardian", "Shadow Striker", "Arc Mage" };

            def.heroName = names[selectedIndex];
            def.heroClass = classes[selectedIndex];
            def.rarity = HeroRarity.Rare;
            def.maxHealth = 150 + selectedIndex * 25;
            def.attackDamage = 24 + selectedIndex * 8;
            def.defense = 6 + selectedIndex;
            def.attackSpeed = 1.25f + selectedIndex * 0.15f;
            def.moveSpeed = 4.0f + selectedIndex * 0.2f;
            def.primaryRole = roles[selectedIndex];

            var skillA = ScriptableObject.CreateInstance<AbilityDefinition>();
            skillA.abilityName = selectedIndex == 0 ? "Shield Bash" : selectedIndex == 1 ? "Shadow Step" : "Arc Burst";
            skillA.damageMultiplier = 1.6f;
            skillA.cooldown = 3.5f;
            skillA.range = 3f;
            skillA.description = "A stronger skill.";

            var skillB = ScriptableObject.CreateInstance<AbilityDefinition>();
            skillB.abilityName = selectedIndex == 0 ? "Guardian Ward" : selectedIndex == 1 ? "Twin Dagger" : "Solar Flare";
            skillB.damageMultiplier = 2.0f;
            skillB.cooldown = 5f;
            skillB.range = 3.5f;
            skillB.description = "Heavy burst.";

            def.abilities = new[] { skillA, skillB };
            return def;
        }

        private void SpawnWave()
        {
            if (currentDungeon == null)
            {
                return;
            }

            int enemiesToSpawn = Mathf.Min(2 + waveIndex, currentDungeon.enemyCount);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                string enemyName = currentDungeon.enemyNames[i % currentDungeon.enemyNames.Length];
                GameObject enemyObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                enemyObject.name = enemyName + " " + (i + 1);
                enemyObject.transform.position = new Vector3(5f + i * 2f, 0f, 0f);
                enemyObject.transform.localScale = new Vector3(1.2f, 1.5f, 1.2f);

                var enemy = enemyObject.AddComponent<EnemyController>();
                enemy.Configure(enemyName, currentDungeon.baseEnemyHealth + waveIndex * 25f, currentDungeon.baseEnemyDamage + waveIndex * 4f, currentDungeon.enemySpeed + waveIndex * 0.15f);
                dungeonEnemies.Add(enemy);
            }

            waveIndex++;
        }

        private void SpawnBoss()
        {
            if (currentDungeon == null)
            {
                return;
            }

            GameObject bossObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            bossObject.name = currentDungeon.bossName;
            bossObject.transform.position = new Vector3(7f, 0f, 0f);
            bossObject.transform.localScale = new Vector3(1.7f, 2.2f, 1.7f);

            var boss = bossObject.AddComponent<EnemyController>();
            boss.Configure(currentDungeon.bossName, currentDungeon.bossHealth, currentDungeon.bossDamage, currentDungeon.bossSpeed);

            var renderer = boss.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }

            dungeonEnemies.Add(boss);
        }

        private bool AreAllEnemiesDead()
        {
            for (int i = dungeonEnemies.Count - 1; i >= 0; i--)
            {
                if (dungeonEnemies[i] != null && dungeonEnemies[i].gameObject.activeSelf)
                {
                    return false;
                }
            }

            return true;
        }

        private void ClearDungeonField()
        {
            foreach (var enemy in dungeonEnemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }

            if (dungeonHero != null)
            {
                Destroy(dungeonHero.gameObject);
            }

            dungeonHero = null;
            dungeonEnemies.Clear();
            bossSpawned = false;
            waveIndex = 0;
        }

        private void FinishDungeon(bool victory)
        {
            if (!isRunning)
            {
                return;
            }

            isRunning = false;
            var reward = DungeonRewardTable.RollRewards(currentDungeon);

            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.gold += reward.gold;
                ProgressionSystem.Instance.xp += reward.xp;
            }

            if (DungeonResultController.Instance != null)
            {
                DungeonResultController.Instance.ShowResult(victory, reward.gold, reward.xp, reward.lootName);
            }

            ClearDungeonField();
        }
    }
}
