using Game.Logic.Heroes;
using UnityEngine;

namespace Game.Logic.UI.HeroView
{
    public class HeroUIFactory
    {
        private readonly Hero _hero;
        private readonly StatsHeroView _statsHeroView;

        private Presenter _presenter;

        public HeroUIFactory(Hero hero, StatsHeroView statsHeroView)
        {
            _hero = hero;
            _statsHeroView = statsHeroView;
            Enable();
        }

        private void Enable()
        {
             var stats = Object.Instantiate(_statsHeroView, _hero.transform);
            _presenter = new Presenter(stats, _hero);
            _presenter.Enable();
            _presenter.SetDamageView();
        }

        public void Disable()
        {
            _presenter.Disable();
        }
    }
}