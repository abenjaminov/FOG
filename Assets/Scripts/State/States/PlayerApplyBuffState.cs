using Abilities;
using Entity;

namespace State.States
{
    public class PlayerApplyBuffState<T> : PlayerAbilityState<T> where T : Buff
    {
        public bool IsBuffApplied;
        
        public PlayerApplyBuffState(CharacterWrapper character, T ability) : base(character, ability)
        {
        }
    }
}