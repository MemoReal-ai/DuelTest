using Game.Logic.Infrastructure;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI.WinPanelView
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _restartButton;
        
        private SceneHandler _sceneHandler;

        private void Start()
        {
            _restartButton.onClick.AddListener(_sceneHandler.Restart);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(_sceneHandler.Restart);
        }

        public void Initialize(SceneHandler sceneHandler)
        {
            _sceneHandler = sceneHandler;
        }

        public void ShowWinName(string heroName)
        {
            _text.text = heroName.Replace("(Clone)", string.Empty) + "\n winning";
        }
    }
}