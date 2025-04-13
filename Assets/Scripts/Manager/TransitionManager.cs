using System;
using System.Collections;
using DG.Tweening;
using Interfaces;
using NaughtyAttributes;
using UI;
using UI.Scenes;
using UnityEngine;
using Utilities;

namespace Manager
{
    public class TransitionManager : MySingleton<TransitionManager>, IInitializable
    {
        [ReadOnly] public UIScene CurrentScene;
        
        [SerializeField] private UIWindow fadeScreenWindow;
        [SerializeField] private CanvasGroup fadeScreen;
        [SerializeField] private BeginningScene beginningScene;

        #region Events
        public Action<UIScene> OnSceneChanged;
        public Action<UIWindow> OnWindowAdded;
        public Action<UIWindow> OnWindowClosed;
        #endregion
        
        public void Initialize()
        {
            StartCoroutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            beginningScene.Activate();
            fadeScreenWindow.Activate();
            fadeScreenWindow.Close();
            CurrentScene = beginningScene;
            
            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(beginningScene.InitializeRoutine());
            
            fadeScreenWindow.Deactivate();
        }
        
        public void ChangeScene(UIScene scene)
        {
            StartCoroutine(ChangeSceneRoutine(scene));
        }

        private IEnumerator ChangeSceneRoutine(UIScene scene)
        {
            fadeScreenWindow.Activate();
            fadeScreenWindow.Open();
            CurrentScene.Close();
            
            yield return new WaitForSeconds(1.25f);
            
            CurrentScene.Deactivate();
            CurrentScene = scene;
            CurrentScene.Activate();

            fadeScreenWindow.Close();
            
            yield return new WaitForSeconds(1.25f);
            
            fadeScreenWindow.Deactivate();
            
            CurrentScene.Open();
            
            yield return new WaitWhile(() => !CurrentScene.IsOpened);

            CurrentScene.StartTutorial(CurrentScene.CurrentTutorialIndex);

            OnSceneChanged?.Invoke(CurrentScene);
        }

        public void AddWindow(UIWindow window)
        {
            StartCoroutine(AddWindowRoutine(window));
        }

        private IEnumerator AddWindowRoutine(UIWindow window)
        {
            fadeScreenWindow.Activate();
            fadeScreen.DOFade(.5f, .5f);
            window.Activate();
            window.Open();
            
            yield return new WaitForSeconds(1f);
            
            OnWindowAdded?.Invoke(window);
        }
        
        public void CloseWindow(UIWindow window)
        {
            StartCoroutine(CloseWindowRoutine(window));
        }

        private IEnumerator CloseWindowRoutine(UIWindow window)
        {
            fadeScreenWindow.Close();
            window.Close();
            
            yield return new WaitForSeconds(1f);
            
            fadeScreenWindow.Deactivate();
            window.Deactivate();
            OnWindowClosed?.Invoke(window);
        }
    }
}
