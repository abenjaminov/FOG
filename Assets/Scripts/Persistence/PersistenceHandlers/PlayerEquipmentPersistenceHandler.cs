using System;
using System.Text;
using Persistence.Accessors;
using Persistence.PersistenceObjects;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class PlayerEquipmentPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private InventoryItemsList _itemsList;
        
        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            var equipmentPersistence = accessor.GetValue<PlayerEquipmentPersistence>("PlayerEquipment");

            if (equipmentPersistence == null) return;
            
            _playerEquipment.Armour = _itemsList.GetItemMetaById(equipmentPersistence.ArmourItemMetaId) as EquipmentItemMeta;
            _playerEquipment.Helmet = _itemsList.GetItemMetaById(equipmentPersistence.HelmetItemMetaId) as EquipmentItemMeta;
            _playerEquipment.Cape = _itemsList.GetItemMetaById(equipmentPersistence.CapeItemMetaId) as EquipmentItemMeta;
            _playerEquipment.PrimaryWeapon = _itemsList.GetItemMetaById(equipmentPersistence.PrimaryWeaponItemMetaId) as WeaponItemMeta;
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var equipmentPersistence = GetEquipmentPersistence();
            
            accessor.PersistData("PlayerEquipment", equipmentPersistence);
        }

        private PlayerEquipmentPersistence GetEquipmentPersistence()
        {
            var equipmentPersistence = new PlayerEquipmentPersistence()
            {
                ArmourItemMetaId = _playerEquipment.Armour != null ? _playerEquipment.Armour.Id : "",
                CapeItemMetaId = _playerEquipment.Cape != null ? _playerEquipment.Cape.Id : "",
                HelmetItemMetaId = _playerEquipment.Helmet != null ? _playerEquipment.Helmet.Id : "",
                PrimaryWeaponItemMetaId = _playerEquipment.PrimaryWeapon != null ? _playerEquipment.PrimaryWeapon.Id : ""
            };

            return equipmentPersistence;
        }

        public override void PrintPersistantData()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("##### EQUIPMENT PERSISTENCE #####");
            strBuilder.AppendLine($"Helmet: {(_playerEquipment.Helmet != null ? _playerEquipment.Helmet.Name : "None")}");
            strBuilder.AppendLine($"Cape: {(_playerEquipment.Cape != null ? _playerEquipment.Cape.Name : "None")}");
            strBuilder.AppendLine($"Armour: {(_playerEquipment.Armour != null ? _playerEquipment.Armour.Name : "None")}");
            strBuilder.AppendLine($"PrimaryWeapon: {(_playerEquipment.PrimaryWeapon != null ? _playerEquipment.PrimaryWeapon.Name : "None")}");
            
#if UNITY_EDITOR
            Debug.Log(strBuilder.ToString());
#endif
            
            base.PrintPersistenceAsTextInternal(strBuilder.ToString(), "Player Equipment");
        }
    }
}