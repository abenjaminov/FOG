using Game;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;
using UnityEngine.Events;
using UnityEngineInternal;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Inventory Channel", menuName = "Channels/Inventory Channel", order = 3)]
    public class InventoryChannel : ScriptableObject
    {
        public Inventory.Inventory MainInventory;
        
        public UnityAction<InventoryItem, int> ItemAmountChangedEvent;
        public UnityAction<InventoryItem, int> ItemAmountChangedSilentEvent;
        public UnityAction<InventoryItem, Entity.Player.Player> UseItemRequestEvent;
        public UnityAction<InventoryItem> FailedToUseItemEvent;
        public UnityAction<InventoryItem> DropItemRequestEvent;
        
        public UnityAction<KeyCode, InventoryItem> HotkeyAssignedEvent;
        public UnityAction<KeyCode> HotkeyUnAssignedEvent;
        public UnityAction InsufficientFundsEvent;
        public UnityAction UsedCoinsEvent;

        public UnityAction UsedPotionEvent;

        public void OnUsePotion()
        {
            UsedPotionEvent?.Invoke();   
        }

        public void OnHotkeyUnAssigned(KeyCode code)
        {
            HotkeyUnAssignedEvent?.Invoke(code);
        }
        
        public void OnHotkeyAssigned(KeyCode code, InventoryItem item)
        {
            HotkeyAssignedEvent?.Invoke(code, item);
        }

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

        public void OnDropItemRequest(InventoryItem item)
        {
            DropItemRequestEvent?.Invoke(item);
        }

        public bool UseCoinsRequest(int coinsAmount)
        {
            if (MainInventory.CurrencyItem.Amount < coinsAmount)
            {
                InsufficientFundsEvent?.Invoke();
                return false;
            };
            
            MainInventory.AddItem(MainInventory.CurrencyItem.ItemMeta, -coinsAmount);
            UsedCoinsEvent?.Invoke();
            return true;
        }
    }
}