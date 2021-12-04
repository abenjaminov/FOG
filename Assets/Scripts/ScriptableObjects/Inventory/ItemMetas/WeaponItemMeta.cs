using UnityEngine;

namespace ScriptableObjects.Inventory.ItemMetas
{
    [CreateAssetMenu(fileName = "Inventory Weapon Meta", menuName = "Inventory/Weapon", order = 4)]
    public class WeaponItemMeta : EquipmentItemMeta
    {
        public AudioClip UseSFX;
        public AudioClip HitSFX;
        public bool IsStaff;
    }
}