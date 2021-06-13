using Entity;
using UnityEngine;

namespace Abilities
{
    public abstract class Buff : Ability
    {
        public float BuffTime;
        public float TimeUntillBuffEnds;
        public Sprite BuffSprite;
        
        public Buff(WorldEntity host, KeyCode hotKey, float buffTime, Sprite buffSprite) : base(host, hotKey)
        {
            BuffTime = buffTime;
            BuffSprite = buffSprite;
        }

        public override void Use()
        {
            TimeUntillBuffEnds = BuffTime;
        }

        public abstract void End();
    }
}