using Game.Logic.Heroes;
using Game.Logic.SpawnerHeroes;
using UnityEngine;

namespace Game.Logic.UI.WinPanelView
{
    public class PanelUIFactory : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private WinPanelView _winPanelView;

        private PanelPresenter _presenter;

        private void Start()
        {
            _presenter = new PanelPresenter(_winPanelView, _spawner.ContainerHeroes);
            _presenter.Enable();
        }

        private void OnDestroy()
        {
            _presenter.Disable();
        }
    }
}