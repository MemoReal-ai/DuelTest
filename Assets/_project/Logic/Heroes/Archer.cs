using _project.Logic.Heroes;
using UnityEngine;
using UnityEngine.Serialization;

public class Archer : Heroes ,IDebufable
{
    [SerializeField] private float poisenDamage=1f;
    [SerializeField] private float poisenDuration=5f;
    public void DoDebuff(Heroes hero,float duration)
    {
        hero.TakeDebuffPoisen(poisenDamage, poisenDuration);
        Debug.Log($"Примене эффект Poisen на {hero.GetType().Name}");
    }
}
