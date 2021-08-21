using Abilities;
using Entity.Enemies;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Entity.Player.Bow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private CombatChannel _combatChannel;
        [HideInInspector] public Player ParentCharacter;
        private Ability _parentAbility;
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
            if (numberOfEnemiesLeft <= 0) return;
            if (!other.TryGetComponent(typeof(Enemy), out var character)) return;
            
            _combatChannel.OnEnemyHit(ParentCharacter, (Enemy)character, _parentAbility);
            numberOfEnemiesLeft--;

            if (numberOfEnemiesLeft <= 0)
            {
                Destroy(gameObject);    
            }
        }
    }
}
