namespace Game.Logic.Heroes
{
    public interface IDebuffable
    {
        void DoDebuff(Hero hero, float duration);
    }
}