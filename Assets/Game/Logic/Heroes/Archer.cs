using UnityEngine;

namespace Game.Logic.Heroes
{
    public class Archer : Hero, IDebuffable
    {
        [SerializeField] private int _poisonDamage = 1;

        private float _poisonDuration;

        public void DoDebuff(Hero hero, float duration)
        {
            _poisonDuration = duration;
            hero.TakeDebuffPoison(_poisonDamage, _poisonDuration);
            Debug.Log($"Применен эффект Poison на {hero.GetType().Name}");
        }
    }
}