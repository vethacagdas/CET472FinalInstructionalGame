using System.Collections.Generic;
using DragDrop;
using TMPro;
using UnityEngine;

namespace Mission.Poseidon
{
    public class Poseidon_Bar : MonoBehaviour
    {
        [SerializeField] private PoseidonMission mission;
        [SerializeField] private ContainerItem[] containerItems;
        [SerializeField] private string[] texts;
        private TextMeshProUGUI _text;
        private int _containedAmount;
        private DraggableItem _draggableItem;
        
        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _draggableItem = GetComponent<DraggableItem>();
        }

        public void ResetContainedAmount() => _containedAmount = 0;
        public void IncreaseContainedAmount() => _containedAmount++;
        
        private void ChangeText(string text) => _text.text = text;

        public void CheckContainers()
        {
            for (int i = 0; i < containerItems.Length; i++)
            {
                if (_draggableItem.LastDroppedContainer != containerItems[i]) continue;
                
                ChangeText(texts[i]);
                break;
            }
        }

        public void CheckContainedAmount()
        {
            if (_containedAmount == containerItems.Length) mission.Report.Open();
        }
    }
}
