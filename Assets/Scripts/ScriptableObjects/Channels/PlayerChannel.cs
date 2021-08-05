using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Player Channel", menuName = "Channels/Player Channel", order = 6)]
    public class PlayerChannel : ScriptableObject
    {
        public UnityAction<EquipmentItemMeta> WeaponChangedEvent;
        
        public void OnWeaponChanged(EquipmentItemMeta newWeapon)
        {
            WeaponChangedEvent?.Invoke(newWeapon);
        }
    }
}