using UnityEngine;

namespace ElroyYa.Pang.Entities.Player
{
    /// <summary>
    /// Manages the visuals of the player
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class PlayerView : EntityView<PlayerModel>
    {
        private Animator Animator { get; set; }
        
        private SpriteRenderer SpriteRenderer { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void OnPositionChanged(Vector2 newPosition, float dir)
        {
            // determine if the player is moving by checking if the dir isn't 0
            var isMoving = !Mathf.Approximately(dir, 0);
            Animator.SetBool(PlayerAnimationsConstants.IsMovingParameter, isMoving);

            if (!isMoving) return;

            // cache transform for better perf
            var transformRef = transform;

            // set player orientation
            var currEuler = transformRef.eulerAngles;
            currEuler.y = dir < 0 ? 180 : 0;
            transformRef.eulerAngles = currEuler;
        }

        public void OnDead()
        {
            Animator.SetBool(PlayerAnimationsConstants.IsDeadParameter, true);
        }

        public void OnShield(bool isActive)
        {
            SpriteRenderer.color = isActive ? Color.cyan : Color.white;
        }
    }
}