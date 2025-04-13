using System;
using System.Collections;
using Mission.Report;
using UI.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ReportsPanelButton : MonoBehaviour
    {
        public Button Button;
        [SerializeField] private Report report;

        private ReportsPanelWindow _reportsPanelWindow;

        private void Awake()
        {
            Button = GetComponentInChildren<Button>();
            _reportsPanelWindow = GetComponentInParent<ReportsPanelWindow>();
        }
        
        public void OpenReport()
        {
            StartCoroutine(OpenReportRoutine());
        }

         private IEnumerator OpenReportRoutine()
        {
            _reportsPanelWindow.CloseCurrentReport();
            
            yield return new WaitForSeconds(1f);
            
            _reportsPanelWindow.SetCurrentReport(report);
            _reportsPanelWindow.OpenCurrentReport();
        }
    }
}