using System;
using Player;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Game
{
    public class Drop : MonoBehaviour
    {
        private Sprite _sprite;
        [SerializeField] private float _lifeSpan;
        [SerializeField] private float _dropHeight;

        private GroundCheck _groundCheck;
        private FloatUpDown _floatComponent;
        private SpriteRenderer _renderer;
        private float _timeAlive;
        private Collider2D _collider;

        private float _speed;

        private void Awake()
        {
            _floatComponent = GetComponent<FloatUpDown>();
            _floatComponent.enabled = false;
            
            _renderer = GetComponent<SpriteRenderer>();

            _speed = Mathf.Sqrt(2 * 9.8f * _dropHeight);

            _groundCheck = GetComponent<GroundCheck>();
            _groundCheck.OnGroundChanged += OnGroundChanged;

            _collider = GetComponent<Collider2D>();
        }

        public void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
        }

        private void OnGroundChanged(bool isOnGround, float maxHeight)
        {
            if (!_floatComponent.enabled && isOnGround)
            {
                _floatComponent.enabled = true;
                _groundCheck.enabled = false;
                var position = transform.position;
                position = new Vector3(position.x, maxHeight - _collider.offset.y, position.z);
                transform.position = position;
            }
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;

            if (_timeAlive >= _lifeSpan)
            {
                Destroy(gameObject);
            }

            if (!_groundCheck.enabled) return;
            
            transform.Translate(Vector3.up * (_speed * Time.deltaTime));

            _speed -= 9.8f * Time.deltaTime;
        }
    }
}