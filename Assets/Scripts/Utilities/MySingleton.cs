using UnityEngine;

namespace Utilities
{
    public class MySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Debug.LogError($"There is an instance of the {typeof(T).FullName}");

                if (Instance.gameObject) Destroy(Instance.gameObject);
                
                Instance = this as T;
            }
        }
    }
}
