using System;
using Game.Scripts.Level;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ammo
{
    public class AmmoController : EntityController<AmmoModel, AmmoView>
    {
        /// <summary>
        /// Fire the ammo, initialize our settings and fire.
        /// </summary>
        public void Fire(Vector2 origin)
        {
            // as this isn't called through the fixed update (input comes from Update) we need to set the position
            // directly through the transform instead of through the RigidBody
            transform.position = origin;
        }

        /// <summary>
        /// Simulate movement
        /// </summary>
        private void FixedUpdate()
        {
            View.SetPosition(View.GetPosition() + new Vector2(0, Model.Speed * Time.fixedDeltaTime), 0);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // if we hit a ball or the ceiling, return ourselves to pool
            if (!other.CompareTag(LevelConstants.CeilingTag) &&
                !other.CompareTag(LevelConstants.BallTag)) return;

            AmmoPool.Instance.Return(gameObject, Model.Index);
        }
    }
}