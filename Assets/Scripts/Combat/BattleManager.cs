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
        public int roundNumber = 1;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

        public void StartBattle()
        {
            ClearBattle();
            SpawnHeroes();
            SpawnEnemies();
            battleActive = true;
            roundNumber = 1;
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

            for (int i = 0; i < names.Length; i++)
            {
                GameObject heroObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                heroObject.name = names[i];
                heroObject.transform.position = new Vector3(-4f + i * 2f, 0f, 0f);

                var hero = heroObject.AddComponent<HeroController>();
                var definition = ScriptableObject.CreateInstance<HeroDefinition>();
                definition.heroName = names[i];
                definition.heroClass = i == 0 ? HeroClass.Warrior : i == 1 ? HeroClass.Assassin : HeroClass.Mage;
                definition.rarity = HeroRarity.Rare;
                definition.maxHealth = 120 + i * 25;
                definition.attackDamage = 18 + i * 8;
                definition.defense = 4 + i;
                definition.attackSpeed = 1.2f + i * 0.15f;
                definition.moveSpeed = 3.5f + i * 0.25f;
                definition.primaryRole = "Frontline";

                var basic = ScriptableObject.CreateInstance<AbilityDefinition>();
                basic.abilityName = i == 0 ? "Shield Bash" : i == 1 ? "Shadow Step" : "Arc Burst";
                basic.abilityType = AbilityType.Damage;
                basic.damageMultiplier = 1.3f + i * 0.2f;
                basic.cooldown = 4f;
                basic.range = 2.5f;
                basic.description = "A burst damage skill.";

                var second = ScriptableObject.CreateInstance<AbilityDefinition>();
                second.abilityName = i == 0 ? "Guardian Ward" : i == 1 ? "Twin Dagger" : "Solar Flare";
                second.abilityType = AbilityType.Buff;
                second.damageMultiplier = 1.6f;
                second.cooldown = 6f;
                second.range = 3f;
                second.description = "High-output follow-up attack.";

                definition.abilities = new AbilityDefinition[] { basic, second };
                hero.Configure(definition);
                hero.transform.localScale = new Vector3(1f, 1.2f, 1f);

                var renderer = hero.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = colors[i];
                }

                heroes.Add(hero);
            }
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

        public void ManualHeroAttack(int index)
        {
            if (index < 0 || index >= heroes.Count)
            {
                return;
            }

            var hero = heroes[index];
            if (hero == null || !hero.gameObject.activeSelf)
            {
                return;
            }

            if (hero.target == null)
            {
                hero.target = hero.FindNearestEnemy();
            }

            if (hero.target != null)
            {
                hero.BasicAttack(hero.target);
            }
        }

        public void HeroCast(int index, int abilityIndex)
        {
            if (index < 0 || index >= heroes.Count)
            {
                return;
            }

            var hero = heroes[index];
            if (hero == null || !hero.gameObject.activeSelf)
            {
                return;
            }

            if (hero.definition == null || hero.definition.abilities == null || abilityIndex >= hero.definition.abilities.Length)
            {
                hero.BasicAttack(hero.target);
                return;
            }

            hero.UseAbility(hero.definition.abilities[abilityIndex], hero.target);
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
                ProgressionSystem.Instance.AwardBattleRewards(150, 3);
            }
            Debug.Log("Victory! Rewards granted.");
        }

        private void EndBattle()
        {
            battleActive = false;
            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.AwardBattleRewards(60, 1);
            }
            Debug.Log("Battle ended.");
        }
    }
}
