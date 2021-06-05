using ElroyYa.Pang.Entities.ItemDrop;
using UnityEngine;

namespace ElroyYa.Pang.Entities.Ball
{
    /// <summary>
    /// Provides computational data to be used for balls
    /// </summary>
    public static class BallCompute
    {
        public const float BallSpeed = 2f;
        
        /// <summary>
        /// Calculate the velocity of the ball depending on its size
        /// (Some hard coded values, couldn't figure out a fitting formula)
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetVelocity(BallModel model)
        {
            return new Vector2(0,
                model.Size >= 3f ? 11 :
                model.Size >= 1.5f ? 10 :
                model.Size >= .5f ? 8 :
                7);
        }

        /// <summary>
        /// Should the ball drop an item? currently set to random of 15%
        /// </summary>
        /// <returns></returns>
        public static bool ShouldDropItem() => Random.Range(0f, 1f) < .15f;

        public static ItemDropModel GetDrop(ItemDropModel[] possibleDrops) =>
            possibleDrops[Random.Range(0, possibleDrops.Length)];
    }
}