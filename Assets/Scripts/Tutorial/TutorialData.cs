using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "TutorialData_", menuName = "Data/Tutorial")]
    public class TutorialData : ScriptableObject
    {
        public TutorialStep[] Steps;
    }
}