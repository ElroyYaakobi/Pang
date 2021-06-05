using System;
using ElroyYa.Pang.Entities.ItemDrop;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ball
{
    /// <summary>
    /// Information regarding the ball such as size, direction and color
    /// </summary>
    public class BallModel : MonoBehaviour, IEntityModel
    {
        [SerializeField]
        private float size;
        
        [SerializeField]
        private Color color;

        [SerializeField]
        private float direction;

        // how much life does this ball has? reduced by one with every split.
        [SerializeField]
        private int life = 4;

        [SerializeField]
        private ItemDropModel[] possibleDrops;

        public float Size
        {
            get => size;
            set => size = value;
        }

        public Color Color
        {
            get => color;
            set => color = value;
        }
        
        public float Direction
        {
            get => direction;
            set => direction = value;
        }

        public int Life
        {
            get => life;
            set => life = value;
        }

        public ItemDropModel[] PossibleDrops
        {
            get => possibleDrops;
            set => possibleDrops = value;
        }
    }
}