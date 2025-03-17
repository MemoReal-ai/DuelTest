using Game.Logic.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI.MVP_WinPanel
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private SceneHandler _sceneHandler;

        private void Start()
        {
            _button.onClick.AddListener(_sceneHandler.Restart);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_sceneHandler.Restart);
        }
    }
}