using Entity;
using State;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability
    {
        protected WorldEntity _host;
        public KeyCode HotKey;
        public bool IsHotKeyDown;
        public float DamagePercentage;
        
        protected Ability(WorldEntity host, KeyCode hotKey)
        {
            _host = host;
            HotKey = hotKey;
        }

        public abstract void Use();
    }
}