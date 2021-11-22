using System.Linq;
using System.Text;
using Persistence.Accessors;
using Persistence.PersistenceObjects;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class InventoryPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private HotKeyInfo _hotKeyInfo;
        [SerializeField] private InventoryChannel _inventoryChannel;

        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            var persistence = accessor.GetValue<PlayerInventoryPersistence>("PlayerInventory");
            
            if (persistence == null) return;
            
            foreach (var hotKeyCode in persistence.KeyCodeToItemIdMap.Keys)
            {
                var item = _playerInventory.OwnedItems.FirstOrDefault(x =>
                    x.ItemMeta.Id == persistence.KeyCodeToItemIdMap[hotKeyCode]);

                if (item == null) continue;
                
                _inventoryChannel.OnHotkeyAssigned(hotKeyCode, item);
            }
            
            // Inventory items amount doesnt reset on debug so dont add the items
            if (Debug.isDebugBuild) return;
            
            foreach (var ownedItem in persistence.OwnedItems)
            {
                var itemMeta = _playerInventory.GetItemMetaById(ownedItem.InventoryItemMetaId);
                _playerInventory.AddItem(itemMeta, ownedItem.Amount);
            }
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var persistence = GetPersistenceItem();

            accessor.PersistData("PlayerInventory", persistence);
        }

        private PlayerInventoryPersistence GetPersistenceItem()
        {
            var persistence = new PlayerInventoryPersistence();

            var itemsPersistence = _playerInventory.OwnedItems.Select(x =>
                new PlayerInventoryItemPersistence()
                {
                    Name = x.ItemMeta.Name,
                    Amount = x.Amount,
                    InventoryItemMetaId = x.ItemMeta.Id
                });
            persistence.OwnedItems = itemsPersistence.ToList();
            persistence.CurrencyItem = new PlayerInventoryItemPersistence()
            {
                Amount = _playerInventory.CurrencyItem.Amount,
                InventoryItemMetaId = _playerInventory.CurrencyItem.ItemMeta.Id
            };
            
            persistence.KeyCodeToItemIdMap =
                _hotKeyInfo.HotkeyCodeToItemIdMap.ToDictionary(x => x.Key, 
                    x => x.Value.Id);
            
            return persistence;
        }

        public override void PrintPersistantData()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("##### INVENTORY PERSISTENCE #####");
            var persistence = GetPersistenceItem();
            foreach (var persistentItem in persistence.OwnedItems)
            {
                strBuilder.AppendLine($"Item : {persistentItem.Name}, {persistentItem.Amount}");
            }
            strBuilder.AppendLine($"Currency : {persistence.CurrencyItem.Amount}");
            
            Debug.Log(strBuilder.ToString());
        }
    }
}