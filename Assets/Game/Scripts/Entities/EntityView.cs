using UnityEngine;

namespace ElroyYa.Pang.Entities
{
    public interface IEntityView
    {
        Vector2 GetPosition();
        void SetPosition(Vector2 position, float dir);
        void SetVelocity(Vector2 velocity);
    }

    [RequireComponent(typeof(IEntityModel), typeof(Rigidbody2D))]
    public abstract class EntityView<T> : MonoBehaviour, IEntityView
        where T : IEntityModel
    {
        protected T Model { get; private set; }
        protected Rigidbody2D Rigidbody { get; private set; }

        protected virtual void Awake()
        {
            Model = GetComponent<T>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public virtual Vector2 GetPosition() => Rigidbody.position;

        public virtual void SetPosition(Vector2 position, float dir)
        {
            Rigidbody.MovePosition(position);
            OnPositionChanged(position, dir);
        }
        
        public void SetVelocity(Vector2 velocity) => Rigidbody.velocity = velocity;

        /// <summary>
        /// Called when the position of the entity view has been modified
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="dir"></param>
        protected virtual void OnPositionChanged(Vector2 newPosition, float dir)
        {
        }
    }
}