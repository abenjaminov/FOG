using Assets.HeroEditor.Common.CharacterScripts;
using Character;
using Entity;

namespace State.States.PlayerStates
{
    public class PlayerIdleState : IdleState
    {
        private Entity.Player.Player _player;
        
        public PlayerIdleState(Entity.Player.Player player, ICharacterMovement characterMovement) : base(player, characterMovement)
        {
            _player = player;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _player.GetCharacter().SetState(CharacterState.Idle);
        }
    }
}