using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

namespace DragDrop
{
    public class ContainerItem : DragDropItem, IDropHandler
    {
        public UnityEvent OnCorrectDrop;
        public UnityEvent OnWrongDrop;
        
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;

            if (!dropped.TryGetComponent(out DraggableItem draggableItem)) return;
            
            draggableItem.SetDroppedContainer(this);
            
            if (draggableItem.ObjectId == ObjectId)
            {
                Debug.Log("Matched!");
                OnCorrect(draggableItem);
            }
            else
            {
                Debug.Log("Wrong match!");
                OnWrong(draggableItem);
            }
        }

        private void OnCorrect(DraggableItem draggableItem)
        {
            draggableItem.transform.SetParent(transform); // TODO: Add this to UnityEvent of DraggableItem
            draggableItem.transform.position = transform.position;
          
            // draggableItem.enabled = false; // TODO: Add this to UnityEvent of DraggableItem
            draggableItem.SetIsContained(true);
            
            enabled = false;
            
            OnCorrectDrop?.Invoke();
        }

        private void OnWrong(DraggableItem draggableItem)
        {
            draggableItem.ReturnOldPosition();
            draggableItem.SetIsContained(false);
            OnWrongDrop?.Invoke();
        }

        
    }
}