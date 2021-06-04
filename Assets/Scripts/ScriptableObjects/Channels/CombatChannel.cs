using Character;
using Character.Enemies;
using Platformer;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Channels
{
    [CreateAssetMenu(fileName = "Combat Channel", menuName = "Channels/Combat Channel", order = 1)]
    public class CombatChannel : ScriptableObject
    {
        public UnityAction<Enemy> EnemyDiedEvent;
        
        public void OnCharacterHit(CharacterWrapper attacker, CharacterWrapper receiver)
        {
            var damage = TraitsCalculator.CalculateDamage(attacker.Traits, receiver.Traits);
            receiver.ReceiveDamage(damage);
        }

        public void OnEnemyDied(Enemy enemy)
        {
            EnemyDiedEvent?.Invoke(enemy);
        }
    }
}