using System;
using System.Collections.Generic;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Player Equipment", menuName = "Inventory/Player Equipment", order = 2)]
    public class PlayerEquipment : ScriptableObject
    {
        public EquipmentItemMeta Helmet;
        public EquipmentItemMeta Torso;
        public EquipmentItemMeta Boots;
        public EquipmentItemMeta Pelvis;
        public EquipmentItemMeta Cape;
        public EquipmentItemMeta Gloves;
        public EquipmentItemMeta PrimaryWeapon;
        public EquipmentItemMeta SecondaryWeapon;

        public Dictionary<EquipmentPart, EquipmentItemMeta> MetaByType;
        
        private void OnEnable()
        {
            this.MetaByType = new Dictionary<EquipmentPart, EquipmentItemMeta>()
            {
                {EquipmentPart.Helmet, this.Helmet},
                {EquipmentPart.Vest, this.Torso},
                {EquipmentPart.Boots, this.Boots},
                {EquipmentPart.Belt, this.Pelvis},
                {EquipmentPart.Cape, this.Cape},
                {EquipmentPart.Gloves, this.Gloves},
            };
        }

        public void SetMetaItem(EquipmentItemMeta meta)
        {
            if (meta == null) return;
            
            MetaByType[meta.Part] = meta;

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
                default:
                    break;
            }
        }
    }
}