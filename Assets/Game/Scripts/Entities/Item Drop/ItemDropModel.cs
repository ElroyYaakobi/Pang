using System;
using UnityEngine;

namespace ElroyYa.Pang.Entities.ItemDrop
{
    [Serializable]
    public class ItemDropModel
    {
        [SerializeField]
        private string data;

        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private Vector3 scale;

        public string Data => data;
        public Sprite Sprite => sprite;
        public Vector3 Scale => scale;
    }
}