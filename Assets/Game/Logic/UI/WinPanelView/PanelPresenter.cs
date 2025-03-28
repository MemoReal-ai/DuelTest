using System.Collections.Generic;
using Game.Logic.Heroes;
using Game.Logic.Infrastructure;

namespace Game.Logic.UI.WinPanelView
{
    public class PanelPresenter
    {
        private readonly WinPanelView _winPanelView;
        private readonly IEnumerable<Hero> _heroes;
        private readonly SceneHandler _sceneHandler;

        public PanelPresenter(WinPanelView winPanelView, IEnumerable<Hero> heroes, SceneHandler sceneHandler)
        {
            _winPanelView = winPanelView;
            _heroes = heroes;
            _sceneHandler = sceneHandler;

            _winPanelView.gameObject.SetActive(false);
        }

        public void Enable()
        {
            foreach (var hero in _heroes)
            {
                hero.OnWin += Show;
            }

            _winPanelView.RestartButton.onClick.AddListener(_sceneHandler.Restart);
        }

        public void Disable()
        {
            foreach (var hero in _heroes)
            {
                hero.OnWin -= Show;
            }
            _winPanelView.RestartButton.onClick.RemoveListener(_sceneHandler.Restart);
        }

        private void Show(Hero hero)
        {
            _winPanelView.gameObject.SetActive(true);
            _winPanelView.ShowWinName(hero.name);
        }
    }
}