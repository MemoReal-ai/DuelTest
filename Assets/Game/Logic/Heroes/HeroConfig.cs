using UnityEngine;

namespace Game.Configs.Hero.Heroes
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField, Min(1)]
        public int Speed { get; private set; } = 1;

        [field: SerializeField, Min(1)]
        public float AttackCooldown { get; private set; } = 1f;

        [field: SerializeField, Min(1)]
        public float RangeAttack { get; private set; } = 1f;

        [field: SerializeField, Min(1)]
        public int MaxHealth { get; private set; } = 1;

        [field: SerializeField, Min(0)]
        public int Damage { get; private set; } = 1;

        public void DecreaseAttack(float percentDecrease)
        {
            Damage = (int)(Damage * (1 - percentDecrease));
        }

        public void IncreaseAttackCooldown(float timeToIncrease)
        {
            AttackCooldown += timeToIncrease;
        }

        public void RestoreDamage(int damageDefault)
        {
            Damage = damageDefault;
        }

        public void RestoreAttackCooldown(float attackCooldownDefault)
        {
            AttackCooldown = attackCooldownDefault;
        }
    }
}