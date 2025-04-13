using System;
using Manager;
using UI;
using UI.Scenes;
using UnityEngine;

namespace Mission
{
    public class Mission : UIElement
    {
        public Action OnMissionStarted;
        public Action OnMissionCompleted;
        
        public Report.Report Report;
        public UIElement FadeScreen;
        public PlanetScene Planet;

        private void Awake()
        {
            Report = transform.Find("Report").GetComponent<Report.Report>();
            FadeScreen = transform.Find("FadeScreen").GetComponent<UIElement>();
            Planet = GetComponentInParent<PlanetScene>();
            
            CloseAnimation.OnFinishedEvent.AddListener(CallOnMissionCompleted);
        }

        public void CallOnMissionStarted()
        {
            Debug.Log($"{gameObject.name} mission started!");
            OnMissionStarted?.Invoke();
        }

        public void CallOnMissionCompleted()
        {
            GameManager.Instance.IncreaseDiamondAmount(10);
            OnMissionCompleted?.Invoke();
        }
    }
}