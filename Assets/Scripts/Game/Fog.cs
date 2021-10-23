using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Game
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private VisualEffect _visualEffect;

        private void Start()
        {
            Stop();
        }

        public void SetAlpha(float alpha)
        {
            _visualEffect.SetFloat("FogAlpha", alpha);
        }

        public void SetSpawnPosition(Vector3 position)
        {
            _visualEffect.SetVector3("SpawnLocation", position);
        }
        
        public void SetSpawnBoxSize(Vector3 size)
        {
            _visualEffect.SetVector3("SpawnSize", size);
        }

        public void Play()
        {
            _visualEffect.Play();
        }

        public void Stop()
        {
            _visualEffect.Stop();
        }
    }
}