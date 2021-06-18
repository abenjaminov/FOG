using Entity;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Abilities.Archer
{
    public class FastAttackBuff : Buff
    {
        [SerializeField] private float _fastAttackFactor;
        private Traits _traits;
        private float _previousDelaytBetweenAttacks;

        protected override void Awake()
        {
            base.Awake();

            _traits = _host.Traits;
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