using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElroyYa.Pang.Menu.Levels
{
    public interface ILevelController
    {
        LevelModel[] GetLevels();
        void LoadLevel(LevelModel level);
    }
    
    /// <summary>
    /// Controls the level logic
    /// </summary>
    public class LevelController : MonoBehaviour, ILevelController
    {
        /// <summary>
        /// Get all of the available levels from the game.
        /// </summary>
        /// <returns></returns>
        public LevelModel[] GetLevels()
        {
            var scenesNum = SceneManager.sceneCountInBuildSettings;
            
            // first scene is always main menu
            var levels = new LevelModel[scenesNum - 1];

            for (var i = 0; i < levels.Length; i++)
            {
                levels[i] = new LevelModel(i + 1);
            }

            return levels;
        }

        /// <summary>
        /// Load a provided level
        /// </summary>
        /// <param name="level">the level to load</param>
        public void LoadLevel(LevelModel level)
        {
            SceneManager.LoadScene(level.LevelIndex, LoadSceneMode.Single);
        }
    }
}