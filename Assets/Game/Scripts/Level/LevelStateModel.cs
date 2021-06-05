using ElroyYa.Pang.Utilities;
using UnityEngine;

namespace ElroyYa.Pang.Level
{
    public class LevelStateModel
    {
        public float GameTime { get; set; }
        public int GameScore { get; set; }
        public int GameLevel { get; }

        public LevelStateModel(int level)
        {
            GameLevel = level;
        }
    }
}