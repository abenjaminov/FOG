using System;
using Entity;
using State;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        protected WorldEntity _host;
        public KeyCode HotKey;
        [HideInInspector] public bool IsHotKeyDown;
        public float DamagePercentage;

        protected virtual void Awake()
        {
            _host = GetComponent<WorldEntity>();
        }

        public abstract void Use();
    }
}