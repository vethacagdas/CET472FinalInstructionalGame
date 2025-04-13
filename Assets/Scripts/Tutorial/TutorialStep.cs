using UnityEngine;

namespace Tutorial
{
    [System.Serializable]
    public class TutorialStep
    {
        public Sprite EkoBotSprite;
        [TextArea(1, 3)]public string Instruction;
        public PanelState PanelState;
    }
    
    public enum PanelState
    {
        Upper,
        Middle
    }
}