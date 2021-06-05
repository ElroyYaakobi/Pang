using UnityEngine;

namespace ElroyYa.Pang.Utilities
{
    /// <summary>
    /// A singleton is an object that exists once in the scene.
    /// Allows static access to that instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance) return instance;
                instance = FindObjectOfType<T>();
                return instance;
            }
        }

        protected virtual void OnEnable()
        {
            if (instance && instance != this)
                Destroy(gameObject);

            instance = this as T;
        }

        protected virtual void OnDisable()
        {
            if (instance == this)
                instance = null;
        }
    }
}