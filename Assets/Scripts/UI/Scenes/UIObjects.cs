using UnityEngine;
using Utilities;

namespace UI.Scenes
{
    public class UIObjects : MySingleton<UIObjects>
    {
        [Space(5)][Header("SCENES")]
        public BeginningScene BeginningScene;
        public FirstTutorialScene FirstTutorialScene;
        public SecondTutorialScene SecondTutorialScene;
        public UniverseScene UniverseScene;
        public FinalScene finalScene;

        [Space(5)] [Header("WINDOWS")] 
        public UIWindow FadeScreenWindow;
        public UIWindow GeneralWindow;
        public UIWindow NotebookWindow;
        public ReportsPanelWindow ReportsPanelWindow;
        public UIWindow InfoWindow;
    }
}