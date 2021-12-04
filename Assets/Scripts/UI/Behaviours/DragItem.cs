using System;
using ScriptableObjects.Channels;
using UI.Mouse;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Behaviours
{
    public class DragItem : MonoBehaviour, ISingleClickHandler
    {
        public Image DragImage;
        private IDragTarget CurrentTarget;
        public IDraggable Draggable;
        [SerializeField] private DragChannel _dragChannel;
        
        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void HandleClick()
        {
            if (CurrentTarget == null)
            {
                _dragChannel.OnEndDragVoid(Draggable);                
            }
            else
            {
                CurrentTarget.DragDropped(Draggable);
                _dragChannel.OnDragEnd(CurrentTarget, Draggable);    
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(typeof(IDragTarget), out var component)) return;

            var target = (IDragTarget)component;

            CurrentTarget = target;
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (CurrentTarget == null) return;
            
            if (!other.gameObject.TryGetComponent(typeof(IDragTarget), out var component)) return;

            var target = (IDragTarget)component;

            if (CurrentTarget == target)
            {
                CurrentTarget = null;
            }
                
        }
    }
}