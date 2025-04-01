using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Game.Logic.Infrastructure;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class WeakDebuff : IDebuff
    {
        private readonly DebuffWeaklyConfig _weaklyConfig;
        private bool _isDebuffed;


        public WeakDebuff(DebuffWeaklyConfig weaklyConfig)
        {
            _weaklyConfig = weaklyConfig;
        }

        public void Execute(Hero hero)
        {
            if (_isDebuffed == false)
            {
                _ = Debuff(hero);
            }
        }

        private async UniTask Debuff(Hero hero)
        {
            _isDebuffed = true;

            hero.DecreaseAttack(_weaklyConfig.PercentStatus);
            Debug.Log($"Примене эффект Weakly на {hero.HeroConfig.Name}");

            await UniTask.Delay(TimeSpan.FromSeconds(_weaklyConfig.Duration));
            hero.RestoreDamage();
            _isDebuffed = false;
        }
    }
}