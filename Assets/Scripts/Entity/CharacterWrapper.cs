using System;
using System.Collections.Generic;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common;
using Platformer;
using ScriptableObjects;
using ScriptableObjects.Inventory;
using ScriptableObjects.Inventory.ItemMetas;
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
        protected Assets.HeroEditor.Common.CharacterScripts.Character _character;
        public AnimationEvents AnimationEvents;
        
        protected virtual void Start()
        {
            AnimationEvents = GetComponentInChildren<AnimationEvents>();

            _character = GetComponentInChildren<Assets.HeroEditor.Common.CharacterScripts.Character>();
        }
        
        public Assets.HeroEditor.Common.CharacterScripts.Character GetCharacter()
        {
            if (_character == null)
            {
                _character = GetComponentInChildren<Assets.HeroEditor.Common.CharacterScripts.Character>();
            }

            return _character;
        }
        
        public void GetReady()
        {
            _character.GetReady();
        }

        public void Climb()
        {
            _character.SetState(CharacterState.Climb);
        }
    }
}