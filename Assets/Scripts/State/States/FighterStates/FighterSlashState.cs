using Abilities.Melee;
using Entity;

namespace State.States.FighterStates
{
    public class FighterSlashState : PlayerAbilityState<SlashAbility>
    {
        private Entity.Player.Player _player;
        
        public FighterSlashState(CharacterWrapper character, SlashAbility ability) : 
            base(character,ability)
        {
            _player = character as Entity.Player.Player;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Ability.Use();
        }
    }
}