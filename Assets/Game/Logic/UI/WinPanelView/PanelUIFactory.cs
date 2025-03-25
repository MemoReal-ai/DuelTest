using Game.Logic.Infrastructure;
using Game.Logic.SpawnerHeroes;
using UnityEngine;

namespace Game.Logic.UI.WinPanelView
{
    public class PanelUIFactory
    {
        private readonly Spawner _spawner;
        private readonly WinPanelView _winPanelView;
        private readonly SceneHandler _sceneHandler;

        private PanelPresenter _presenter;

        public PanelUIFactory(Spawner spawner, WinPanelView winPanelView, SceneHandler sceneHandler)
        {
            _spawner = spawner;
            _winPanelView = winPanelView;
            _sceneHandler = sceneHandler;
            Enable();
        }

        private void Enable()
        {
            var createPanel = Object.Instantiate(_winPanelView);
            createPanel.Initialize(_sceneHandler);
            _presenter = new PanelPresenter(createPanel, _spawner.ContainerHeroes);
            _presenter.Enable();
        }

        public void Disable()
        {
            _presenter.Disable();
        }
    }
}