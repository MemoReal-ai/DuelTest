using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Logic.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    
    
    }
}
