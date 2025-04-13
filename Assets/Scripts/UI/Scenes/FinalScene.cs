using System.Collections;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scenes
{
    public class FinalScene : UIScene
    {
        [SerializeField] private UIElement certificate;
        
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

                certificate.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.PlayerName;
                certificate.Open();
                
                yield break;
            }

            currentStep = currentTutorial.Steps[currentStepIndex];
            
            ekoBotImage.gameObject.SetActive(true);
            ekoBotImage.sprite = currentStep.EkoBotSprite;
            
            SetTutorialComponents();
            
            instructionText.text = string.Empty;
            
            currentTutorialPanel.Open();

            SetText(currentStep.Instruction);
            
            yield return new WaitForSeconds(.75f);
            
            if (currentStepIndex != 0) undoButton.Open();
            continueButton.Open();
        }

        public void PlayAgainButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}