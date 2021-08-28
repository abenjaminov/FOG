using System;
using System.Collections.Generic;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Player Equipment", menuName = "Inventory/Player Equipment", order = 2)]
    public class PlayerEquipment : ScriptableObject
    {
        [SerializeField] private PlayerChannel _playerChannel;
        
        [Header("Equipment")]
        public EquipmentItemMeta Helmet;
        public EquipmentItemMeta Cape;
        public EquipmentItemMeta Armour;
        public WeaponItemMeta PrimaryWeapon;
        public WeaponItemMeta SecondaryWeapon;

        private Dictionary<EquipmentPart, Func<EquipmentItemMeta>> MetaByType;
        
        private void OnEnable()
        {
            MetaByType = new Dictionary<EquipmentPart, Func<EquipmentItemMeta>>()
            {
                {EquipmentPart.Helmet, () => Helmet},
                {EquipmentPart.Cape, () => Cape},
                {EquipmentPart.Armor, () => Armour},
                {EquipmentPart.MeleeWeapon1H, () => PrimaryWeapon},
                {EquipmentPart.Bow, () => PrimaryWeapon},
            };
        }

        public EquipmentItemMeta GetItemMetaByPartType(EquipmentPart partType)
        {
            return MetaByType[partType]();
        }
        
        public void SetMetaItem(EquipmentItemMeta meta, EquipmentPart? part = null)
        {
            switch (part ?? meta.Part)
            {
                case EquipmentPart.Helmet:
                    Helmet = meta;
                    break;
                case EquipmentPart.Cape:
                    Cape = meta;
                    break;
                case EquipmentPart.Armor:
                    Armour = meta;
                    break;
                case EquipmentPart.MeleeWeapon1H:
                    ChangePrimaryWeapon(meta as WeaponItemMeta, part);
                    break;
                case EquipmentPart.Bow:
                    ChangePrimaryWeapon(meta as WeaponItemMeta, part);
                    break;
                default:
                    break;
            }
        }

        private void ChangePrimaryWeapon(WeaponItemMeta meta, EquipmentPart? part = null)
        {
            _playerChannel.OnWeaponChanged(meta, part);
            PrimaryWeapon = meta;
        }
    }
}