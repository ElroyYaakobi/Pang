using System;
using ElroyYa.Pang.Entities.Ammo;
using ElroyYa.Pang.Entities.ItemDrop;
using ElroyYa.Pang.Level;
using Game.Scripts.Level;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Player
{
    /// <summary>
    /// Control the logic of the player.
    /// </summary>
    public class PlayerController : EntityController<PlayerModel, PlayerView>
    {
        /// <summary>
        /// Handle player movement
        /// </summary>
        private void FixedUpdate()
        {
            if (!Model.IsAlive) return;

            var dir = GameInput.GetHorizontalInput();
            
            // movement speed * delta time is encapsulated to reduce multiplication operations
            View.SetPosition(View.GetPosition() + new Vector2(dir, 0) * (Model.MovementSpeed * Time.fixedDeltaTime),
                dir);
        }

        /// <summary>
        /// Handle player weapon (it's done in Update to get accurate input)
        /// </summary>
        private void Update()
        {
            if (Model.IsAlive && GameInput.GetIsFire())
            {
                Fire();
            }
            
            Model.RateOfFireCounter++;
        }

        private void Fire()
        {
            // ensure rate of fire
            if (Model.RateOfFireCounter < Model.UsedAmmo.RateOfFire)
                return;

            var ammo = AmmoPool.Instance.Get(Model.UsedAmmo.Index).GetComponent<AmmoController>();
            ammo.Fire(View.GetPosition());
            
            Model.RateOfFireCounter = 0;
        }
        
        /// <summary>
        /// Handle player intersection (ball or ammo/ anything else)
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Model.IsAlive) return;
            
            if (other.CompareTag(LevelConstants.ItemDropTag))
            {
                var dropCarrier = other.GetComponent<ItemDropCarrier>();
                ProcessItemDrop(dropCarrier.Model.Data);
                return;
            }

            if (other.CompareTag(LevelConstants.BallTag))
            {
                if (Model.HasShield)
                {
                    Model.HasShield = false;
                    View.OnShield(false);
                    
                    return;
                }

                Model.IsAlive = false;
                    View.OnDead();
                    LevelStateController.Instance.ResetLevel();
            }
        }

        /// <summary>
        /// Process an incoming item drop using its provided data (ammo change/ shield)
        /// </summary>
        /// <param name="data"></param>
        private void ProcessItemDrop(string data)
        {
            if (data == "shield")
            {
                Model.HasShield = true;
                View.OnShield(true);
                
                return;
            }

            if (!data.StartsWith("ammo_")) return;

            var ammoIndex = data[data.Length - 1].ToString(); // transform to string so we don't get the char value
            Model.UsedAmmo = AmmoDatabase.Instance.AmmoTypes[Convert.ToInt32(ammoIndex)];
        }
    }
}
