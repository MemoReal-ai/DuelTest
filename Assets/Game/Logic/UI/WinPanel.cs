using Game.Logic.SpawnerHeroes;
using TMPro;
using UnityEngine;

namespace Game.Logic.UI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private Spawner _spawner;

        private void Awake()
        {
            _winPanel.SetActive(false);
            foreach (var hero in _spawner.ConteinerHeroes) hero.OnWin += ShowWinName;
        }

        private void OnDestroy()
        {
            foreach (var hero in _spawner.ConteinerHeroes) hero.OnWin -= ShowWinName;
        }

        private void ShowWinName(Heroes.Heroes hero)
        {
            _winPanel.SetActive(true);
            _text.text = hero.GetType().Name + "\n winning";
        }
    }
}