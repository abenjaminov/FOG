using System;
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
            // Inventory items amount doesnt reset on debug so dont add the items
            //if(Application.isEditor) return;

            var persistence = accessor.GetValue<PlayerInventoryPersistence>("PlayerInventory");
            
            if (persistence == null) return;
            
            foreach (var ownedItem in persistence.OwnedItems)
            {
                var itemMeta = _playerInventory.GetItemMetaById(ownedItem.InventoryItemMetaId);
                _playerInventory.AddItem(itemMeta, ownedItem.Amount);
            }
            
            foreach (var hotKey in persistence.HotKeys)
            {
                var item = _playerInventory.OwnedItems.FirstOrDefault(x =>
                    x.ItemMeta.Id == hotKey.ItemId);

                if (item == null) continue;
                
                _inventoryChannel.OnHotkeyAssigned(hotKey.Key, item);
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

            persistence.HotKeys =
                _hotKeyInfo.HotkeyCodeToItemIdMap.Select(x => new HotkeyPersistence()
                {
                    Key = x.Key,
                    ItemId = x.Value.Id,
                    Name = x.Value.Name
                }).ToList();
            
            return persistence;
        }

        public override void PrintPersistantData()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("##### INVENTORY PERSISTENCE #####");
            var persistence = GetPersistenceItem();
            strBuilder.AppendLine("1. Items:");
            foreach (var persistentItem in persistence.OwnedItems)
            {
                strBuilder.AppendLine($"\tItem : {persistentItem.Name}, {persistentItem.Amount}");
            }
            strBuilder.AppendLine($"\tCurrency : {persistence.CurrencyItem.Amount}");
            
            strBuilder.AppendLine("2. Hotkeys:");
            
            foreach (var hotKey in persistence.HotKeys)
            {
                strBuilder.AppendLine($"{Enum.GetName(typeof(KeyCode), hotKey.Key)} -> {hotKey.ItemId}");
            }
            
            #if UNITY_EDITOR
            Debug.Log(strBuilder.ToString());
            #endif
            
            base.PrintPersistenceAsTextInternal(strBuilder.ToString(), "Inventory");
        }
    }
}