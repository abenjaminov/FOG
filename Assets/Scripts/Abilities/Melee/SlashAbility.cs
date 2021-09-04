using Assets.HeroEditor.Common.CharacterScripts;

namespace Abilities.Melee
{
    public class SlashAbility : MeleeAttack
    {
        private AnimationEvents _animationEvents;
        protected override void Awake()
        {
            base.Awake();
            _animationEvents = GetComponentInChildren<AnimationEvents>();
        }

        public override void Use()
        {
            _hostWrapper.GetCharacter().Slash();
        }

        public override void Activate()
        {
            _animationEvents.MeleeStrikeEvent += OnMeleeHit;
        }

        public override void Deactivate()
        {
            _animationEvents.MeleeStrikeEvent -= OnMeleeHit;
        }
    }
}