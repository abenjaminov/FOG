using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Game
{
    public class Fog : MonoBehaviour
    {
        [SerializeField] private VisualEffect _visualEffect;
        [SerializeField] private Camera _cameraToFollow;

        private Vector3 SpawnSize => new Vector3(Screen.width * 1.1f, Screen.height * 1.1f, 0);
        private void Update()
        {
            var camPosition = _cameraToFollow.transform.position;
            
            _visualEffect.SetVector3("SpawnLocation", new Vector3(camPosition.x, camPosition.y, 0));
        }

        public void SetAlpha(float alpha)
        {
            _visualEffect.SetFloat("FogAlpha", alpha);
        }
    }
}