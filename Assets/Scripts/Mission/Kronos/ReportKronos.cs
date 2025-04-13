using Mission.Report;
using UnityEngine;
using UnityEngine.UI;

namespace Mission
{
    public class ReportKronos : Report.Report
    {
        [Space(10)]
        [SerializeField] private ReportAnswerButtonObject[] answerButtons;

        protected override void Awake()
        {
            base.Awake();
            answerButtons = GetComponentsInChildren<ReportAnswerButtonObject>();
        }

        public override void CheckAnswers()
        {
            isCompleted = true;
            
            foreach (ReportAnswerButtonObject reportAnswer in answerButtons)
            {
                if (reportAnswer.IsCorrect)
                {
                    reportAnswer.SetCorrectnessImage(correctSprite, Color.green);
                }
                else
                {
                    reportAnswer.SetCorrectnessImage(wrongSprite, Color.red);
                    isCompleted = false;
                }
                
                reportAnswer.SetButtonInteractables(true);
            }
        }
        
        public override void OpenPanel()
        {
            base.OpenPanel();
            if (isCompleted)
            {
                feedbackPanel.GetComponent<Image>().sprite = correctFeedbackPanelSprite;
                feedbackText.text = "Canlılar arasındaki bu mantıksal olayı başarıyla değerlendirdin, tebrikler!";
            }
            else
            {
                feedbackPanel.GetComponent<Image>().sprite = wrongFeedbackPanelSprite;
                feedbackText.text = "Boşlukları doğru tamamlayamadın, tekrar dene!";
            }
        }
    }
}