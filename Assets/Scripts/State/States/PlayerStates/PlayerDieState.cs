using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using UnityEngine;

namespace State.States.PlayerStates
{
    public class PlayerDieState : DieState
    {
        private Entity.Player.Player _player;
        
        public PlayerDieState(Entity.Player.Player player,
            ICharacterMovement characterMovement, Animator animator) : base(player, characterMovement, animator)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _characterMovement.SetVelocity(Vector2.zero);
            _player.GetCharacter().SetState(CharacterState.DeathB);
        }
    }
}