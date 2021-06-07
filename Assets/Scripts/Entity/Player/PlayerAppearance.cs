using System;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory;
using UnityEngine;

namespace Entity.Player
{
    public abstract class PlayerAppearance : MonoBehaviour
    {
        [SerializeField] protected PlayerEquipment _playerEquipment;
        [SerializeField] protected Player _player;

        protected Assets.HeroEditor.Common.CharacterScripts.Character _character;
        
        protected virtual void Awake()
        {
            _character = _player.GetCharacter();
            // _character.Equip(_playerEquipment.Armour.Item, EquipmentPart.Armor);
            // _character.Equip(_playerEquipment.Boots.Item, EquipmentPart.Boots);
            // _character.Equip(_playerEquipment.Helmet.Item, EquipmentPart.Helmet);
            // _character.Equip(_playerEquipment.Belt.Item, EquipmentPart.Belt);
            // _character.Equip(_playerEquipment.Cape.Item, EquipmentPart.Cape);
            // _character.Equip(_playerEquipment.Gloves.Item, EquipmentPart.Gloves);
        }
    }
}