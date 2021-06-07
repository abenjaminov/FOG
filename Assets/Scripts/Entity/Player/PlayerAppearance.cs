using System;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace Entity.Player
{
    public abstract class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] protected PlayerEquipment _playerEquipment;
        [SerializeField] protected Player _player;

        private Assets.HeroEditor.Common.CharacterScripts.Character _character;
        
        protected virtual void Awake()
        {
            _character = _player.GetCharacter();
            if(_playerEquipment.Torso != null)
                _character.Equip(_playerEquipment.Boots.Item, EquipmentPart.Vest);
            
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
        }

        public void EquipItem(EquipmentItemMeta meta)
        {
            _character.Equip(meta.Item, meta.Part);
        }
    }
}