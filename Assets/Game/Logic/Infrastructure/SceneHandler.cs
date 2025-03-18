using UnityEngine.SceneManagement;

namespace Game.Logic.Infrastructure
{
    public class SceneHandler
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}