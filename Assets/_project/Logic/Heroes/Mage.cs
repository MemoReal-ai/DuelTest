using UnityEngine;
namespace _project.Logic.Heroes
{
    public class Mage:Heroes,IDebufable
    {
        [SerializeField,Range(0,1)] private int weakCountPersent=1;
        [SerializeField, Range(0,100)] private float weakDuration=10f;
        public void DoDebuff(Heroes hero,float duration)
        { hero.TakeDebuffWeakly(weakCountPersent,weakDuration); ;
            Debug.Log($"Примене эффект Weakly на {hero.GetType().Name}");
        }
    }
}