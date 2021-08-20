using Game;
using ScriptableObjects.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Inventory Channel", menuName = "Channels/Inventory Channel", order = 3)]
    public class InventoryChannel : ScriptableObject
    {
        public UnityAction<InventoryItem, int> ItemAddedEvent;

        public void OnItemAdded(InventoryItem item, int amountAdded)
        {
            ItemAddedEvent?.Invoke(item, amountAdded);
        }
    }
}