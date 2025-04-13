using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DragDrop
{
    public class DraggableItem : DragDropItem, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsContained;
        public ContainerItem LastDroppedContainer;

        private Vector3 _firstPosition;
        private Vector3 _oldPosition;

        public UnityEvent OnContained;
        public UnityEvent OnContainFailed;
        
        private void Start()
        {
            _firstPosition = transform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            LastDroppedContainer = null;
            _oldPosition = transform.position;
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            if (LastDroppedContainer is null) ReturnOldPosition();
        }

        public void ReturnOldPosition() => transform.position = _oldPosition;
        public void ReturnFirstPosition() => transform.position = _firstPosition;
        
        public void SetDroppedContainer(ContainerItem container) => LastDroppedContainer = container;

        public void SetIsContained(bool state)
        {
            IsContained = state;
            
            if (state) OnContained?.Invoke();
            else OnContainFailed?.Invoke();
        }

        public void ChangeColorToRed()
        {
            image.DOColor(Color.red, .75f).OnComplete(() => image.DOColor(Color.white, .75f));
        }
        
        public void ChangeColorToGreen()
        {
            image.DOColor(Color.green, .75f).OnComplete(() => image.DOColor(Color.white, .75f));
        }
    }
}