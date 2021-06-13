using Abilities;
using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using UnityEngine;

namespace State.States
{
    public abstract class PlayerAbilityState<T> : IAbilityState where T : Ability
    {
        public T Ability; 
        protected CharacterWrapper _character;
        private Rigidbody2D _rigidbody2D;
        private float _previousHorizontalVelocity;
        protected AnimationEvents _animationEvents;
        

        protected PlayerAbilityState(CharacterWrapper character, T ability)
        {
            _rigidbody2D = character.GetComponent<Rigidbody2D>();
            _character = character;
            Ability = ability;
            _animationEvents = character.GetComponentInChildren<AnimationEvents>();
        }

        public void Tick() { }

        public virtual void OnEnter()
        {
            _previousHorizontalVelocity = _rigidbody2D.velocity.x;
            _character.GetReady();
            
            if (_rigidbody2D.velocity.y == 0)
            {
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }
        }

        public virtual void OnExit()
        {
            Ability.Use();
            _rigidbody2D.velocity = new Vector2(_previousHorizontalVelocity, _rigidbody2D.velocity.y);
            _character.GetCharacter().Relax();
        }

        public KeyCode GetHotKey()
        {
            return Ability.HotKey;
        }

        public void SetHotKeyDown(bool isDown)
        {
            Ability.IsHotKeyDown = isDown;
        }

        public bool IsHotKeyDown()
        {
            return Ability.IsHotKeyDown;
        }
    }
}