using System;
using System.Collections;
using Manager;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI.Scenes
{
    public class PlanetScene : UIScene
    {
        [Space(7)] [Header("PLANET PROPERTIES")]
        public bool IsCompleted;
        [SerializeField] private Sprite infoPanelSprite;
        [SerializeField] private Mission.Mission[] missions;
        
        [ReadOnly] public Mission.Mission CurrentMission;
        protected int currentMissionIndex;

        public void SetIsCompleted(bool state) => IsCompleted = state;

        private void OnEnable()
        {
            GameManager.Instance.SetInfoPanelSprite(infoPanelSprite);
        }

        public void StartMission()
        {
            StartCoroutine(StartMissionRoutine());
        }

        protected virtual IEnumerator StartMissionRoutine()
        {
            yield return new WaitForSeconds(1.75f);
        }
        
        public void SetMission()
        {
            StartCoroutine(SetMissionRoutine());
        }

        private IEnumerator SetMissionRoutine()
        {
            yield return new WaitForSeconds(1.5f);
            if (CurrentMission != null) CurrentMission.Deactivate();
            if (currentMissionIndex < missions.Length) CurrentMission = missions[currentMissionIndex];
            
            CurrentMission.Activate();
            
            GameManager.Instance.SetCurrentMission(CurrentMission);
            
            currentMissionIndex++;
        }

        protected override IEnumerator SkipStepRoutine()
        {
            yield break;
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            yield break;
        }
    }
}
