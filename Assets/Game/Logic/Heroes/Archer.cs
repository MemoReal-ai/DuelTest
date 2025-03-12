using UnityEngine;

namespace Game.Logic.Heroes
{
    public class Archer : Heroes, IDebuffable
    {
        [SerializeField] private int _poisonDamage = 1;
       
        private float _poisonDuration;

        public void DoDebuff(Heroes hero, float duration)
        {
            _poisonDuration = duration ;
            hero.TakeDebuffPoisen(_poisonDamage, _poisonDuration);
            Debug.Log($"Применен эффект Poisen на {hero.GetType().Name}");
        }
    }
}