using System;
using UnityEngine;

namespace Game
{
    public class FadeoutToPoint : MonoBehaviour
    {
        private Transform _transformToFollow;
        private float timeAlive = 0;
        private SpriteRenderer _renderer;
        private float _startingDistance;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetTransformToFollow(Transform transform)
        {
            _transformToFollow = transform;
            _startingDistance = Vector3.Distance(_transformToFollow.position, transform.position);
        }

        private void Update()
        {
            if (_transformToFollow == null) return;
            
            timeAlive += Time.deltaTime;
            MoveObject();

            var distance = Vector3.Distance(_transformToFollow.position, transform.position);
            SetAlpha(distance);
        }

        private void MoveObject()
        {
            var position = transform.position;
            var direction = _transformToFollow.position - position;
            Debug.DrawLine(position, position + direction, Color.red);

            direction.Normalize();
            transform.Translate(direction * (Mathf.Exp(6 * timeAlive) * Time.deltaTime));
        }

        private void SetAlpha(float distance)
        {
            var color = _renderer.color;
            color = new Color(color.r, color.g, color.b, distance / _startingDistance);
            _renderer.color = color;
        }
    }
}