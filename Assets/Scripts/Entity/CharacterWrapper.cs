using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common;
using Platformer;
using ScriptableObjects;
using ScriptableObjects.Inventory;
using State;
using UI;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Entity
{
    public abstract class CharacterWrapper : WorldEntity
    {
        protected AnimationEvents _animationEvents;

        [Header("Character Pack")]
        [SerializeField] protected Assets.HeroEditor.Common.CharacterScripts.Character _character;
        [SerializeField] private Transform ArmL;
        [SerializeField] private Transform ArmR;

        protected virtual void Awake()
        {
            base.Awake();
            _animationEvents = GetComponentInChildren<AnimationEvents>();
        }
        
        public Assets.HeroEditor.Common.CharacterScripts.Character GetCharacter()
        {
            return _character;
        }
        
        public void GetReady()
        {
            _character.GetReady();
        }

        public void EquipItem(EquipmentItemMeta meta)
        {
            _character.Equip(meta.Item, meta.Part);
        }
    }
}