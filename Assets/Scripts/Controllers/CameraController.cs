﻿using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boundsCollider;
        private Transform _player;
        private UnityEngine.Camera _camera;
        
        private float _rightBound;
        private float _leftBound;
        private float _topBound;
        private float _bottomBound;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _player = FindObjectOfType<Entity.Player.Player>().transform;
        }

        private void Start()
        {
            var vertExtent = _camera.orthographicSize;
            var horizExtent = vertExtent * Screen.width / Screen.height;

            var bounds = _boundsCollider.bounds;

            _leftBound = bounds.min.x + horizExtent;
            _rightBound = bounds.max.x - horizExtent;
            _bottomBound = bounds.min.y + vertExtent;
            _topBound = bounds.max.y - vertExtent;
        }
        
        void Update () 
        {
            var position = _player.position;
            
            var pos = new Vector3(position.x, position.y, transform.position.z);
            pos.x = Mathf.Clamp(pos.x, _leftBound, _rightBound);
            pos.y = Mathf.Clamp(pos.y, _bottomBound, _topBound);
            transform.position = pos;
        }
    }
}