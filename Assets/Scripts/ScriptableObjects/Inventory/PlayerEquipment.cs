using HeroEditor.Common;
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
    }
}