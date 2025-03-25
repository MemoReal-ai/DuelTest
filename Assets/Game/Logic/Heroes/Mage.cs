using Game.Logic.Heroes.Debuff;
using UnityEngine;

namespace Game.Logic.Heroes
{
    public class Mage : Hero
    {
        [SerializeField] private DebuffWeaklyConfig _debuffWeaklyConfig;

        protected override void Start()
        {
            base.Start();
            Debuff = new WeakDebuff(_debuffWeaklyConfig, Launcher);
        }
    }
}