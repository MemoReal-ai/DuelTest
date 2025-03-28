using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class PoisonDebuff : IDebuff
    {
        private readonly DebuffPoisonConfig _config;
        private bool _isDebuffed;

        public PoisonDebuff(DebuffPoisonConfig config)
        {
            _config = config;
        }

        public void Execute(Hero hero)
        {
            if (_isDebuffed == false)
            {
                _ = Debuff(hero);
                Debug.Log($"Применен эффект Poison на {hero.name.Replace("(Clone)", string.Empty)}");
            }
        }

        private async UniTask Debuff(Hero hero)
        {
            _isDebuffed = true;
            for (int i = 0; i < _config.PoisonTick; i++)
            {
                if (hero == null)
                {
                    break;
                }

                hero.TakeDamage(_config.PoisonDamage);
                Debug.Log($"Применен тик Poison на {hero.name.Replace("(Clone)", string.Empty)}");
                await UniTask.Delay(TimeSpan.FromSeconds(_config.DurationBetweenTick));
            }

            _isDebuffed = false;
        }
    }
}