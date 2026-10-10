using System.Collections.Generic;
using UnityEngine;

namespace BattleLegends
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        public List<HeroController> heroes = new List<HeroController>();
        public List<EnemyController> enemies = new List<EnemyController>();
        public bool battleActive;
        public int selectedHeroIndex = 0;

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

        public void SetSelectedHero(int index)
        {
            selectedHeroIndex = Mathf.Clamp(index, 0, 2);
        }

        public void StartBattle()
        {
            ClearBattle();
            SpawnHeroes();
            SpawnEnemies();
            battleActive = true;

            if (BattleResultController.Instance != null)
            {
                BattleResultController.Instance.HideResult();
            }
        }

        public void ReturnToMenu()
        {
            ClearBattle();
            battleActive = false;
            SceneLoader.LoadScene("MainMenu");
        }

        private void Update()
        {
            if (!battleActive)
            {
                return;
            }

            if (heroes.Count == 0 || enemies.Count == 0)
            {
                EndBattle();
                return;
            }

            if (AreAllHeroesDefeated())
            {
                EndBattle();
                return;
            }

            if (AreAllEnemiesDefeated())
            {
                Victory();
            }
        }

        private void ClearBattle()
        {
            foreach (var hero in heroes)
            {
                if (hero != null)
                {
                    Destroy(hero.gameObject);
                }
            }

            foreach (var enemy in enemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }

            heroes.Clear();
            enemies.Clear();
        }

        private void SpawnHeroes()
        {
            string[] names = { "Astra", "Varek", "Lyra" };
            Color[] colors = { Color.cyan, Color.magenta, Color.green };
            var heroDefinition = CreateSelectedHeroDefinition(selectedHeroIndex);

            GameObject heroObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            heroObject.name = heroDefinition.heroName;
            heroObject.transform.position = new Vector3(-2f, 0f, 0f);

            var hero = heroObject.AddComponent<HeroController>();
            hero.Configure(heroDefinition);

            var renderer = hero.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = colors[selectedHeroIndex];
            }

            heroes.Add(hero);
        }

        private HeroDefinition CreateSelectedHeroDefinition(int index)
        {
            string[] names = { "Astra", "Varek", "Lyra" };
            HeroClass[] classes = { HeroClass.Warrior, HeroClass.Assassin, HeroClass.Mage };
            string[] roles = { "Frontline Guardian", "Shadow Striker", "Arc Mage" };

            var def = ScriptableObject.CreateInstance<HeroDefinition>();
            def.heroName = names[index];
            def.heroClass = classes[index];
            def.rarity = HeroRarity.Rare;
            def.maxHealth = 120 + index * 25;
            def.attackDamage = 18 + index * 8;
            def.defense = 4 + index;
            def.attackSpeed = 1.2f + index * 0.15f;
            def.moveSpeed = 3.5f + index * 0.2f;
            def.primaryRole = roles[index];

            var first = ScriptableObject.CreateInstance<AbilityDefinition>();
            first.abilityName = index == 0 ? "Shield Bash" : index == 1 ? "Shadow Step" : "Arc Burst";
            first.abilityType = AbilityType.Damage;
            first.damageMultiplier = 1.3f + index * 0.2f;
            first.cooldown = 4f;
            first.range = 2.5f;

            var second = ScriptableObject.CreateInstance<AbilityDefinition>();
            second.abilityName = index == 0 ? "Guardian Ward" : index == 1 ? "Twin Dagger" : "Solar Flare";
            second.abilityType = AbilityType.Buff;
            second.damageMultiplier = 1.6f;
            second.cooldown = 6f;
            second.range = 3f;

            def.abilities = new AbilityDefinition[] { first, second };
            return def;
        }

        private void SpawnEnemies()
        {
            string[] names = { "Goblin Raider", "Bone Archer", "Wisp Fang" };

            for (int i = 0; i < names.Length; i++)
            {
                GameObject enemyObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                enemyObject.name = names[i];
                enemyObject.transform.position = new Vector3(4f + i * 2f, 0f, 0f);
                enemyObject.transform.localScale = new Vector3(1f, 1.4f, 1f);

                var enemy = enemyObject.AddComponent<EnemyController>();
                enemy.Configure(names[i], 90f + i * 20f, 11f + i * 4f, 2.4f + i * 0.2f);
                enemies.Add(enemy);
            }
        }

        private bool AreAllHeroesDefeated()
        {
            foreach (var hero in heroes)
            {
                if (hero != null && hero.gameObject.activeSelf)
                {
                    return false;
                }
            }

            return true;
        }

        private bool AreAllEnemiesDefeated()
        {
            foreach (var enemy in enemies)
            {
                if (enemy != null && enemy.gameObject.activeSelf)
                {
                    return false;
                }
            }

            return true;
        }

        private void Victory()
        {
            battleActive = false;

            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.AwardBattleRewards(150, 45);
            }

            if (BattleResultController.Instance != null)
            {
                BattleResultController.Instance.ShowResult(true, 150, 45);
            }
        }

        private void EndBattle()
        {
            battleActive = false;

            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.AwardBattleRewards(60, 15);
            }

            if (BattleResultController.Instance != null)
            {
                BattleResultController.Instance.ShowResult(false, 60, 15);
            }
        }
    }
}
