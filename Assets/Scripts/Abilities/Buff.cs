using Entity;
using UnityEngine;

namespace Abilities
{
    public abstract class Buff : Ability
    {
        public float BuffTime;
        [HideInInspector] public float TimeUntillBuffEnds;
        public Sprite BuffSprite;

        public override void Use()
        {
            TimeUntillBuffEnds = BuffTime;
        }

        public abstract void End();
    }
}