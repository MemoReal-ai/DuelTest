using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Infrastructure
{
    public class DataHandler : MonoBehaviour
    {
        [SerializeField] private List<HeroWinData> _heroes;

        public void SaveData()
        {
            foreach (var hero in _heroes)
            {
                PlayerPrefs.SetInt(hero.Key, hero.HeroConfig.WinCount);
            }

            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            foreach (var hero in _heroes)
            {
                hero.HeroConfig.SetWinCount(PlayerPrefs.GetInt(hero.Key, hero.HeroConfig.WinCount));
            }
        }
    }
}