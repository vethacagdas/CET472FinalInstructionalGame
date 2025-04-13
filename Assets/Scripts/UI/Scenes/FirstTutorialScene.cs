using System;
using System.Collections;
using Manager;
using TMPro;
using Tutorial;
using UnityEngine;

namespace UI.Scenes
{
    public class FirstTutorialScene : UIScene
    {
        [SerializeField] private UIElement playerNameInputField;
        
        protected override IEnumerator SkipStepRoutine()
        {
            yield return new WaitForSeconds(1f);
            
            if (currentStepIndex == 1)
            {
                TMP_InputField inputField = playerNameInputField.GetComponent<TMP_InputField>();
                GameManager.Instance.SetPlayerName(inputField.text);
                playerNameInputField.Deactivate();
            }
            
            currentStepIndex++;
            OnSkippedStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }
        
        protected override IEnumerator PlayTutorialStepRoutine()
        {
            if (currentStepIndex >= currentTutorial.Steps.Length)
            {
                yield return StartCoroutine(StopTutorialRoutine());
                TransitionManager.Instance.ChangeScene(UIObjects.Instance.SecondTutorialScene);
                yield break;
            }

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
            
            if (currentStepIndex == 1)
            {
                yield return StartCoroutine(GetPlayerNameRoutine());
            }
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }

        private IEnumerator GetPlayerNameRoutine()
        {
            playerNameInputField.Activate();
            playerNameInputField.Open();
            
            yield return new WaitForSeconds(1f);
            
            TMP_InputField inputField = playerNameInputField.GetComponent<TMP_InputField>();
            
            while (inputField.text == string.Empty)
            {
                yield return null;
            }
        }
    }
}