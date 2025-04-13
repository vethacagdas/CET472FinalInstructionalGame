using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Mission.Report
{
    public class ReportAnswerButtonObject : MonoBehaviour
    {
        public bool IsCorrect;
        [SerializeField] private int correctAnswerIndex;
        [SerializeField] private Image correctnessImage;
        
        private Button[] _buttons;
        
        private void Awake()
        {
            _buttons = GetComponentsInChildren<Button>();
        }

        public void CheckAnswer(int buttonIndex)
        {
            IsCorrect = correctAnswerIndex == buttonIndex;

            int otherButtonIndex = buttonIndex == 0 ? 1 : 0;
            
            _buttons[buttonIndex].GetComponent<CanvasGroup>().DOFade(1f, .5f);
            _buttons[otherButtonIndex].GetComponent<CanvasGroup>().DOFade(.5f, .5f);
        }

        public void SetButtonInteractables(bool state)
        {
            foreach (Button button in _buttons)
            {
                button.interactable = state;
            }
        }
        
        public void SetCorrectnessImage(Sprite sprite, Color color)
        {
            correctnessImage.gameObject.SetActive(true);
            correctnessImage.sprite = sprite;
            correctnessImage.transform.DOScale(1, .5f).SetEase(Ease.OutBack);
            correctnessImage.color = color;
        }

    }
}