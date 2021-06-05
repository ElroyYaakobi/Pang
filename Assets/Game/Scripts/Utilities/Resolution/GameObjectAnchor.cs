using System;
using UnityEngine;

namespace ElroyYa.Pang.Utilities
{
    /// <summary>
    /// Make sure that the game object is at the same relative position to the screen boundaries at all time.
    /// </summary>
    [ExecuteInEditMode]
    public class GameObjectAnchor : MonoBehaviour
    {
        /// <summary>
        /// TODO: Support more anchors, for now that's enough
        /// </summary>
        private enum Anchors
        {
            CenterTop,
            CenterBottom,
            CenterLeft,
            CenterRight
        }

        [SerializeField]
        private Anchors anchor;

        [SerializeField]
        private Vector3 anchorOffset;

        private Camera Camera { get; set; }

        /// <summary>
        /// Update the anchor on start, as this executes after the 'Awake' of CameraRatioPersistence.
        /// Its important to keep this order or the system would break.
        /// </summary>
        private void Start()
        {
            Camera = Camera.main;
            Debug.Assert(Camera, $"Can't process {nameof(GameObjectAnchor)} as the main camera couldn't be found");
            
            UpdateAnchor();
        }

#if UNITY_EDITOR
        /// <summary>
        /// We want this to update only on the editor screen (And off-play) so the we can edit the position of these objects
        /// while playing the game
        /// </summary>
        private void Update()
        {
            if (Application.isPlaying) return;
            UpdateAnchor();
        }
#endif

        /// <summary>
        /// Set the position of the object depending on the screen boundaries.
        /// </summary>
        private void UpdateAnchor()
        {
            transform.position = GetAnchorPosition(anchor, Camera) + anchorOffset;
        }

        private static Vector3 GetAnchorPosition(Anchors anchor, Camera camera)
        {
            var screenPos = Vector3.zero;
            switch (anchor)
            {
                case Anchors.CenterTop:
                    screenPos = new Vector3(Screen.width / 2f, Screen.height);
                    break;
                case Anchors.CenterBottom:
                    screenPos = new Vector3(Screen.width / 2f, 0);
                    break;
                case Anchors.CenterLeft:
                    screenPos = new Vector3(0, Screen.height / 2f);
                    break;
                case Anchors.CenterRight:
                    screenPos = new Vector3(Screen.width, Screen.height / 2f);
                    break;
            }

            var worldPos = camera.ScreenToWorldPoint(screenPos);
            worldPos.z = 0;
            return worldPos;
        }
    }
}