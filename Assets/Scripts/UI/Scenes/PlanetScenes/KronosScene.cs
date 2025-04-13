using System;
using System.Collections;
using Mission.Kronos;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class KronosScene : PlanetScene
    {
        protected override IEnumerator StartMissionRoutine()
        {
            yield return base.StartMissionRoutine();
            KronosMission kronosMission = CurrentMission as KronosMission;
            if (kronosMission != null) kronosMission.SetDraggableItems(true);
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
                Debug.Log("Start mission after tutorial");

                yield return new WaitForSeconds(.75f);
                StartMission();
                
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
            
            if(currentStepIndex == 3) SetMission();
            
            SetText(currentStep.Instruction);
            
            yield return new WaitForSeconds(.75f);
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }
    }
}