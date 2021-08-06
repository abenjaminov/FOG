using UnityEngine;

namespace Abilities.Gun
{
    public class ShootAttackAbility : Attack
    {
        public override void Use()
        {
            _hostWrapper.GetCharacter().Shoot();
        }
    }
}