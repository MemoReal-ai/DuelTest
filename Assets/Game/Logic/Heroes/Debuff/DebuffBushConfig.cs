using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{[CreateAssetMenu(fileName = "DebuffBushConfig", menuName = "Config/Debuff/Bush/DebuffConfig")]
    public class DebuffBushConfig : ScriptableObject
    {
        [field:SerializeField, Range(0, 1)]
        public float BushChance { get; private set; } = 0.5f;
        [field: SerializeField, Min(1)]
        public float DurationDebuff { get; private set; } = 1f;
    }
}
