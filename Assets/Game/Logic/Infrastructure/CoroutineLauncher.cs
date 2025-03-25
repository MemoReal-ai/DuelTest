using System.Collections;
using UnityEngine;

namespace Game.Logic.Infrastructure
{
    public class CoroutineLauncher : MonoBehaviour
    {
        public void LaunchCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }
    }
}
