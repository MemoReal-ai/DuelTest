using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    [CreateAssetMenu(fileName = "DebuffPoisonConfig", menuName = "Config/Debuff/Poison/DebuffConfig")]
    public class DebuffPoisonConfig : ScriptableObject
    {
        [field: SerializeField]
        public int PoisonDamage { get; private set; } = 1;
        [field: SerializeField]
        public int PoisonTick { get; private set; } = 5;
        [field: SerializeField, Min(1)]
        public float DurationBetweenTick { get; private set; }
    }
}