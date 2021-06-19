using Entity;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Abilities
{
    public abstract class Attack : Ability
    {
        protected CharacterWrapper _hostWrapper;
        public int NumberOfEnemies;
        [SerializeField] protected CombatChannel _combatChannel;

        protected override void Awake()
        {
            base.Awake();
            
            _hostWrapper = _host as CharacterWrapper;
        }
    }
}