using System;
using ElroyYa.Pang.Entities.Ammo;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Player
{
    /// <summary>
    /// Holds the information about the player
    /// </summary>
    public class PlayerModel : MonoBehaviour, IEntityModel
    {
        [SerializeField]
        private float movementSpeed = 5f;

        [SerializeField]
        private AmmoModel usedAmmo;

        public float MovementSpeed => movementSpeed;

        public AmmoModel UsedAmmo
        {
            get => usedAmmo;
            set => usedAmmo = value;
        }

        /// <summary>
        /// How many frames passed since last fire.
        /// </summary>
        public int RateOfFireCounter { get; set; } = 999;
        
        public bool HasShield { get; set; }

        public bool IsAlive { get; set; } = true;
    }
}