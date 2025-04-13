using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class ReportAnswerInputField : MonoBehaviour
    {
        [SerializeField] private string answer;
        [SerializeField] private Image correctnessImage;
        private TMP_InputField _inputField;
        
        private void Awake()
        {
            _inputField = GetComponentInChildren<TMP_InputField>();
        }

        public bool CheckAnswer()
        {
            return answer.Equals(_inputField.text, StringComparison.CurrentCultureIgnoreCase);
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