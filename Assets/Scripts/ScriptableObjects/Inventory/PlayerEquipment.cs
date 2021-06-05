using HeroEditor.Common;
using UnityEngine;

namespace ScriptableObjects.Inventory
{
    [CreateAssetMenu(fileName = "Player Equipment", menuName = "Inventory/Player Equipment", order = 2)]
    public class PlayerEquipment : ScriptableObject
    {
        public EquipmentItemMeta Helmet;
        public EquipmentItemMeta Armour;
        public EquipmentItemMeta Boots;
        public EquipmentItemMeta Belt;
        public EquipmentItemMeta Cape;
        public EquipmentItemMeta Gloves;
        public EquipmentItemMeta PrimaryWeapon;
        public EquipmentItemMeta SecondaryWeapon;
    }
}