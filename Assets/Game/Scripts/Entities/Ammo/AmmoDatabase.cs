using System.Collections.Generic;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ammo
{
    [CreateAssetMenu(fileName = "AmmoDatabase", menuName = "Pang/Ammo Database", order = 0)]
    public class AmmoDatabase : ScriptableObject
    {
        private static AmmoDatabase instance;

        /// <summary>
        /// Allows us to hold a reference to the database more easily
        /// </summary>
        public static AmmoDatabase Instance
        {
            get
            {
                if (instance) return instance;
                
                instance = Resources.Load<AmmoDatabase>(nameof(AmmoDatabase));
                return instance;
            }
        }
        
        [SerializeField]
        private AmmoModel[] ammoTypes;

        public AmmoModel[] AmmoTypes => ammoTypes;
    }
}