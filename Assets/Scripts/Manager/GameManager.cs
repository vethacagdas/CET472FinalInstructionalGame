using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Manager
{
    public class GameManager : MySingleton<GameManager>
    {
        [ReadOnly] public string PlayerName = "Vetha";
        [ReadOnly] public int DiamondAmount;
        [ReadOnly] public Mission.Mission CurrentMission;
        [SerializeField] private TextMeshProUGUI diamondText;
        [SerializeField] private Image infoPanelImage;
        
        private void Start()
        {
            TransitionManager.Instance.Initialize();
        }
        
        public void IncreaseDiamondAmount(int amount)
        {
            DiamondAmount += amount;
            UpdateDiamondText();
        }

        public void SetInfoPanelSprite(Sprite sprite) => infoPanelImage.sprite = sprite;
        
        private void UpdateDiamondText() => diamondText.text = DiamondAmount.ToString();
        
        public void SetPlayerName(string playerName) => PlayerName = playerName;

        public void SetCurrentMission(Mission.Mission mission) => CurrentMission = mission;

        public void OpenCurrentReport() => CurrentMission.Report.Open();
        public void CloseCurrentReport()
        {
            CurrentMission.Report.transform.DOScale(0, 1f).SetEase(Ease.InBack);
            CurrentMission.Report.OnClosed();
            CurrentMission.FadeScreen.Close();
        }
        
        public void Quit()
        {
            if (Application.isPlaying) Application.Quit();
        }
    }
}