using Game.Logic.Infrastructure;
using Game.Logic.SpawnerHeroes;

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
        }

        public void Enable()
        {
            _presenter = new PanelPresenter(_winPanelView, _spawner.ContainerHeroes,_sceneHandler);
            _presenter.Enable();
        }

        public void Disable()
        {
            _presenter.Disable();
        }
    }
}