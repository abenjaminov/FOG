using Animations;
using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;
using UnityEngine;

namespace State.States
{
    public abstract class PlayerAttackState : IState
    {
        protected CharacterWrapper _character;
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private float _previousHorizontalVelocity;

        protected PlayerAttackState(CharacterWrapper character)
        {
            _animator = character.GetComponent<Animator>();
            _rigidbody2D = character.GetComponent<Rigidbody2D>();
            _character = character;
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
            _rigidbody2D.velocity = new Vector2(_previousHorizontalVelocity, _rigidbody2D.velocity.y);
            _character.GetCharacter().Relax();
        }
    }
}