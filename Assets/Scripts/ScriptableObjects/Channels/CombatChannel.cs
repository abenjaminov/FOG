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
        
        public void OnEnemyHit(Entity.Player.Player player, Enemy enemy, Ability ability = null)
        {
            var damage = TraitsHelper.CalculatePlayerDamage(player.PlayerTraits, player.Equipment);
            var damageWithAbilityFactor = Mathf.Ceil(damage * (ability?.DamagePercentage ?? 1));
            enemy.ReceiveDamage(damageWithAbilityFactor);
        }

        public void OnPlayerHit(Entity.Player.Player player, Enemy enemy)
        {
            player.ReceiveDamage(1);
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