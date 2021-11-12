using System.Linq;
using System.Text;
using Persistence.Accessors;
using Persistence.PersistenceObjects;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class InventoryPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private Inventory _playerInventory;

        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            if (Debug.isDebugBuild) return;
            
            var persistence = accessor.GetValue<PlayerInventoryPersistence>("PlayerInventory");

            if (persistence == null) return;
            
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