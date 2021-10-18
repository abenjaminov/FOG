using UI.Behaviours;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Drag Channel", menuName = "Channels/Drag Channel")]
    public class DragChannel : ScriptableObject
    {
        public UnityAction<IDraggable> StartDragEvent;
        public UnityAction<IDragTarget, IDraggable> EndDragEvent;
        public UnityAction<IDraggable> EndDragVoidEvent;

        public void OnDragStart(IDraggable draggable)
        {
            StartDragEvent?.Invoke(draggable);
        }

        public void OnDragEnd(IDragTarget dragTarget, IDraggable draggable)
        {
            EndDragEvent?.Invoke(dragTarget, draggable);
        }

        public void OnEndDragVoid(IDraggable draggable)
        {
            EndDragVoidEvent?.Invoke(draggable);
        }
    }
}