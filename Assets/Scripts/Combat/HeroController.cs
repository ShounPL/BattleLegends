using UnityEngine;

namespace BattleLegends
{
    public class HeroController : CombatActor
    {
        public HeroDefinition definition;
        public int level = 1;
        public int xp = 0;

        private Vector3 movementInput;

        protected override void Awake()
        {
            base.Awake();
            isHero = true;
        }

        public void Configure(HeroDefinition heroDefinition)
        {
            definition = heroDefinition;
            actorName = heroDefinition.heroName;
            maxHealth = heroDefinition.maxHealth;
            health = maxHealth;
            damage = heroDefinition.attackDamage;
            defense = heroDefinition.defense;
            attackSpeed = heroDefinition.attackSpeed;
            moveSpeed = heroDefinition.moveSpeed;
            attackRange = 1.75f;

            if (bodyRenderer != null)
            {
                bodyRenderer.material.color = Color.cyan;
            }
        }

        private void Update()
        {
            if (!BattleManager.Instance || !BattleManager.Instance.battleActive)
            {
                return;
            }

            if (target == null || !target.gameObject.activeSelf)
            {
                target = FindNearestEnemy();
            }

            if (target == null)
            {
                return;
            }

            Vector3 targetDirection = target.transform.position - transform.position;
            float distance = targetDirection.magnitude;

            if (distance > attackRange)
            {
                movementInput = targetDirection.normalized;
                transform.position += movementInput * moveSpeed * Time.deltaTime;
            }
            else if (Time.time >= nextAttackTime)
            {
                BasicAttack(target);
            }
        }

        public CombatActor FindNearestEnemy()
        {
            CombatActor closest = null;
            float closestDistance = float.MaxValue;

            foreach (var enemy in BattleManager.Instance.enemies)
            {
                if (enemy == null || !enemy.gameObject.activeSelf)
                {
                    continue;
                }

                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = enemy;
                }
            }

            return closest;
        }

        public void CastFirstAbility()
        {
            if (definition == null || definition.abilities == null || definition.abilities.Length == 0)
            {
                BasicAttack(target);
                return;
            }

            UseAbility(definition.abilities[0], target);
        }

        public void CastSecondAbility()
        {
            if (definition == null || definition.abilities == null || definition.abilities.Length < 2)
            {
                BasicAttack(target);
                return;
            }

            UseAbility(definition.abilities[1], target);
        }
    }
}
