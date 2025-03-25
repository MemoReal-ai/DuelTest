using Game.Logic.Infrastructure;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    [CreateAssetMenu(fileName = "DebuffConfig", menuName = "Config/Debuff/Weakly/DebuffConfig", order = 1)]
    public class DebuffWeaklyConfig : ScriptableObject
    {
        [field: SerializeField,Range(0,1)] public float PercentStatus { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}
