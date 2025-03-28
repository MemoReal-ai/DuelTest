using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic.UI.WinPanelView
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [field: SerializeField]
        public Button RestartButton { get; private set; }

        public void ShowWinName(string heroName)
        {
            _text.text = heroName.Replace("(Clone)", string.Empty) + "\n winning";
        }
    }
}