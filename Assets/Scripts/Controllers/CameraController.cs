using System;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEditor;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        private BoxCollider2D _boundsCollider;
        [SerializeField] private LocationsChannel _locationsChannel;
        [SerializeField] private float _followSpeed;
        private Transform _playerTransform;
        private Camera _camera;
        
        private float _rightBound;
        private float _leftBound;
        private float _topBound;
        private float _bottomBound;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _playerTransform = FindObjectOfType<Entity.Player.Player>(true).transform;
            
            _locationsChannel.ChangeLocationCompleteEvent += ChangeLocationCompleteEvent;
        }

        private void OnDestroy()
        {
            _locationsChannel.ChangeLocationCompleteEvent -= ChangeLocationCompleteEvent;
        }

        private void ChangeLocationCompleteEvent(SceneMeta arg0, SceneMeta arg1)
        {
            var levelBounds = GameObject.FindGameObjectWithTag("LevelBounds");

            if (levelBounds == null)
            {
                Debug.LogError("There is no Object with LevelBounds Tag");
                return;
            }
            
            _boundsCollider = GameObject.FindGameObjectWithTag("LevelBounds").GetComponent<BoxCollider2D>();
            
            UpdateBounds();
            
            var destination = GetClampedDestinationPosition(transform.position);
            transform.position = destination;
        }

        private void UpdateBounds()
        {
            var vertExtent = _camera.orthographicSize;
            var horizExtent = vertExtent * Screen.width / Screen.height;

            var bounds = _boundsCollider.bounds;

            _leftBound = bounds.min.x + horizExtent;
            _rightBound = bounds.max.x - horizExtent;
            _bottomBound = bounds.min.y + vertExtent;
            _topBound = bounds.max.y - vertExtent;
        }

        void FixedUpdate () 
        {
            var currentPosition = transform.position;
            
            var destination = GetClampedDestinationPosition(currentPosition);
            currentPosition = Vector3.Lerp(currentPosition, destination, _followSpeed * Time.deltaTime);
            transform.position = currentPosition;
        }

        private Vector3 GetClampedDestinationPosition(Vector3 currentPosition)
        {
            var playerPosition = _playerTransform.position;
            var destination = new Vector3(playerPosition.x, playerPosition.y, currentPosition.z);
            destination.x = Mathf.Clamp(destination.x, _leftBound, _rightBound);
            destination.y = Mathf.Clamp(destination.y, _bottomBound, _topBound);
            return destination;
        }
    }
}