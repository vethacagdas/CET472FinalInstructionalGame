using BrunoMikoski.AnimationSequencer;
using Interfaces;
using UnityEngine;

namespace UI
{
    public class UIObject : MonoBehaviour, IActivatable
    {
        [Space(5)] [Header("ANIMATION PROPERTIES")]
        public AnimationSequencerController OpenAnimation;
        public AnimationSequencerController CloseAnimation;
        public bool IsOpened;
        public bool IsClosed;

        protected virtual void Start()
        {
            if (OpenAnimation != null)
            {
                OpenAnimation.OnStartEvent.AddListener(OnOpened);
            }

            if (CloseAnimation != null)
            {
                CloseAnimation.OnFinishedEvent.AddListener(OnClosed);
            }
        }

        public void Open()
        {
            if (OpenAnimation is null) return;
            
            OpenAnimation.Play();
        }
        
        public void Close()
        {
            if (CloseAnimation is null) return;

            CloseAnimation.Play();
        }

        public void OnOpened()
        {
            IsOpened = true;
            IsClosed = false;
        }

        public void OnClosed()
        {
            IsClosed = true;
            IsOpened = false;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
