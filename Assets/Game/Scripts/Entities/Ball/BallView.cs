using System;
using UnityEngine;
using UnityEngine.UI;

namespace ElroyYa.Pang.Entities.Ball
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BallView : EntityView<BallModel>
    {
        private SpriteRenderer SpriteRenderer { get; set; }

        protected override void Awake()
        {
            base.Awake();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Initialize();
        }

        /// <summary>
        /// Setup the ball visibility from the model
        /// </summary>
        public void Initialize()
        {
            transform.localScale = Vector3.one * Model.Size;
            SpriteRenderer.color = Model.Color;
        }

        /// <summary>
        /// Specifically for the ball, we don't want to move using the rigidbody methods as that breaks our velocity
        /// calculations
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetPosition() => transform.position;

        /// <summary>
        /// Specifically for the ball, we don't want to move using the rigidbody methods as that breaks our velocity
        /// calculations
        /// </summary>
        /// <returns></returns>
        public override void SetPosition(Vector2 position, float dir) => transform.position = position;
    }
}