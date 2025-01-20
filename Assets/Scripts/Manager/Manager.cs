using System.Collections;
using UnityEngine;

namespace Void.Manager
{
    public class Manager : MonoBehaviour
    {
        public static Manager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}