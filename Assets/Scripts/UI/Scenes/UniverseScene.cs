using System;
using System.Collections;
using Manager;
using NaughtyAttributes;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class UniverseScene : UIScene
    {
        [ReadOnly] public PlanetObject CurrentPlanet;
        [SerializeField] private PlanetObject[] planets;
        
        private void OnEnable()
        {
            TransitionManager.Instance.AddWindow(UIObjects.Instance.GeneralWindow);
            SetCurrentPlanet();
        }

        private void SetCurrentPlanet()
        { 
            if (planets[2].PlanetScene.IsCompleted) CurrentPlanet = planets[3];
            else if (planets[1].PlanetScene.IsCompleted) CurrentPlanet = planets[2];
            else if (planets[0].PlanetScene.IsCompleted) CurrentPlanet = planets[1];
            else CurrentPlanet = planets[0];
        }

        private void SetCurrentTutorial()
        {
            if (CurrentPlanet == planets[3]) currentTutorial = Tutorials[3];
            else if (CurrentPlanet == planets[2]) currentTutorial = Tutorials[2];
            else if (CurrentPlanet == planets[1]) currentTutorial = Tutorials[1];
            else if (CurrentPlanet == planets[0]) currentTutorial = Tutorials[0];
        }

        public void ChangeSceneDevBuild()
        {
            if (!Debug.isDebugBuild) return;
            
            TransitionManager.Instance.ChangeScene(CurrentPlanet.NextPlanetScene);
            CurrentPlanet = CurrentPlanet.NextPlanetObject;
        }
        
        protected override IEnumerator SkipStepRoutine()
        {
            yield return new WaitForSeconds(1f);
            
            currentStepIndex++;
            OnSkippedStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                yield return StartCoroutine(StopTutorialRoutine());
                yield break;
            }

            SetCurrentTutorial();
            
            currentStep = currentTutorial.Steps[currentStepIndex];
            
            switch (currentStep.PanelState)
            {
                case PanelState.Upper:
                    ekoBotImage.gameObject.SetActive(false);
                    break;
                case PanelState.Middle:
                    ekoBotImage.gameObject.SetActive(true);
                    ekoBotImage.sprite = currentStep.EkoBotSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SetTutorialComponents();
            
            instructionText.text = string.Empty;
            
            currentTutorialPanel.Open();

            SetText(currentStep.Instruction);
            
            yield return new WaitForSeconds(.75f);
        }
    }
}