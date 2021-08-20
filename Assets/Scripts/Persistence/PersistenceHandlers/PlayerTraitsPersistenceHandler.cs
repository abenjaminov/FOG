using Persistence.Accessors;
using Persistence.PersistenceObjects;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class PlayerTraitsPersistenceHandler : PersistentMonoBehaviour
    {
        [SerializeField] private PlayerTraits _playerTraits;

        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            var playerTraitsPersistence = accessor.GetValue<PlayerTraitsPersistence>("PlayerTraits");

            if (playerTraitsPersistence == null) return;
            
            _playerTraits.Constitution = playerTraitsPersistence.Constitution;
            _playerTraits.Dexterity = playerTraitsPersistence.Dexterity;
            _playerTraits.Intelligence = playerTraitsPersistence.Intelligence;
            _playerTraits.Strength = playerTraitsPersistence.Strength;
            _playerTraits.CurrentHealth = playerTraitsPersistence.CurrentHealth;
            _playerTraits.PointsLeft = playerTraitsPersistence.PointsLeft;
            _playerTraits.Level = playerTraitsPersistence.Level;
            _playerTraits.SetResistancePointsSilent(playerTraitsPersistence.ResistancePointsGained);
            _playerTraits.MonsterStateResistance = playerTraitsPersistence.MonsterStateResistance;
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var playerTraitsPersistence = new PlayerTraitsPersistence()
            {
                Constitution = _playerTraits.Constitution,
                Dexterity = _playerTraits.Dexterity,
                Intelligence = _playerTraits.Intelligence,
                Strength = _playerTraits.Strength,
                CurrentHealth = _playerTraits.CurrentHealth,
                PointsLeft = _playerTraits.PointsLeft,
                Level = _playerTraits.Level,
                ResistancePointsGained = _playerTraits.ResistancePointsGained,
                MonsterStateResistance = _playerTraits.MonsterStateResistance
            };

            accessor.PersistData("PlayerTraits", playerTraitsPersistence);
        }
    }
}