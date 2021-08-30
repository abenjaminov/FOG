using Abilities.Magic;
using Entity;

namespace State.States.MagicStates
{
    public class MagicAttackState : PlayerAbilityState<MagicAttackAbility>
    {
        public MagicAttackState(CharacterWrapper character, MagicAttackAbility ability) : base(character, ability)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            Ability.Use();
        }
    }
}