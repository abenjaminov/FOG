using System;
using UnityEngine;

namespace Game
{
    public class Ladder : MonoBehaviour
    {
        private const float EdgeHeight = 0.2f;
        private const float EdgeHalfHeight = EdgeHeight / 2;

        public Vector3 Center => transform.position;
        [SerializeField] private float _height;
        [SerializeField] private float _width;
        [SerializeField] private LadderEdge _upperEdge;
        [SerializeField] private LadderEdge _lowerEdge;

        private Vector3 _upperEdgePosition;
        private Vector3 _lowerEdgePosition;
        
        private BoxCollider2D _collider2D;
        
        private void Awake()
        {
            _collider2D = GetComponent<BoxCollider2D>();

            _collider2D.offset = Vector2.zero;
            _collider2D.size = new Vector3(_width, _height);
            
            var upperEdgeCollider = _upperEdge.GetComponent<BoxCollider2D>();
            upperEdgeCollider.offset = Vector2.zero;
            upperEdgeCollider.size = new Vector2(_width, EdgeHeight);
            
            var lowerEdgeCollider = _lowerEdge.GetComponent<BoxCollider2D>();
            lowerEdgeCollider.offset = Vector2.zero;
            lowerEdgeCollider.size = new Vector2(_width, EdgeHeight);
            
            SetEdgePositions();
        }

        private void SetEdgePositions()
        {
            _upperEdgePosition = new Vector3(_upperEdge.transform.position.x, (transform.position.y + (_height / 2)) - EdgeHalfHeight);
            _lowerEdgePosition = new Vector3(_lowerEdge.transform.position.x, (transform.position.y - (_height / 2)) + EdgeHalfHeight);
            _upperEdge.transform.position = _upperEdgePosition;
            _lowerEdge.transform.position = _lowerEdgePosition;
        }
        
        private void OnDrawGizmos()
        {
            SetEdgePositions();
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(_width, _height));
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_upperEdge.transform.position, new Vector3(1, 0.2f));
            Gizmos.DrawWireCube(_lowerEdge.transform.position, new Vector3(1, 0.2f));
        }

        
    }
}