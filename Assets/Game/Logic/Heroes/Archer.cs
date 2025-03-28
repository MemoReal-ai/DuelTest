using Game.Logic.Heroes.Debuff;
using UnityEngine;

namespace Game.Logic.Heroes
{
    public class Archer : Hero
    {
        [SerializeField] private DebuffPoisonConfig _debuffPoisonConfig;

        protected override void Start()
        {
            base.Start();
            Debuff = new PoisonDebuff(_debuffPoisonConfig);
        }
    }
}