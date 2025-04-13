using System;
using DragDrop;
using UI.Scenes;
using UnityEngine;

namespace Mission.Poseidon
{
    public class Machine : MonoBehaviour
    {
        public bool CanChangeOrganism;
        
        [SerializeField] private PoseidonObject[] organisms;
        
        private PoseidonScene _poseidonScene; //TODO: mission
        private PoseidonObject _currentOrganism;
        private int _currentOrganismIndex;

        private void Awake()
        {
            _poseidonScene = GetComponentInParent<PoseidonScene>();
        }

        private void Start()
        {
            _currentOrganism = organisms[0];
        }

        public void ChangeOrganism()
        {
            _currentOrganismIndex++;

            if (_currentOrganismIndex >= organisms.Length)
            {
                _poseidonScene.CurrentMission.Report.Open();
                CanChangeOrganism = false;
                return;
            }

            CanChangeOrganism = true;
            
            _currentOrganism = organisms[_currentOrganismIndex];
        }

        public void EnterDraggableOrganism()
        {
            if (_currentOrganism.TryGetComponent(out DraggableItem draggableItem))
            {
                if (!draggableItem.IsContained) return;
            }
            
            ChangeOrganism();
            
            if (CanChangeOrganism) _currentOrganism.Enter();
        }
        
        public void EnterOrganism() => _currentOrganism.Enter();
        
        public void MoveOrganism() => _currentOrganism.Move();
    }
}