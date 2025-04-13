using DragDrop;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Kronos
{
    public class KronosMission : Mission
    {
        [SerializeField] private DraggableItem[] DraggableItems;
        
        [SerializeField] private UIElement feedbackPanel;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private Sprite correctFeedback;
        [SerializeField] private Sprite wrongFeedback;
        
        private int _correctMatches;
        
        public int CorrectMatches => _correctMatches;

        protected override void Start()
        {
            base.Start();
            SetDraggableItems(false);
            OnMissionCompleted += Report.Open;
        }

        private void OnDestroy()
        {
            OnMissionCompleted -= Report.Open;
        }

        public void SetDraggableItems(bool state)
        {
            foreach (DraggableItem draggableItem in DraggableItems)
            {
                draggableItem.enabled = state;
            }
        }

        public void OpenPanel(bool state)
        {
            feedbackPanel.Open();
            if (state)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedback;
                feedbackText.text = "Harika!! Tam bir ağ yapma uzmanısın.";
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedback;
                feedbackText.text = "Bu canlının ağında bir problem var gibi duruyor. Tekrar dene!";
            }
        }
        
        public void ClosePanel()
        {
            feedbackPanel.Close();
        }
        
        public void IncreaseCorrectMatches() => _correctMatches++;
    }
}