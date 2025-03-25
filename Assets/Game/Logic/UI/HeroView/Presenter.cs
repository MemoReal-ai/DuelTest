using Game.Logic.Heroes;

namespace Game.Logic.UI.HeroView
{
    public class Presenter
    {
        private readonly Hero _hero;
        private readonly StatsHeroView _statsHeroView;

        public Presenter(StatsHeroView statsHeroView, Hero hero)
        {
            _statsHeroView = statsHeroView;
            _hero = hero;
        }

        public void Enable()
        {
            _hero.OnHealthChanged += SetHealth;
            _hero.OnDamageChanged += SetDamageView;
        }

        public void Disable()
        {
            _hero.OnHealthChanged -= SetHealth;
            _hero.OnDamageChanged -= SetDamageView;
        }

        public void SetDamageView()
        {
            _statsHeroView.ShowDamage(_hero.CurrentDamage);
        }

        private void SetHealth(float health)
        {
            _statsHeroView.UpdateHealth(health);
        }
    }
}