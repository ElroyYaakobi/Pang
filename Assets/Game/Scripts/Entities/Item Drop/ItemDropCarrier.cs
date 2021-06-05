using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Level;
using UnityEngine;

namespace ElroyYa.Pang.Entities.ItemDrop
{
    /// <summary>
    /// Carries an item drop model so that the player can figure out what it contains once interacted
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class ItemDropCarrier : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer itemSprite;

        [SerializeField]
        private float timeToLive = 3f;

        private Rigidbody2D Rigidbody { get; set; }

        public ItemDropModel Model { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            // reset freeze state once returns from pool
            Rigidbody.constraints = RigidbodyConstraints2D.None;
        }

        public void SetModel(Vector3 spawnPos, ItemDropModel model)
        {
            transform.position = spawnPos;
            itemSprite.transform.localScale = model.Scale;
            itemSprite.sprite = model.Sprite;
            Model = model;

            // disappear automatically in X seconds
            Invoke(nameof(ReturnToPool), timeToLive);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(LevelConstants.PlayerTag))
            {
                ReturnToPool();
            }

            // once we hit the floor, freeze rigidbody to prevent it from falling 
            // this is to avoid setting it to not-trigger
            if (other.CompareTag(LevelConstants.GroundTag))
            {
                Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        private void ReturnToPool()
        {
            ItemDropPool.Instance.Return(gameObject);
        }
    }
}