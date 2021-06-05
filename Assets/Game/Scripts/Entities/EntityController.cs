using UnityEngine;

namespace ElroyYa.Pang.Entities
{
    [RequireComponent(typeof(IEntityModel), typeof(IEntityView))]
    public abstract class EntityController<T, T1> : MonoBehaviour
        where T : IEntityModel
        where T1 : IEntityView
    {
        protected T Model { get; private set; }
        protected T1 View { get; private set; }

        protected virtual void Awake()
        {
            Model = GetComponent<T>();
            View = GetComponent<T1>();
        }
    }
}