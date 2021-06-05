using System.Globalization;
using TMPro;
using UnityEngine;

namespace ElroyYa.Pang.Level
{
    public class LevelStateView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timeText, scoreText, levelText;

        [SerializeField]
        private CanvasGroup finishedLevelPanel, finishedGamePanel;

        public void UpdateValues(LevelStateModel model)
        {
            timeText.text = $"Time: {Mathf.FloorToInt(model.GameTime).ToString()}";
            scoreText.text = $"Score: {model.GameScore.ToString()}";
            levelText.text = $"Level: {model.GameLevel.ToString()}";
        }

        public void LevelDone(bool hasNextLevel)
        {
            finishedLevelPanel.alpha = hasNextLevel ? 1 : 0;
            finishedLevelPanel.interactable = finishedLevelPanel.alpha > 0;
            finishedLevelPanel.blocksRaycasts = finishedLevelPanel.alpha > 0;
            
            finishedGamePanel.alpha = hasNextLevel ? 0 : 1;
            finishedGamePanel.interactable = finishedGamePanel.alpha > 0;
            finishedGamePanel.blocksRaycasts = finishedGamePanel.alpha > 0;
        }
    }
}