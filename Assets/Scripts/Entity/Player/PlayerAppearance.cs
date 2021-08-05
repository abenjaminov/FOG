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
                EquipItem(_playerEquipment.Torso);
            
            if(_playerEquipment.Boots != null)
                EquipItem(_playerEquipment.Boots);
            
            if(_playerEquipment.Helmet != null)
                EquipItem(_playerEquipment.Helmet);
            
            if(_playerEquipment.Pelvis != null)
                EquipItem(_playerEquipment.Pelvis);
            
            if(_playerEquipment.Cape != null)
                EquipItem(_playerEquipment.Cape);
            
            if(_playerEquipment.Gloves != null)
                EquipItem(_playerEquipment.Gloves);

            if(_playerEquipment.PrimaryWeapon != null)
                this.EquipItem(_playerEquipment.PrimaryWeapon);
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