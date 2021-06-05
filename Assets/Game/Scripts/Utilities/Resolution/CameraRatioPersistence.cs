using System;
using UnityEngine;

namespace ElroyYa.Pang.Utilities
{
    /// <summary>
    /// Using the specified boundaries, the system calculates the required aspect ratio so that the whole
    /// screen is covered. 
    /// </summary>
    [ExecuteInEditMode]
    public class CameraRatioPersistence : MonoBehaviour
    {
        [SerializeField]
        private RectTransform mapBoundaries;

        [SerializeField]
        private Camera cameraRef;

        private void Awake()
        {
            UpdateRatio();
        }

#if UNITY_EDITOR
        ///<summary>
        /// We don't want this to run every frame in a build due to perf concerns. 
        /// (the player wouldn't change resolutions while playing)
        ///</summary>
        private void Update()
        {
            if (Application.isPlaying) return;
            UpdateRatio();
        }
#endif

        /// <summary>
        /// Calculations from https://pressstart.vip/tutorials/2018/06/14/37/understanding-orthographic-size.html
        /// basically calculate what orthographic ratio is needed to cover the entire map boundaries.
        /// </summary>
        private void UpdateRatio()
        {
            var mapBoundariesBounds = mapBoundaries.sizeDelta;
            
            var screenRatio = Screen.width / (float)Screen.height;
            var targetRatio = mapBoundariesBounds.x / mapBoundariesBounds.y;

            if (screenRatio >= targetRatio)
            {
                cameraRef.orthographicSize = mapBoundariesBounds.y / 2;
            }
            else
            {
                var differenceInSize = targetRatio / screenRatio;
                cameraRef.orthographicSize = mapBoundariesBounds.y / 2 * differenceInSize;
            }
        }
    }
}