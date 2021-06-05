using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElroyYa.Pang
{
    /// <summary>
    /// System for handling mobile inputs while checking finger inputs inside defined movement boundaries
    /// </summary>
    public static class GameInput
    {
        private static readonly int ScreenWidth = Screen.width;
        private static readonly float FourthOfScreenWidth = ScreenWidth * .25f;
        
        /// <summary>
        /// Check if fire button is pressed (while ensuring its not the movement bars for mobile)
        /// </summary>
        /// <returns></returns>
        public static bool GetIsFire()
        {
            if (Input.touchCount == 0) return Input.GetButtonDown("Fire1"); // if no touch inputs, just return default input

            // if the finger is in the first 25% of the screen, return left horizontal, if in the last right 25% return right.
            // else nothing
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touchInputPosition = Input.GetTouch(i).rawPosition.x;

                // if any of those is true then the input is in the movement range
                if (touchInputPosition <= FourthOfScreenWidth || 
                    touchInputPosition >= ScreenWidth - FourthOfScreenWidth) continue;

                return true; // not in movement range! player fired!
            }

            return false;
        }

        /// <summary>
        /// Get horizontal input for the game (either keyboard or touch input)
        /// </summary>
        /// <returns></returns>
        public static float GetHorizontalInput()
        {
            var dir = Input.GetAxisRaw("Horizontal");
            return Input.touchCount == 0 ? dir : GetTouchInputHorizontal();
        }

        private static float GetTouchInputHorizontal()
        {
            if (Input.touchCount == 0) return 0f;

            // if the finger is in the first 25% of the screen, return left horizontal, if in the last right 25% return right.
            // else nothing
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touchInputPosition = Input.GetTouch(i).rawPosition.x;

                if (touchInputPosition <= FourthOfScreenWidth) return -1; // left side
                if (touchInputPosition >= ScreenWidth - FourthOfScreenWidth) return 1; // right side
            }

            return 0f;
        }
    }
}