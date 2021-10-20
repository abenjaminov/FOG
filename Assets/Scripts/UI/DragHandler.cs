using System;
using System.Collections;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using UI.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DragHandler : MonoBehaviour
    {
        [SerializeField] private DragChannel _dragChannel;
        [SerializeField] private DragItem _dragItem;
        [SerializeField] private InventoryChannel _inventoryChannel;

        private Sprite currentDraggingSprite;
        
        private void Awake()
        {
            _dragChannel.StartDragEvent += StartDragEvent;
            _dragChannel.EndDragEvent += EndDragEvent;
            _dragChannel.EndDragVoidEvent += EndDragVoidEvent;
        }

        private void EndDragVoidEvent(IDraggable arg0)
        {
            EndDragInternal();
        }

        private void EndDragEvent(IDragTarget target, IDraggable draggable)
        {
            EndDragInternal();
        }

        private void EndDragInternal()
        {
            currentDraggingSprite = null;
            _dragItem.DragImage.sprite = currentDraggingSprite;
            _dragItem.Draggable = null;
            _dragItem.SetActive(false);
        }

        private void StartDragEvent(IDraggable draggable)
        {
            currentDraggingSprite = draggable.GetDragImage();
            if (currentDraggingSprite == null) return;

            _dragItem.DragImage.sprite = currentDraggingSprite;
            _dragItem.Draggable = draggable;
            _dragItem.SetActive(true);
        }

        private void OnDestroy()
        {
            _dragChannel.StartDragEvent -= StartDragEvent;
            _dragChannel.EndDragEvent -= EndDragEvent;
            _dragChannel.EndDragVoidEvent -= EndDragVoidEvent;
        }
    }
}