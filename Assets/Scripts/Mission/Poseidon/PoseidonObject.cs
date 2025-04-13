using BrunoMikoski.AnimationSequencer;
using UI;
using UnityEngine;

namespace Mission.Poseidon
{
    public class PoseidonObject : UIElement
    {
        [SerializeField] private AnimationSequencerController enterAnim;
        [SerializeField] private AnimationSequencerController moveAnim;

        public void Enter() => enterAnim.Play();
        public void Move() => moveAnim.Play();
    }
}