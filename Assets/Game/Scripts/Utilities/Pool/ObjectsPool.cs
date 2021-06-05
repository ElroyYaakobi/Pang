using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ElroyYa.Pang.Utilities
{
    /// <summary>
    /// A simple objects pool, holds several instances of the provided assets and grows on demand
    /// </summary>
    [Serializable]
    public class ObjectsPool
    {
        [Header("Pool")]
        [SerializeField]
        private GameObject objectPrefab;
        
        [SerializeField]
        private int initialPoolSize;

        [SerializeField]
        private Transform poolParent;
        
        private Queue<GameObject> AvailableInstances { get; set; }

        public ObjectsPool(GameObject prefab, int initialSize, Transform poolParent)
        {
            objectPrefab = prefab;
            initialPoolSize = initialSize;
            this.poolParent = poolParent;
        }

        /// <summary>
        /// Initialize the pool, call this method before accessing it for the first time
        /// </summary>
        public void Initialize()
        {
            AvailableInstances = new Queue<GameObject>(initialPoolSize);
            for (var i = 0; i < initialPoolSize; i++)
            {
                Return(AllocateNewInstance());
            }
        }

        /// <summary>
        /// Get an object from the pool (if empty, allocates a new one)
        /// </summary>
        /// <returns></returns>
        public GameObject Get()
        {
            // if pool is empty create a new instance first
            if (AvailableInstances.Count == 0)
            {
                return AllocateNewInstance();
            }

            var instance = AvailableInstances.Dequeue();
            instance.SetActive(true);
            return instance;
        }

        /// <summary>
        /// Return an item to the pool
        /// </summary>
        /// <param name="instance"></param>
        public void Return(GameObject instance)
        {
            instance.SetActive(false);
            instance.transform.SetParent(poolParent);
            AvailableInstances.Enqueue(instance);
        }
        
        /// <summary>
        /// Allocate a new pool instance
        /// </summary>
        /// <returns></returns>
        private GameObject AllocateNewInstance()
        {
            return Object.Instantiate(objectPrefab);
        }
    }
}