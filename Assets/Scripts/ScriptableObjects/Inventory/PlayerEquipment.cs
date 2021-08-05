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
        public EquipmentItemMeta Torso;
        public EquipmentItemMeta Boots;
        public EquipmentItemMeta Pelvis;
        public EquipmentItemMeta Cape;
        public EquipmentItemMeta Gloves;
        public WeaponItemMeta PrimaryWeapon;
        public WeaponItemMeta SecondaryWeapon;

        private Dictionary<EquipmentPart, Func<EquipmentItemMeta>> MetaByType;
        
        private void OnEnable()
        {
            MetaByType = new Dictionary<EquipmentPart, Func<EquipmentItemMeta>>()
            {
                {EquipmentPart.Helmet, () => Helmet},
                {EquipmentPart.Vest, () => Torso},
                {EquipmentPart.Boots, () => Boots},
                {EquipmentPart.Belt, () => Pelvis},
                {EquipmentPart.Cape, () => Cape},
                {EquipmentPart.Gloves, () => Gloves},
                {EquipmentPart.MeleeWeapon1H, () => PrimaryWeapon},
                {EquipmentPart.Bow, () => PrimaryWeapon},
            };
        }

        public EquipmentItemMeta GetItemMetaByPartType(EquipmentPart partType)
        {
            return MetaByType[partType]();
        }
        
        public void SetMetaItem(EquipmentItemMeta meta)
        {
            if (meta == null) return;

            switch (meta.Part)
            {
                case EquipmentPart.Vest:
                    Torso = meta;
                    break;
                case EquipmentPart.Boots:
                    Boots = meta;
                    break;
                case EquipmentPart.Helmet:
                    Helmet = meta;
                    break;
                case EquipmentPart.Belt:
                    Pelvis = meta;
                    break;
                case EquipmentPart.Cape:
                    Cape = meta;
                    break;
                case EquipmentPart.Gloves:
                    Gloves = meta;
                    break;
                case EquipmentPart.MeleeWeapon1H:
                    ChangePrimaryWeapon(meta as WeaponItemMeta);
                    break;
                case EquipmentPart.Bow:
                    ChangePrimaryWeapon(meta as WeaponItemMeta);
                    break;
                default:
                    break;
            }
        }

        private void ChangePrimaryWeapon(WeaponItemMeta meta)
        {
            _playerChannel.OnWeaponChanged(meta);
            PrimaryWeapon = meta;
        }
    }
}