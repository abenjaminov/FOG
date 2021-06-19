using System.Linq;
using Entity;
using Entity.Enemies;
using Newtonsoft.Json.Utilities;
using UnityEngine;

namespace Abilities
{
    public abstract class MeleeAttack : Attack
    {
        [SerializeField] private float _meleeRange;
        [SerializeField] private Transform _hitRayOrigin;
        
        protected void OnMeleeHit()
        {
            var worldDirection = _host.GetWorldMovementDirection();
            var hit = Physics2D.RaycastAll(_hitRayOrigin.position,worldDirection , _meleeRange).ToList();
            hit.AddRange(Physics2D.RaycastAll(_hitRayOrigin.position ,-worldDirection , _meleeRange / 3));

            var enemyColliders = hit.Select((x) => new
                {
                    entity = x.collider.GetComponentInParent<Enemy>(),
                    distance = x.distance
                }).Where(x => x.entity != null).OrderBy(x => x.distance)
                .ToList();

            if (enemyColliders.Count > 0)
            {
                var closestEnemy = enemyColliders[0];
                _combatChannel.OnEntityHit(_host, closestEnemy.entity, this);    
            }
        }
        
        
        private void Update()
        {
            Debug.DrawRay(_hitRayOrigin.position, _host.GetWorldMovementDirection() * _meleeRange, Color.red);
        }
    }
}