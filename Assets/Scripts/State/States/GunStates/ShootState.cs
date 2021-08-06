using Abilities.Gun;
using Entity;

namespace State.States.GunStates
{
    public class ShootState : PlayerAbilityState<ShootAttackAbility>
    {
        public ShootState(CharacterWrapper character, ShootAttackAbility ability) : base(character, ability)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Ability.Use();
        }
    }
}