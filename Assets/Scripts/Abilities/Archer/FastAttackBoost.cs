using Entity;
using ScriptableObjects.Traits;
using State;
using State.States.ArcherStates;
using UnityEngine;

namespace Abilities.Archer
{
    public class FastAttackBuff : Buff
    {
        private readonly float _fastAttackFactor;
        private Traits _traits;
        private float _previousDelaytBetweenAttacks;
        
        public FastAttackBuff(WorldEntity host, KeyCode hotKey, float buffTime, Sprite buffSprite,float fastAttackFactor) : base(host, hotKey, buffTime,buffSprite)
        {
            _fastAttackFactor = fastAttackFactor;
            _traits = host.Traits;
        }

        public override void Use()
        {
            _traits.DelayBetweenAttacks = _traits.BaseDelayBetweenAttacks / _fastAttackFactor;
        }

        public override void End()
        {
            _traits.DelayBetweenAttacks = _traits.BaseDelayBetweenAttacks * _fastAttackFactor;
        }
    }
}