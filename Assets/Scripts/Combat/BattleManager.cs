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
        public bool battleFinished;
        public int roundNumber = 1;
        public int selectedHeroIndex = 0;
        public int lastGoldReward;
        public int lastXpReward;

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

        public void SetSelectedHero(int index)
        {
            selectedHeroIndex = Mathf.Clamp(index, 0, 2);
        }

        public void StartBattle()
        {
            if (BattleResultController.Instance != null)
            {
                BattleResultController.Instance.HideResult();
            }

            ClearBattle();
            SpawnHeroes();
            SpawnEnemies();
            battleActive = true;
            battleFinished = false;
            roundNumber = 1;
        }

        public void ReturnToMenu()
        {
            battleActive = false;
            battleFinished = false;
            lastGoldReward = 0;
            lastXpReward = 0;

            if (BattleResultController.Instance != null)
            {
                BattleResultController.Instance.HideResult();
            }

            ClearBattle();
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

        private HeroDefinition CreateHeroDefinition(int heroIndex)
        {
            string[] names = { "Astra", "Varek", "Lyra" };
            HeroClass[] classes = { HeroClass.Warrior, HeroClass.Assassin, HeroClass.Mage };
            string[] roles = { "Frontline Guardian", "Shadow Striker", "Arc Mage" };

            var definition = ScriptableObject.CreateInstance<HeroDefinition>();
            definition.heroName = names[heroIndex];
            definition.heroClass = classes[heroIndex];
            definition.rarity = HeroRarity.Rare;
            definition.maxHealth = 120 + heroIndex * 25;
            definition.attackDamage = 18 + heroIndex * 8;
            definition.defense = 4 + heroIndex;
            definition.attackSpeed = 1.2f + heroIndex * 0.15f;
            definition.moveSpeed = 3.5f + heroIndex * 0.25f;
            definition.primaryRole = roles[heroIndex];

            var first = ScriptableObject.CreateInstance<AbilityDefinition>();
            first.abilityName = heroIndex == 0 ? "Shield Bash" : heroIndex == 1 ? "Shadow Step" : "Arc Burst";
            first.abilityType = AbilityType.Damage;
            first.damageMultiplier = 1.3f + heroIndex * 0.2f;
            first.cooldown = 4f;
            first.range = 2.5f;
            first.description = "A burst damage skill.";

            var second = ScriptableObject.CreateInstance<AbilityDefinition>();
            second.abilityName = heroIndex == 0 ? "Guardian Ward" : heroIndex == 1 ? "Twin Dagger" : "Solar Flare";
            second.abilityType = AbilityType.Buff;
            second.damageMultiplier = 1.6f;
            second.cooldown = 6f;
            second.range = 3f;
            second.description = "High-output follow-up attack.";

            definition.abilities = new AbilityDefinition[] { first, second };
            return definition;
        }

        private void SpawnHeroes()
        {
            Color[] colors = { Color.cyan, Color.magenta, Color.green };
            var heroDefinition = CreateHeroDefinition(selectedHeroIndex);

            GameObject heroObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            heroObject.name = heroDefinition.heroName;
            heroObject.transform.position = new Vector3(-2f, 0f, 0f);

            var hero = heroObject.AddComponent<HeroController>();
            hero.Configure(heroDefinition);
            hero.transform.localScale = new Vector3(1f, 1.2f, 1f);

            var renderer = hero.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = colors[selectedHeroIndex];
            }

            heroes.Add(hero);
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

        public void ShowResult(bool victory)
        {
            if (BattleResultController.Instance == null)
            {
                return;
            }

            if (victory)
            {
                lastGoldReward = 150;
                lastXpReward = 45;
                BattleResultController.Instance.ShowResult(true, lastGoldReward, lastXpReward);
            }
            else
            {
                lastGoldReward = 60;
                lastXpReward = 15;
                BattleResultController.Instance.ShowResult(false, lastGoldReward, lastXpReward);
            }
        }

        private void Victory()
        {
            battleActive = false;
            battleFinished = true;
            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.AwardBattleRewards(150, 3);
            }

            ShowResult(true);
            Debug.Log("Victory! Rewards granted.");
        }

        private void EndBattle()
        {
            battleActive = false;
            battleFinished = true;
            if (ProgressionSystem.Instance != null)
            {
                ProgressionSystem.Instance.AwardBattleRewards(60, 1);
            }

            ShowResult(false);
            Debug.Log("Battle ended.");
        }
    }
}
