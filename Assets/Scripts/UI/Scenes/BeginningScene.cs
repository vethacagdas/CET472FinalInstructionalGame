using System.Collections;
using UnityEngine;

namespace UI.Scenes
{
    public class BeginningScene : UIScene
    {
        public IEnumerator InitializeRoutine()
        {
            yield return new WaitForSeconds(1);
            Open();       
        }

        protected override IEnumerator SkipStepRoutine()
        {
            throw new System.NotImplementedException();
        }

        protected override IEnumerator PlayTutorialStepRoutine()
        {
            throw new System.NotImplementedException();
        }
    }
}