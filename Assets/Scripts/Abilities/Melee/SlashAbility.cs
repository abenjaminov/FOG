using Assets.HeroEditor.Common.CharacterScripts;

namespace Abilities.Melee
{
    public class SlashAbility : MeleeAttack
    {
        private AnimationEvents _animationEvents;
        protected void Start()
        {
            _animationEvents = GetComponentInChildren<AnimationEvents>();
            _animationEvents.MeleeStrikeEvent += OnMeleeHit;
        }

        public override void Use()
        {
            _hostWrapper.GetCharacter().Slash();
        }
    }
}