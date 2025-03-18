using Game.Logic.Heroes;
using TMPro;
using UnityEngine;

namespace Game.Logic.UI.WinPanelView
{
    public class WinPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ShowWinName(string heroName)
        {
            _text.text = heroName.Replace("(Clone)", string.Empty) + "\n winning";
        }
    }
}