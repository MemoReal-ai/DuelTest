using System.Collections;
using Game.Logic.Infrastructure;
using UnityEngine;

namespace Game.Logic.Heroes.Debuff
{
    public class WeakDebuff :IDebuff
    {
        private readonly DebuffWeaklyConfig _weaklyConfig;
        private CoroutineLauncher _launcher;
        
        private WaitForSeconds _waitForSeconds;
        private bool _isDebuffed;


        public WeakDebuff(DebuffWeaklyConfig weaklyConfig,CoroutineLauncher launcher)
        {
            _weaklyConfig = weaklyConfig;
            _launcher = launcher;
        }
        public void Execute(Hero hero)
        {
            _waitForSeconds = new WaitForSeconds(_weaklyConfig.Duration);

            if (_isDebuffed == false)
            {
                _launcher.LaunchCoroutine(DebuffCoroutine(hero));
            }
        }

        private IEnumerator DebuffCoroutine(Hero hero)
        {
            _isDebuffed = true;

            hero.DecreaseAttack(_weaklyConfig.PercentStatus);
            Debug.Log($"Примене эффект Weakly на {hero.name.Replace("(Clone)", string.Empty)}");

            yield return _waitForSeconds;
            hero.RestoreDamage();
            _isDebuffed = false;
        }
    }
}