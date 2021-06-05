using Entity;
using Entity.Enemies;
using ScriptableObjects;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Character.Player.Archer
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private CombatChannel _combatChannel;
        [HideInInspector] public CharacterWrapper ParentCharacter;
        [SerializeField] private float _speed; 
        [HideInInspector] public Vector2 WorldMovementDirection;
        public float Range;
        private Vector2 _spawnPosition;

        private void Awake()
        {
            _spawnPosition = this.transform.position;
        }

        private void Update()
        {
            transform.Translate(WorldMovementDirection * (_speed * Time.deltaTime), Space.World);

            if (Vector2.Distance(transform.position, _spawnPosition) >= Range)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(typeof(WorldEntity), out var character))
            {
                _combatChannel.OnEntityHit(ParentCharacter, (WorldEntity)character);
                Destroy(gameObject);
            }
        }
    }
}
