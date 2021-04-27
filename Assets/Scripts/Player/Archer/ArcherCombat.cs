using System;
using Platformer;
using ScriptableObjects;
using UnityEngine;

namespace Player.Archer
{
    public class ArcherCombat : Character
    {
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private Transform _arrowSpawnPosition;
        [SerializeField] private PlayerChannel _playerChannel;

        public void OnShootArrow()
        {
            var arrow = Instantiate(_arrowPrefab, _arrowSpawnPosition.position, transform.rotation).GetComponent<Arrow>();
            arrow.WorldMovementDirection = _playerChannel.FaceDirection;
            arrow.ParentCharacter = this;
        }

        public void OnShootEnd()
        {
            _playerChannel.OnMovementActive(true);
        }
        
        void Update()
        {
            if (!_playerChannel.IsJumping && Input.GetKeyDown(KeyCode.LeftControl))
            {
                _playerChannel.OnAttack1();
                _playerChannel.OnMovementActive(false);
            }
        }

        public override void ReceiveDamage(float damage)
        {
            Debug.Log("Archer Hit");
        }
    }
}
