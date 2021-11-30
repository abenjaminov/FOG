using System;
using System.Collections.Generic;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] protected PersistenceChannel _persistenceChannel;
        [SerializeField] protected PlayerChannel _playerChannel;
        [SerializeField] protected PlayerEquipment _playerEquipment;
        [SerializeField] protected Inventory _playerInventory;
        [SerializeField] protected Player _player;

        private Assets.HeroEditor.Common.CharacterScripts.Character _character;

        private void Awake()
        {
            _persistenceChannel.GameModulesLoadedEvent += GameModulesLoadedEvent;
        }

        private void OnDestroy()
        {
            _persistenceChannel.GameModulesLoadedEvent -= GameModulesLoadedEvent;
        }

        private void GameModulesLoadedEvent()
        {
            _character = _player.GetCharacter();

            if(_playerEquipment.Helmet != null)
                _character.Equip(_playerEquipment.Helmet.Item, EquipmentPart.Helmet);

            if(_playerEquipment.Cape != null)
                _character.Equip(_playerEquipment.Cape.Item, EquipmentPart.Cape);
            
            if(_playerEquipment.Armour != null)
                _character.Equip(_playerEquipment.Armour.Item, EquipmentPart.Armor);

            if(_playerEquipment.PrimaryWeapon != null)
            {
                _character.Equip(_playerEquipment.PrimaryWeapon.Item, _playerEquipment.PrimaryWeapon.Part);
                _playerChannel.OnWeaponChanged(_playerEquipment.PrimaryWeapon);
            }
        }

        public void EquipItem(EquipmentItemMeta meta)
        {
            UnEquipItem(meta.Part);
            _character.Equip(meta.Item, meta.Part); 
            _playerEquipment.SetMetaItem(meta, meta.Part);
            _playerChannel.OnItemEquipped(meta);
        }

        public void UnEquipItem(EquipmentPart part)
        {
            var oldEquipment = _playerEquipment.GetItemMetaByPartType(part);

            if (oldEquipment != null)
            {
                _playerInventory.AddItemSilent(oldEquipment, 1);
                _character.UnEquip(oldEquipment.Part);
                _playerEquipment.SetMetaItem(null, part);
                _playerChannel.OnItemUnEquipped(null, part);
            }
        }
    }
}