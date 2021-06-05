using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ElroyYa.Pang.Menu.Levels
{
    /// <summary>
    /// Manages the visuals of a single level
    /// </summary>
    public class LevelView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private TextMeshProUGUI levelIndexText;

        [SerializeField]
        private float onHoverGrowthMultiplier = 1.1f;
        
        private Vector3 SourceScale { get; set; }
        private LevelModel Model { get; set; }
        private ILevelController Controller { get; set; }

        public void Setup(LevelModel model, ILevelController controller)
        {
            Model = model;
            Controller = controller;
            
            levelIndexText.text = model.LevelIndex.ToString();
            SourceScale = transform.localScale;
        }

        #region UI Callbacks
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Controller.LoadLevel(Model);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = SourceScale * onHoverGrowthMultiplier;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = SourceScale;
        }
        
        #endregion
    }
}