using Entity;
using State;
using State.States.ArcherStates;
using UnityEngine;

namespace Abilities.Archer
{
    public class FireArrowAbility : ShootBasicArrowAbility
    {
        public FireArrowAbility(WorldEntity host,KeyCode hotKey,  int numberEnemies, GameObject arrowPrefab, Transform fireTransform) 
            : base(host, hotKey, numberEnemies, arrowPrefab, fireTransform)
        {
            DamagePercentage = 1.3f;
        }
    }
}