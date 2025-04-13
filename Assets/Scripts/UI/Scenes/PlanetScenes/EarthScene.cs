using System;
using System.Collections;
using Mission.Earth;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes
{
    public class EarthScene : PlanetScene
    {
        protected override IEnumerator StartMissionRoutine()
        {
            yield return base.StartMissionRoutine();
            EarthMission mission = CurrentMission as EarthMission;
            
            float value = 0;
            
            if (currentTutorial == Tutorials[0])
            {
                mission.SetMissionPhase(MissionPhase.Tutorial);
                value = 5;
            }
            else if (currentTutorial == Tutorials[1])
            {
                mission.SetMissionPhase(MissionPhase.Grass);
                value = 0;
            }
            else if (currentTutorial == Tutorials[2])
            {
                mission.SetMissionPhase(MissionPhase.Rabbit);
                value = 0;
            }
            else if (currentTutorial == Tutorials[3])
            {
                mission.SetMissionPhase(MissionPhase.Owl);
                value = 0;
            }

            mission.Initialize(value);
        }

        protected override IEnumerator SkipStepRoutine()
        {
            yield return new WaitForSeconds(1f);
            
            if (currentTutorial == Tutorials[0] && currentStepIndex == 0) SetMission();
            
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

                yield return new WaitForSeconds(1f);
                
                StartMission();
                
                yield break;
            }
            
            currentStep = currentTutorial.Steps[currentStepIndex];
            TutorialStep previousStep = currentStep;
            
            if (currentStepIndex > 0) previousStep = currentTutorial.Steps[currentStepIndex - 1];

            if (currentStep.PanelState != previousStep.PanelState)
            {
                currentTutorialPanel.Close();
                yield return new WaitForSeconds(.75f);
            }
            
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
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }
    }
}