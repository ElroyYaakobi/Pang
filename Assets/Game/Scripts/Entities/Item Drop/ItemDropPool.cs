using ElroyYa.Pang.Utilities;
using UnityEngine;

namespace ElroyYa.Pang.Entities.ItemDrop
{
    /// <summary>
    /// Item drop pool
    /// </summary>
    public class ItemDropPool : Singleton<ItemDropPool>
    {
        [SerializeField]
        private ObjectsPool pool;

        private void Awake()
        {
            pool.Initialize();
        }
        
        public GameObject Get() => pool.Get();
        public void Return(GameObject obj) => pool.Return(obj);
    }
}