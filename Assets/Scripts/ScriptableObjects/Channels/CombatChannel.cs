using Abilities;
using Entity;
using Entity.Enemies;
using Helpers;
using Platformer;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/Combat Channel", order = 1)]
    public class CombatChannel : ScriptableObject
    {
        public UnityAction<Enemy> EnemyDiedEvent;
        public UnityAction<Buff> BuffAppliedEvent;
        public UnityAction<Buff> BuffExpiredEvent;
        
        public void OnEntityHit(WorldEntity attacker, WorldEntity receiver, Ability ability = null)
        {
            var damage = TraitsHelper.CalculateDamage(attacker.Traits, receiver.Traits);
            var damageWithAbilityFactor = Mathf.Ceil(damage * (ability?.DamagePercentage ?? 1));
            receiver.ReceiveDamage(damageWithAbilityFactor);
        }

        public void OnEnemyDied(Enemy enemy)
        {
            EnemyDiedEvent?.Invoke(enemy);
        }

        public void OnBuffApplied(Buff buff)
        {
            BuffAppliedEvent?.Invoke(buff);
        }

        public void OnBuffExpired(Buff buff)
        {
            BuffExpiredEvent?.Invoke(buff);
        }
    }
}