using UnityEngine;

namespace UI
{
    public class ReportsPanel : UIElement
    {
        [SerializeField] private ReportsPanelButton[] reportsPanelButtons;

        private void Awake()
        {
            reportsPanelButtons = GetComponentsInChildren<ReportsPanelButton>();
        }

        public void ActivateButtons()
        {
            foreach (ReportsPanelButton reportsPanelButton in reportsPanelButtons)
            {
                reportsPanelButton.Button.interactable = true;
            }
        }
        
        public void DeactivateButtons()
        {
            foreach (ReportsPanelButton reportsPanelButton in reportsPanelButtons)
            {
                reportsPanelButton.Button.interactable = false;
            }
        }
    }
}