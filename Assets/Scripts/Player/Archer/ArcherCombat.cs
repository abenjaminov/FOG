using ScriptableObjects;
using UnityEngine;

namespace Player.Archer
{
    public class ArcherCombat : MonoBehaviour
    {
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private Transform _arrowSpawnPosition;
        [SerializeField] private PlayerChannel _playerChannel;

        public void OnShootArrow()
        {
            var arrow = Instantiate(_arrowPrefab, _arrowSpawnPosition.position, transform.rotation).GetComponent<Arrow>();
            arrow.WorldMovementDirection = _playerChannel.FaceDirection;
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
    }
}
