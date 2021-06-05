using System;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ammo
{
    /// <summary>
    /// Holds information about an ammo type 
    /// </summary>
    [Serializable]
    public class AmmoModel : MonoBehaviour, IEntityModel
    {
        [SerializeField]
        private byte index;
        
        [SerializeField]
        private int rateOfFire = 50;

        [SerializeField]
        private float speed = 10;
        
        /// <summary>
        /// TODO: Automatically read from db instead of manually assigning.
        /// </summary>
        public byte Index => index;
        public int RateOfFire => rateOfFire;
        public float Speed => speed;
    }
}