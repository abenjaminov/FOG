using System.Linq;
using Entity.Enemies;
using UnityEngine;

namespace Abilities.Magic
{
    public class MagicAttackAbility : Attack
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layer;
        
        public override void Use()
        {
            _hostWrapper.GetCharacter().Slash();
            
            var position = transform.position;

            var direction = _host.GetWorldMovementDirection();
            
            var enemies = Physics2D.OverlapAreaAll(position, 
                                     position + (new Vector3(direction.x, 1) * _range), _layer)
                .OrderBy(x => Vector2.Distance(position, x.transform.position))
                .Take(NumberOfEnemies).Select(x => x.GetComponent<Enemy>()).ToList();
            
            enemies.ForEach(x =>
            {
                _combatChannel.OnEnemyHit((Entity.Player.Player)_host, x, this);    
            });
        }
    }
}