using System;
using System.Collections.Generic;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] protected PlayerEquipment _playerEquipment;
        [SerializeField] protected Inventory _playerInventory;
        [SerializeField] protected Player _player;

        private Assets.HeroEditor.Common.CharacterScripts.Character _character;
        
        protected virtual void Start()
        {
            _character = _player.GetCharacter();

            if(_playerEquipment.Torso != null)
                _character.Equip(_playerEquipment.Torso.Item, EquipmentPart.Vest);
            
            if(_playerEquipment.Boots != null)
                _character.Equip(_playerEquipment.Boots.Item, EquipmentPart.Boots);
            
            if(_playerEquipment.Helmet != null)
                _character.Equip(_playerEquipment.Helmet.Item, EquipmentPart.Helmet);
            
            if(_playerEquipment.Pelvis != null)
                _character.Equip(_playerEquipment.Pelvis.Item, EquipmentPart.Belt);
            
            if(_playerEquipment.Cape != null)
                _character.Equip(_playerEquipment.Cape.Item, EquipmentPart.Cape);
            
            if(_playerEquipment.Gloves != null)
                _character.Equip(_playerEquipment.Gloves.Item, EquipmentPart.Gloves);

            if(_playerEquipment.PrimaryWeapon != null)
                _character.Equip(_playerEquipment.PrimaryWeapon.Item, _playerEquipment.PrimaryWeapon.Part);
        }

        public void EquipItem(EquipmentItemMeta meta)
        {
            RemoveItem(meta.Part);
            _character.Equip(meta.Item, meta.Part);
            _playerEquipment.SetMetaItem(meta);
        }

        public void RemoveItem(EquipmentPart part)
        {
            EquipmentItemMeta oldEquipment = _playerEquipment.GetItemMetaByPartType(part);

            if (oldEquipment != null)
            {
                _playerInventory.AddItemNoInfo(oldEquipment, 1);
                _character.UnEquip(oldEquipment.Part);
            }
        }
    }
}