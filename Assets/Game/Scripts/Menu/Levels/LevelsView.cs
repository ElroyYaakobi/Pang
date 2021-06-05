using UnityEngine;

namespace ElroyYa.Pang.Menu.Levels
{
    /// <summary>
    /// Shows all of the available levels
    /// </summary>
    [RequireComponent(typeof(ILevelController), typeof(CanvasGroup))]
    public class LevelsView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        private Transform levelsHolder;

        [SerializeField]
        private LevelView viewPrefab;
        
        private CanvasGroup CanvasGroup { get; set; }
        private ILevelController LevelController { get; set; }

        /// <summary>
        /// Setup levels and set visibility to false on default, we don't want it to be instantly visible.
        /// </summary>
        private void Awake()
        {
            LevelController = GetComponent<ILevelController>();
            CanvasGroup = GetComponent<CanvasGroup>();
            
            SetupLevels();
            Show(false);
        }
        
        private void SetupLevels()
        {
            var allLevels = LevelController.GetLevels();
            foreach (var levelModel in allLevels)
            {
                var levelView = Instantiate(viewPrefab, levelsHolder);
                levelView.Setup(levelModel, LevelController);
            }
        }

        public void Show(bool enable)
        {
            CanvasGroup.alpha = enable ? 1f : 0f;
            CanvasGroup.interactable = enable;
            CanvasGroup.blocksRaycasts = enable;
        }
    }
}