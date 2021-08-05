using System;
using Abilities;
using Abilities.Archer;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;
using Player;
using ScriptableObjects.Channels;
using ScriptableObjects.Inventory.ItemMetas;
using State.States;
using State.States.ArcherStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entity.Player.ArcherClass
{
    public class BowStates : MonoBehaviour
    {
        private Player _archer;

        [SerializeField] private PlayerChannel _playerChannel;
        
        [SerializeField] protected Ability _basicAttackAbility;
        [HideInInspector] public PlayerAbilityState<ShootArrowAbility> BasicAttackState;
        
        [Header("Shoot Fire Arrow Ability")]
        [SerializeField] private ShootArrowAbility _fireArrowAbility;

        [Header("Fast Attack Buff")]
        [SerializeField] private FastAttackBuff _fastAttackBuff;

        [HideInInspector] public bool IsEnabled; 
        
        private PlayerStates _playerStates;

        private void Start()
        {
            _playerChannel.WeaponChangedEvent += OnWeaponChanged;
        }

        private void OnWeaponChanged(EquipmentItemMeta weapon)
        {
            IsEnabled = weapon.Part == EquipmentPart.Bow;

            if (IsEnabled)
            {
                Enable();
            }
        }

        public void LinkToStates(PlayerStates playerStates)
        {
            _archer = GetComponent<Player>();
            _playerStates = playerStates;
            BasicAttackState = new ArcherShootArrowAbilityState(_archer, _basicAttackAbility as ShootArrowAbility);
        }

        private void Enable()
        {
            _playerStates.AnimationEvents.BowChargeEndEvent += BowChargeEndEvent;
            var strongArrowState = new ArcherShootArrowAbilityState(_archer, _fireArrowAbility);
            
            _playerStates.AddAttackState(strongArrowState);

            var fastAttackBuffState =
                new ArcherApplyFastAttackBuffState(_archer, _fastAttackBuff);
            
            _playerStates.AddBuffState(fastAttackBuffState,() => fastAttackBuffState.IsBuffApplied);
        }

        private void BowChargeEndEvent()
        {
            _playerStates.IsAbilityAnimationActivated = false;
        }
    }
}