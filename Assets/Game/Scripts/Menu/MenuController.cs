using UnityEngine;
using UnityEngine.UI;

namespace ElroyYa.Pang.Menu
{
    /// <summary>
    /// Sets the menu resolution for the time being
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        private void Awake()
        {
            Screen.orientation = ScreenOrientation.Landscape;
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}