using System;
using System.Collections;
using System.Threading.Tasks;
using ElroyYa.Pang.Entities.Ball;
using ElroyYa.Pang.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElroyYa.Pang.Level
{
    /// <summary>
    /// Manages the state of the current level
    /// </summary>
    [RequireComponent(typeof(LevelStateView))]
    public class LevelStateController : Singleton<LevelStateController>
    {
        [SerializeField]
        private int resetLevelDelayMs = 3000;
        
        private LevelStateView View { get; set; } 

        public LevelStateModel Model { get; private set; }

        private void Awake()
        {
            View = GetComponent<LevelStateView>();
            Model = new LevelStateModel(SceneManager.GetActiveScene().buildIndex);
            View.UpdateValues(Model);

            BallController.OnBallDestroyed += OnBallDestroyed;
        }

        private void OnDestroy()
        {
            BallController.OnBallDestroyed -= OnBallDestroyed;
        }

        private void Update()
        {
            Model.GameTime += Time.deltaTime;
            View.UpdateValues(Model);
        }

        private void OnBallDestroyed(BallModel ballModel)
        {
            const int baseScore = 10;
            Model.GameScore += (ballModel.Life + 1) * baseScore;

            CheckEndLevel();
        }

        /// <summary>
        /// If no balls exist in the scene anymore, load the next level (if exists) if not, show the end game screen.
        /// </summary>
        private async void CheckEndLevel()
        {
            if (BallController.ActiveBalls.Count > 0) return;

            var nextScene = SceneManager.GetActiveScene().buildIndex;
            var hasNextLevel = nextScene < SceneManager.sceneCountInBuildSettings;
            
            View.LevelDone(hasNextLevel);

            if (!hasNextLevel) return;
            
            await Task.Delay(resetLevelDelayMs);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        
        public async void ResetLevel()
        {
            await Task.Delay(resetLevelDelayMs);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}