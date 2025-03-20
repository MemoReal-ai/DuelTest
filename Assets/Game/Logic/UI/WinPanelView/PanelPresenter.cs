using System.Collections.Generic;
using Game.Logic.Heroes;

namespace Game.Logic.UI.WinPanelView
{
    public class PanelPresenter
    {
        private readonly WinPanelView _winPanelView;
        private readonly IEnumerable<Hero> _heroes;

        public PanelPresenter(WinPanelView winPanelView, IEnumerable<Hero> heroes)
        {
            _winPanelView = winPanelView;
            _heroes = heroes;
            _winPanelView.gameObject.SetActive(false);
        }

        public void Enable()
        {
            foreach (var hero in _heroes)
            {
                hero.OnWin += Show;
            }
        }

        public void Disable()
        {
            foreach (var hero in _heroes)
            {
                hero.OnWin -= Show;
            }
        }

        private void Show(Hero hero)
        {
            _winPanelView.gameObject.SetActive(true);
            _winPanelView.ShowWinName(hero.name);
        }
    }
}