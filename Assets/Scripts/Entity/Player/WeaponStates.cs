using System;
using Abilities;
using HeroEditor.Common.Enums;
using Player;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using State.States;
using UnityEngine;

namespace Entity.Player
{
    public abstract class WeaponStates<TBasicAbilityType> : MonoBehaviour where TBasicAbilityType : Ability
    {
        [SerializeField] protected TBasicAbilityType _basicAttackAbility;
        [HideInInspector] public PlayerAbilityState<TBasicAbilityType> BasicAttackState;
        
        [SerializeField] private PlayerChannel _playerChannel;

        [HideInInspector] public bool IsEnabled;

        protected PlayerStates _playerStates;

        protected abstract EquipmentPart WeaponEquipmentType { get; }

        private void Awake()
        {
            _playerChannel.WeaponChangedEvent += OnWeaponChanged;
        }

        protected virtual void OnWeaponChanged(WeaponItemMeta weapon)
        {
            IsEnabled = weapon.Part == WeaponEquipmentType;

            if (IsEnabled)
            {
                ActivateStates();
            }
        }

        public abstract void LinkToStates(PlayerStates playerStates);

        protected abstract void ActivateStates();
        protected abstract void DeActivateStates();

        private void OnDisable()
        {
            _playerChannel.WeaponChangedEvent -= OnWeaponChanged;
        }
    }
}