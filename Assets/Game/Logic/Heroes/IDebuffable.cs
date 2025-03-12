namespace Game.Logic.Heroes
{
    public interface IDebuffable
    {
        void DoDebuff(Heroes hero, float duration);
    }
}