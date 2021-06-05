using ElroyYa.Pang.Utilities;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ball
{
    /// <summary>
    /// A pool for balls preserving GC allocation
    /// </summary>
    public class BallPool : Singleton<BallPool>
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