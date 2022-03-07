using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Game
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private VisualEffect _visualEffect;

        [SerializeField] private bool DebugFog;
        [Range(0, 1)] [SerializeField] private float FogAlpha;
        
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

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

        [ContextMenu("Play")]
        public void Play()
        {
            _visualEffect.Play();
        }

        public void GradualStop()
        {
            _visualEffect.Stop();
        }
        
        public void Stop()
        {
            _visualEffect.Reinit();
            _visualEffect.Stop();
        }

        private void Update()
        {
            if (DebugFog)
            {
                SetAlpha(FogAlpha);
            }
            
            if (_mainCamera == null) return;
            
            transform.position = _mainCamera.transform.position + new Vector3(0,0,5f);
        }
    }
}