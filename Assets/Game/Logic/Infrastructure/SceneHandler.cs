using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Logic.Infrastructure
{
    public class SceneHandler : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}