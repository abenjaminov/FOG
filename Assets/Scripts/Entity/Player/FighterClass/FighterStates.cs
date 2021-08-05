using Abilities.Fighter;
using Player;
using State.States.FighterStates;

namespace Entity.Player.FighterClass
{
    public class FighterStates : PlayerStates
    {
        private Entity.Player.Player _fighter;

        protected override void Start()
        {
            _fighter = GetComponent<Entity.Player.Player>();
            
            _basicAttackState = new FighterSlashState(_fighter, _basicAttackAbility as SlashAbility);
            
            base.Start();
            
            _animationEvents.SlashEndEvent += SlashEndEvent;
        }

        private void SlashEndEvent()
        {
            _isAbilityAnimationActivated = false;
        }
    }
}