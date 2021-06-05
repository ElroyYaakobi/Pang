using System.Collections.Generic;
using ElroyYa.Pang.Utilities;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ammo
{
    public class AmmoPool : Singleton<AmmoPool>
    {
        [SerializeField]
        private int initialPoolSize = 10;
        
        private ObjectsPool[] ammoPool;

        private void Awake()
        {
            var ammoTypes = AmmoDatabase.Instance.AmmoTypes;
            
            ammoPool = new ObjectsPool[ammoTypes.Length];
            for (var i = 0; i < ammoPool.Length; i++)
            {
                var pool = new ObjectsPool(ammoTypes[i].gameObject, initialPoolSize, transform);
                pool.Initialize();
                
                ammoPool[i] = pool;
            }
        }

        public GameObject Get(byte ammoIndex) => ammoPool[ammoIndex].Get();
        public void Return(GameObject obj, byte ammoIndex) => ammoPool[ammoIndex].Return(obj);
    }
}