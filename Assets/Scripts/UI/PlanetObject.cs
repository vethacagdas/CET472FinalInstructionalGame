using Manager;
using UI.Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlanetObject : UIElement
    {
        public bool IsLocked;

        [SerializeField] private PlanetScene previousPlanetScene;
        public PlanetScene PlanetScene;
        public PlanetObject NextPlanetObject;
        public PlanetScene NextPlanetScene;
        [SerializeField] private Image lockedImage;
        [SerializeField] private Button planetButton;

        private void OnEnable() 
        {
            CheckIfShouldUnlock();
            if (IsLocked) Lock();
            else Unlock();
        }

        private void Lock()
        {
            lockedImage.gameObject.SetActive(true);
            planetButton.interactable = false;
        }

        private void Unlock()
        {
            lockedImage.gameObject.SetActive(false);
            planetButton.interactable = true;
        }

        private void CheckIfShouldUnlock()
        {
            if (previousPlanetScene is null) return;
            IsLocked = !previousPlanetScene.IsCompleted;
        }
        
        public void EnterPlanet()
        {
            if (PlanetScene == null) return;
            TransitionManager.Instance.ChangeScene(PlanetScene);
        }
    }
}
