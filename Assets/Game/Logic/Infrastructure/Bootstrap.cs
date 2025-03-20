using Game.Logic.SpawnerHeroes;
using Game.Logic.UI.WinPanelView;
using UnityEngine;

namespace Game.Logic.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private WinPanelView _winPanelView;
        [SerializeField] private Spawner _spawner;

        private PanelUIFactory _panelUIFactory;

        private void Awake()
        {
            var sceneHandler = new SceneHandler();
            _panelUIFactory = new PanelUIFactory(_spawner, _winPanelView,sceneHandler);
        }

        private void OnDestroy()
        {
            _panelUIFactory.Disable();
        }
    }
}