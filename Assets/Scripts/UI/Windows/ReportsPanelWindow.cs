using Mission.Report;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReportsPanelWindow : UIWindow
    {
        [ReadOnly] public Report CurrentReport;
        public Transform ReportsParent;
        
        public void SetCurrentReport(Report report) => CurrentReport = report;
        public void OpenCurrentReport() => CurrentReport?.Open();
        public void CloseCurrentReport() => CurrentReport?.Close();
        
        public void ActivatePlanet(Button planet) => planet.interactable = true;
    }
}