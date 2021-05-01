using System;
using System.Numerics;
using Platformer;
using ScriptableObjects;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Player.Archer
{
    public class ArcherCombat : Character
    {
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private Transform _arrowSpawnPosition;
        [SerializeField] private PlayerChannel _playerChannel;
        public Vector2 WorldMovementDirection;
        [SerializeField] private Traits _archerTraits;

        public void OnShootArrow()
        {
            var arrow = Instantiate(_arrowPrefab, _arrowSpawnPosition.position, transform.rotation).GetComponent<Arrow>();
            arrow.WorldMovementDirection = WorldMovementDirection;
            arrow.ParentCharacter = this;
            arrow.Range = TraitsCalculator.CalculateAttackRange(_archerTraits);
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