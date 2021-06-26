using Game;
using ScriptableObjects.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Inventory Channel", menuName = "Channels/Inventory Channel", order = 3)]
    public class InventoryChannel : ScriptableObject
    {
        public UnityAction<InventoryItem, InventoryItem> ItemAddedEvent;

        public void OnItemAdded(InventoryItem itemAddition, InventoryItem item)
        {
            ItemAddedEvent?.Invoke(itemAddition, item);
        }
    }
}