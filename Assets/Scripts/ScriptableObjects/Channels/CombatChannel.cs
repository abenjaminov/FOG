using Abilities;
using Entity.Enemies;
using Helpers;
using ScriptableObjects.Channels.Info;
using ScriptableObjects.Inventory;
using ScriptableObjects.Traits;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/Combat Channel", order = 1)]
    public class CombatChannel : ScriptableObject
    {
        [SerializeField] private PlayerEquipment _playerEquipment;
        [SerializeField] private PlayerTraits _playerTraits;

        public UnityAction<UseAbilityEventInfo> UseAbilityEvent;
        public UnityAction<EnemyHitEventInfo> EnemyHitEvent;
        public UnityAction<Enemy> EnemyDiedEvent;
        public UnityAction<Buff> BuffAppliedEvent;
        public UnityAction<Buff> BuffExpiredEvent;

        public void OnUseAbility(Entity.Player.Player player)
        {
            UseAbilityEvent?.Invoke(new UseAbilityEventInfo()
            {
                Weapon = player.Equipment.PrimaryWeapon
            });
        }
        
        public void OnEnemyHit(Entity.Player.Player player, Enemy enemy, Ability ability = null)
        {
            var damage = TraitsHelper.CalculateDamageInflictedByPlayer(player.PlayerTraits, player.Equipment);
            var damageWithAbilityFactor = Mathf.Ceil(damage * (ability != null ? ability.DamagePercentage : 1));
            enemy.ReceiveDamage(damageWithAbilityFactor);
            
            EnemyHitEvent?.Invoke(new EnemyHitEventInfo()
            {
                Damage = damageWithAbilityFactor,
                Enemy = enemy,
                Weapon = player.Equipment.PrimaryWeapon
            });
        }

        public void OnPlayerHit(Entity.Player.Player player, Enemy enemy)
        {
            var damageToPlayer = TraitsHelper.GetEnemyDamage(enemy, _playerEquipment, _playerTraits);
            player.ReceiveDamage(damageToPlayer);
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