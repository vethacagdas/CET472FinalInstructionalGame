using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Mission.Report
{
    public class ReportDropdownObject : MonoBehaviour
    {
        [SerializeField] private Image correctnessImage;
        [SerializeField] private int correctOption;
        private TMP_Dropdown _dropdown;
        
        public bool IsCorrect => _dropdown.value == correctOption;

        private void Awake()
        {
            _dropdown = GetComponentInChildren<TMP_Dropdown>();
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
