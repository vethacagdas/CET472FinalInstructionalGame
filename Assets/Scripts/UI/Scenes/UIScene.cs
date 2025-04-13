using System;
using System.Collections;
using Manager;
using NaughtyAttributes;
using TMPro;
using Tutorial;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Scenes
{
    public abstract class UIScene : UIObject
    {
        [Space(7)] [Header("TUTORIAL PROPERTIES")]
        public TutorialData[] Tutorials;
        public UIElement[] TutorialPanels;
        [ReadOnly] public int CurrentTutorialIndex;
        [SerializeField] protected Image ekoBotImage;
        
        protected TextMeshProUGUI instructionText;
        protected UIElement continueButton;
        protected UIElement undoButton;

        protected TutorialData currentTutorial;
        protected UIElement currentTutorialPanel;
        protected TutorialStep currentStep;
        protected int currentStepIndex;
        
        public Action OnSkippedStep;
        public Action OnUndidStep;
        public UnityEvent OnTutorialStarted;
        public UnityEvent OnTutorialStopped;

        #region Start & Stop Tutorial
        public void StartTutorial(int index)
        {
            if (Tutorials.Length <= 0) return;

            Debug.Log($"{name}: {CurrentTutorialIndex}.tutorial started!");
            
            currentTutorial = Tutorials[index];
            
            OnTutorialStarted?.Invoke();
            
            currentStepIndex = 0;
            
            StartCoroutine(PlayTutorialStepRoutine());
        }

        public void StopTutorial() => StartCoroutine(StopTutorialRoutine());
        
        protected IEnumerator StopTutorialRoutine()
        {
            currentTutorialPanel?.Close();
            
            if (CurrentTutorialIndex >= Tutorials.Length)
            {
                CurrentTutorialIndex = 0;
            }
            else
            {
                CurrentTutorialIndex++;
            }
            
            yield return new WaitForSeconds(1f);
            
            ekoBotImage.gameObject.SetActive(false);
            
            OnTutorialStopped?.Invoke();
            
        }
        #endregion

        #region Skip Step
        public void SkipStep()
        {
            undoButton.Close();
            continueButton.Close();
            StartCoroutine(SkipStepRoutine());
        }

        protected abstract IEnumerator SkipStepRoutine();
        #endregion

        #region Undo Step
        public void UndoStep() => StartCoroutine(UndoStepRoutine());

        private IEnumerator UndoStepRoutine()
        {
            undoButton.Close();
            continueButton.Close();

            yield return new WaitForSeconds(1f);
            
            currentStepIndex--;
            OnUndidStep?.Invoke();
            StartCoroutine(PlayTutorialStepRoutine());
        }
        #endregion
        
        protected abstract IEnumerator PlayTutorialStepRoutine();
        
        protected void SetTutorialComponents()
        {
            currentTutorialPanel = currentStep.PanelState switch
            {
                PanelState.Middle => TutorialPanels[0],
                PanelState.Upper => TutorialPanels[1],
                _ => throw new ArgumentOutOfRangeException()
            };
            
            instructionText = currentTutorialPanel.GetComponentInChildren<TextMeshProUGUI>();
            continueButton = currentTutorialPanel.transform.Find("ContinueButton").GetComponent<UIElement>();
            undoButton = currentTutorialPanel.transform.Find("UndoButton").GetComponent<UIElement>();
        }
        
        protected void SetText(string text)
        {
            string[] words = text.Split(' ');
            
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains("<playerName>"))
                {
                    words[i] = words[i].Replace("<playerName>", GameManager.Instance.PlayerName);
                }
            }
            
            string newText = string.Join(" ", words);
            
            // Set the size of the text
            instructionText.enableAutoSizing = true;
            instructionText.text = newText;
            instructionText.ForceMeshUpdate();
            instructionText.enableAutoSizing = false;
        }
    }
}