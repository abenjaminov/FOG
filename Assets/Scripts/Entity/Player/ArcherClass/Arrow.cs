using Abilities;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Entity.Player.ArcherClass
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private CombatChannel _combatChannel;
        [HideInInspector] public CharacterWrapper ParentCharacter;
        [HideInInspector] private Ability _parentAbility;
        [SerializeField] private float _speed; 
        [HideInInspector] public Vector2 WorldMovementDirection;
        
        public float Range;
        private Vector2 _spawnPosition;
        private int numberOfEnemiesLeft;

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

        public void SetParentAbility(Attack parentAbility)
        {
            _parentAbility = parentAbility;
            numberOfEnemiesLeft = parentAbility.NumberOfEnemies;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(typeof(WorldEntity), out var character))
            {
                _combatChannel.OnEntityHit(ParentCharacter, (WorldEntity)character, _parentAbility);
                numberOfEnemiesLeft--;

                if (numberOfEnemiesLeft <= 0)
                {
                    Destroy(gameObject);    
                }
            }
        }
    }
}
