using System;
using System.Collections;
using Manager;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class AresScene : PlanetScene
    {
        protected override IEnumerator StartMissionRoutine()
        {
            yield return base.StartMissionRoutine();
            CurrentMission.Open();
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
                
                if (currentTutorial == Tutorials[0] || currentTutorial == Tutorials[2])
                {
                    Debug.Log("Start mission after tutorial");
                    StartMission();
                }
                else if (currentTutorial == Tutorials[1])
                {
                    yield return new WaitForSeconds(.75f);
                    CurrentMission.Report.Open();
                }
                else if (currentTutorial == Tutorials[3])
                {
                    TransitionManager.Instance.ChangeScene(UIObjects.Instance.UniverseScene);
                }
                yield break;
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            TutorialStep previousStep = currentStep;
            
            if (currentStepIndex > 0) previousStep = currentTutorial.Steps[currentStepIndex - 1];
            
            if (currentStep.PanelState != previousStep.PanelState) currentTutorialPanel.Close();
            
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
            
            if (currentTutorial == Tutorials[0] && currentStepIndex == 1) SetMission();
            
            SetText(currentStep.Instruction);
            
            yield return new WaitForSeconds(.75f);
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }
    }
}