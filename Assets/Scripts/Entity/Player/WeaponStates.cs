using System;
using Abilities;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;
using Player;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using State.States;
using UnityEngine;
using UnityEngine.Events;

namespace Entity.Player
{
    public abstract class WeaponStates : MonoBehaviour
    {
        public bool IsAbilityAnimationActivated;
        protected AnimationEvents AnimationEvents;
        [SerializeField] protected PlayerChannel _playerChannel;

        [HideInInspector] public bool IsEnabled;

        [SerializeField] protected PlayerStates _playerStates;

        protected abstract EquipmentPart WeaponEquipmentType { get; }

        public UnityAction<WeaponStates> StatesActivatedEvent;

        protected virtual void Awake()
        {
            _playerChannel.WeaponChangedEvent += OnWeaponChanged;
            AnimationEvents = GetComponentInChildren<AnimationEvents>();
        }
        
        protected virtual void OnWeaponChanged(WeaponItemMeta weapon, EquipmentPart part)
        {
            IsEnabled = weapon != null && weapon.Part == WeaponEquipmentType;

            TryEnableStates();
        }
        
        public virtual void Initialize() {}
        
        public abstract void HookStates();

        protected abstract void ActivateStates();
        protected abstract void DeActivateStates();
        
        protected void TryEnableStates()
        {
            if (IsEnabled)
            {
                ActivateStates();
                
                StatesActivatedEvent?.Invoke(this);
            }
        }
        
        private void OnDisable()
        {
            _playerChannel.WeaponChangedEvent -= OnWeaponChanged;
        }
    }
    
    public abstract class WeaponStates<TBasicAbilityType> : WeaponStates where TBasicAbilityType : Ability
    {
        [SerializeField] protected TBasicAbilityType _basicAttackAbility;
        [HideInInspector] public PlayerAbilityState<TBasicAbilityType> BasicAttackState;
    }
}