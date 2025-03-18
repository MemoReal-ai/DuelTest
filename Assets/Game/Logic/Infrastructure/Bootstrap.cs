using Game.Logic.UI.WinPanelView;
using UnityEngine;

namespace Game.Logic.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private RestartButton _restartButton;
        private void Awake()
        {
            var sceneHandler = new SceneHandler();
            _restartButton.Initialize(sceneHandler);
            
        }
    }
}
