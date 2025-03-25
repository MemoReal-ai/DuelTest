using System.Collections;
using Game.Logic.Infrastructure;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class BushDebuff : IDebuff
    {
        private readonly CoroutineLauncher _launcher;
        private readonly DebuffBushConfig _config;
        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public BushDebuff(DebuffBushConfig config,CoroutineLauncher launcher)
        {
            _launcher = launcher;
            _config = config;
        }

        public void Execute(Hero hero)
        {
            var bush = Random.value;

            if (_config.BushChance > bush && _isDebuffed == false)
            {
                _waitForSeconds = new WaitForSeconds(_config.DurationDebuff);
                _launcher.LaunchCoroutine( DebuffCoroutine(hero));
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;

            hero.IncreaseAttackCooldown(_config.DurationDebuff);
            Debug.Log($"Применен эффект Bush на  {hero.name.Replace("(Clone)", string.Empty)}");

            yield return _waitForSeconds;
            hero.RestoreAttackCooldown();
            _isDebuffed = false;
        }
    }
}