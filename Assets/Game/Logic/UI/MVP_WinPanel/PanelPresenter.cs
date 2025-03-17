using Game.Logic.Heroes;
using UnityEngine;

namespace Game.Logic.UI.MVP_WinPanel
{
    public class PanelPresenter
    {
        private readonly GameObject _panel;
        private readonly WinPanelView _winPanelView;

        public PanelPresenter(GameObject panel, WinPanelView winPanelView)
        {
            _panel = panel;
            _winPanelView = winPanelView;
        }

        public void Show(Hero hero)
        {
            _panel.SetActive(true);
            _winPanelView.ShowWinName(hero);
        }
    }
}