using Abilities;
using Entity;
using Entity.Enemies;
using Platformer;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/Combat Channel", order = 1)]
    public class CombatChannel : ScriptableObject
    {
        public UnityAction<Enemy> EnemyDiedEvent;
        
        public void OnEntityHit(WorldEntity attacker, WorldEntity receiver, Ability ability = null)
        {
            var damage = TraitsCalculator.CalculateDamage(attacker.Traits, receiver.Traits);
            var damageWithAbilityFactor = Mathf.Ceil(damage * (ability?.DamagePercentage ?? 1));
            receiver.ReceiveDamage(damageWithAbilityFactor);
        }

        public void OnEnemyDied(Enemy enemy)
        {
            EnemyDiedEvent?.Invoke(enemy);
        }
    }
}