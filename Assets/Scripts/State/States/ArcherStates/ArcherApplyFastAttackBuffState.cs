using Abilities.Bow;
using Entity;
using Entity.Player;
using UnityEngine;

namespace State.States.ArcherStates
{
    public class ArcherApplyFastAttackBuffState : PlayerApplyBuffState<FastAttackBuff>
    {
        private WorldEntitiyBuffs _buffs;
        
        public ArcherApplyFastAttackBuffState(CharacterWrapper character, FastAttackBuff ability) : base(character, ability)
        {
            _buffs = character.GetComponent<WorldEntitiyBuffs>();
        }

        public override void OnEnter()
        {
            _buffs.ApplyBuff(Ability);
            IsBuffApplied = true;
        }

        public override void OnExit()
        {
            IsBuffApplied = false;
        }
    }
}