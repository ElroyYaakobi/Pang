namespace ElroyYa.Pang.Menu.Levels
{
    /// <summary>
    /// Holds information about a level in the game
    /// </summary>
    public class LevelModel
    {
        public int LevelIndex { get; }

        public LevelModel(int index)
        {
            LevelIndex = index;
        }
    }
}