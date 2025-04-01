using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Logic.Heroes.Debuff
{
    public class BushDebuff : IDebuff
    {
        private readonly DebuffBushConfig _config;
        private bool _isDebuffed;

        public BushDebuff(DebuffBushConfig config)
        {
            _config = config;
        }

        public void Execute(Hero hero)
        {
            var bush = Random.value;

            if (_config.BushChance > bush && _isDebuffed == false)
            {
                _ = Debuff(hero);
            }
        }

        private async UniTask Debuff(Hero hero)
        {
            _isDebuffed = true;

            hero.IncreaseAttackCooldown(_config.DurationDebuff);
            Debug.Log($"Применен эффект Bush на  {hero.HeroConfig.Name}");

            await UniTask.Delay(TimeSpan.FromSeconds(_config.DurationDebuff));
            hero.RestoreAttackCooldown();
            _isDebuffed = false;
        }
    }
}