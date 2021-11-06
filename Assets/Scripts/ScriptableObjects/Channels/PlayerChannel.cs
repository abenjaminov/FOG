using HeroEditor.Common.Enums;
using ScriptableObjects.Inventory.ItemMetas;
using UI.Screens;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Player Channel", menuName = "Channels/Player Channel", order = 6)]
    public class PlayerChannel : BaseChannel
    {
        public UnityAction LevelUpEvent;
        public UnityAction<float> GainedResistancePointsEvent;
        public UnityAction MonsterResistanceChangedEvent;
        public UnityAction TraitsChangedEvent;
        public UnityAction ReviveEvent;
        public UnityAction<WeaponItemMeta, EquipmentPart> WeaponChangedEvent;
        public UnityAction<EquipmentItemMeta, EquipmentPart> ItemEquippedEvent;
        public UnityAction<EquipmentItemMeta, EquipmentPart> ItemUnEquippedEvent;

        public void OnGainedResistancePointsEvent(float amount)
        {
            GainedResistancePointsEvent?.Invoke(amount);
        }

        public void OnMonsterResistanceChanged()
        {
            MonsterResistanceChangedEvent?.Invoke();
        }

        public void OnTraitsChangedEvent()
        {
            TraitsChangedEvent?.Invoke();
        }

        public void OnRevive()
        {
            ReviveEvent?.Invoke();
        }
        
        public void OnLevelUp()
        {
            LevelUpEvent?.Invoke();
        }
        
        public void OnWeaponChanged(WeaponItemMeta newWeapon, EquipmentPart? part = null)
        {
            InvokeEvent(() =>
            {
                WeaponChangedEvent?.Invoke(newWeapon, part ?? newWeapon.Part);    
            });
        }

        public void OnItemEquipped(EquipmentItemMeta itemMeta, EquipmentPart? part = null)
        {
            ItemEquippedEvent?.Invoke(itemMeta, part ?? itemMeta.Part);
        }
        
        public void OnItemUnEquipped(EquipmentItemMeta itemMeta, EquipmentPart? part = null)
        {
            ItemUnEquippedEvent?.Invoke(itemMeta, part ?? itemMeta.Part);
        }
    }
}