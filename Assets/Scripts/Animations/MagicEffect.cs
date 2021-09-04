using System;
using UnityEngine;
using UnityEngine.Events;

namespace Animations
{
    public class MagicEffect : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        public UnityAction EffectCompletedEvent;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_particleSystem.IsAlive()) return;
            
            EffectCompletedEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}