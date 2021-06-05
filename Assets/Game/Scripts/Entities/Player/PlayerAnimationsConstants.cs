using UnityEngine;

namespace ElroyYa.Pang.Entities.Player
{
    /// <summary>
    /// Holds constant data regarding the player animations such as animation parameters 
    /// </summary>
    public static class PlayerAnimationsConstants
    {
        public static readonly int IsMovingParameter = Animator.StringToHash("IsMoving");
        public static readonly int IsDeadParameter = Animator.StringToHash("IsDead");
    }
}