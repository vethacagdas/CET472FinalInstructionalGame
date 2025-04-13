using System;
using System.Collections;
using DG.Tweening;
using DragDrop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mission.Kronos
{
    public class KronosContainerItem : KronosObject, IDropHandler
    {
        private KronosMission _kronosMission;

        protected override void Awake()
        {
            base.Awake();
            _kronosMission = GetComponentInParent<KronosMission>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;

            if (!dropped.TryGetComponent(out DraggableItem draggableItem)) return;
            
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
            draggableItem.transform.SetParent(transform);
            draggableItem.transform.position = transform.position;
                    
            Image webImage = draggableItem.transform.Find("Web").GetComponent<Image>();
            
            webImage.DOColor(Color.green, .5f);
                    
            draggableItem.enabled = false;
            
            _kronosMission.IncreaseCorrectMatches();
            
            if (_kronosMission.CorrectMatches >= 8) _kronosMission.CallOnMissionCompleted();
            else _kronosMission.OpenPanel(true);
            
            enabled = false;
        }

        private void OnWrong(DraggableItem draggableItem)
        {
            StartCoroutine(OnWrongRoutine(draggableItem));
            _kronosMission.OpenPanel(false);
        }
        
        private IEnumerator OnWrongRoutine(DraggableItem draggableItem)
        {
            draggableItem.ReturnOldPosition();
            Image webImage = draggableItem.transform.Find("Web").GetComponent<Image>();
            webImage.DOColor(Color.red, .5f);

            yield return new WaitForSeconds(1f);
            
            webImage.DOColor(Color.white, .5f);
        }
    }
}