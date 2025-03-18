using Game.Logic.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI.WinPanelView
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private SceneHandler _sceneHandler;

      
        private void Start()
        {
            _button.onClick.AddListener(_sceneHandler.Restart);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_sceneHandler.Restart);
        }
        
        public void Initialize(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }

    }
}