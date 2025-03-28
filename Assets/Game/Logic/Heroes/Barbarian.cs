using Game.Logic.Heroes.Debuff;
using UnityEngine;

namespace Game.Logic.Heroes
{
    public class Barbarian : Hero
    {
        [SerializeField] private DebuffBushConfig _debuffBushConfig;


        protected override void Start()
        {
            base.Start();
            Debuff = new BushDebuff(_debuffBushConfig);
        }
    }
}