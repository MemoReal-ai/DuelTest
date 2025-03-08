
using _project.Logic.Heroes;
using TMPro;
using UnityEngine;


public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Spawner spawner;
    
    private void Start()
    {
        winPanel.SetActive(false);
        foreach (var hero in spawner.ConteinerHeroes)
        {
            hero.OnWin += ShowWniName;
        }
    }

    private void OnDestroy()
    { foreach (var hero in spawner.ConteinerHeroes)
        {
            hero.OnWin -= ShowWniName;
        }   
    }

    private void ShowWniName(Heroes hero)
    {  
        winPanel.SetActive(true);
        text.text = hero.GetType().Name+"\n winning";
    }
}

