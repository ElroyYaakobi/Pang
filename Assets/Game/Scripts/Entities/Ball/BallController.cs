using System;
using System.Collections.Generic;
using ElroyYa.Pang.Entities.ItemDrop;
using Game.Scripts.Level;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ball
{
    public delegate void BallDestroyed(BallModel model);
    
    /// <summary>
    /// Controls the logic of the ball
    /// (Splitting and Intersections)
    /// </summary>
    public class BallController : EntityController<BallModel, BallView>
    {
        public static BallDestroyed OnBallDestroyed;
        public static List<BallController> ActiveBalls = new List<BallController>();

        private void OnEnable()
        {
            ActiveBalls.Add(this);
        }

        private void OnDisable()
        {
            ActiveBalls.Remove(this);
        }

        private void FixedUpdate()
        {
            View.SetPosition(View.GetPosition() +
                             new Vector2(Model.Direction, 0) * (BallCompute.BallSpeed * Time.fixedDeltaTime),
                Model.Direction);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // if we hit the ground we want to reset the velocity of the ball
            if (other.CompareTag(LevelConstants.GroundTag))
            {
                View.SetVelocity(BallCompute.GetVelocity(Model));
                return;
            }

            // inverse the direction of the ball if we hit one of the walls
            if (other.CompareTag(LevelConstants.WallTag))
            {
                Model.Direction = -Model.Direction;
                return;
            }
            
            // if we hit a bullet, split (or destroy if too small)
            if (!other.CompareTag(LevelConstants.BulletTag)) return;
            
            Split();
        }

        /// <summary>
        /// Split the ball OR destroy if no life remaining
        /// </summary>
        private void Split()
        {
            DropItem();
            
            // no life remaining
            if (Model.Life <= 1)
            {
                ReturnToPull();
                return;
            }
            
            // create two balls
            for (int i = 0, dir = -1; i < 2; i++, dir += 2)
            {
                var ball = BallPool.Instance.Get().GetComponent<BallController>();
                ball.OnSplitFrom(this, dir);
            }

            ReturnToPull();
        }

        /// <summary>
        /// Return the ball to the pool (don't destroy it)
        /// </summary>
        private void ReturnToPull()
        {
            BallPool.Instance.Return(gameObject);
            OnBallDestroyed?.Invoke(Model);
        }

        /// <summary>
        /// If possible, drop an item
        /// </summary>
        private void DropItem()
        {
            if (!BallCompute.ShouldDropItem()) return;

            var dropObj = ItemDropPool.Instance.Get().GetComponent<ItemDropCarrier>();
            dropObj.SetModel(View.GetPosition(), BallCompute.GetDrop(Model.PossibleDrops));
        }

        private void OnSplitFrom(BallController splitFrom, int dir)
        {
            View.SetPosition(splitFrom.View.GetPosition(), 0);
            Model.PossibleDrops = splitFrom.Model.PossibleDrops;
            Model.Size = splitFrom.Model.Size / 2f;
            Model.Color = splitFrom.Model.Color;
            Model.Direction = dir;
            Model.Life = splitFrom.Model.Life - 1;
            View.Initialize();
        }
    }
}