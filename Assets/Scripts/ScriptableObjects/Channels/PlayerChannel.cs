using ScriptableObjects.Inventory.ItemMetas;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Player Channel", menuName = "Channels/Player Channel", order = 6)]
    public class PlayerChannel : BaseChannel
    {
        public UnityAction<WeaponItemMeta> WeaponChangedEvent;
        
        public void OnWeaponChanged(WeaponItemMeta newWeapon)
        {
            InvokeEvent(() =>
            {
                WeaponChangedEvent?.Invoke(newWeapon);    
            });
        }
    }
}