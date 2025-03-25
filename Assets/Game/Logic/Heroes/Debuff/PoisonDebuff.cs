using System.Collections;
using Game.Logic.Infrastructure;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class PoisonDebuff : IDebuff
    {
        private DebuffPoisonConfig _config;
        private CoroutineLauncher _launcher;
        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed = false;

        public PoisonDebuff(DebuffPoisonConfig config, CoroutineLauncher launcher)
        {
            _config = config; 
            _launcher = launcher;
        }

        public void Execute(Hero hero)
        {
            if (_isDebuffed == false)
            {
                _waitForSeconds = new WaitForSeconds(_config.DurationBetweenTick);

                _launcher.LaunchCoroutine(DebuffCoroutine(hero));
                Debug.Log($"Применен эффект Poison на {hero.name.Replace("(Clone)", string.Empty)}");
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
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
                yield return _waitForSeconds;
            }

            _isDebuffed = false;
        }
    }
}