using Game;
using ScriptableObjects.Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Inventory Channel", menuName = "Channels/Inventory Channel", order = 3)]
    public class InventoryChannel : ScriptableObject
    {
        public UnityAction<InventoryItem, int> ItemAmountChangedEvent;
        public UnityAction<InventoryItem, int> ItemAmountChangedSilentEvent;
        public UnityAction<InventoryItem, Entity.Player.Player> UseItemRequestEvent;
        public UnityAction<InventoryItem> FailedToUseItemEvent;

        public void OnItemAmountChanged(InventoryItem item, int amountDelta)
        {
            ItemAmountChangedEvent?.Invoke(item, amountDelta);
        }
        
        public void OnItemAmountChangedSilent(InventoryItem item, int amountDelta)
        {
            ItemAmountChangedSilentEvent?.Invoke(item, amountDelta);
        }

        public void OnUseItemRequest(InventoryItem item, Entity.Player.Player player = null)
        {
            UseItemRequestEvent?.Invoke(item, player);
        }

        public void OnFailedToUseItem(InventoryItem item)
        {
            FailedToUseItemEvent?.Invoke(item);
        }
    }
}