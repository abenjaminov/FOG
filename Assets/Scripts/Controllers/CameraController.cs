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

        void Update () 
        {
            var position = _playerTransform.position;
            
            var pos = new Vector3(position.x, position.y, transform.position.z);
            pos.x = Mathf.Clamp(pos.x, _leftBound, _rightBound);
            pos.y = Mathf.Clamp(pos.y, _bottomBound, _topBound);
            transform.position = pos;
        }
    }
}