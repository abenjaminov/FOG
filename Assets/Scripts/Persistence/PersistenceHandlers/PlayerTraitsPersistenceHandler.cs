using System;
using Persistence.Accessors;
using ScriptableObjects.Traits;
using UnityEngine;

namespace Persistence.PersistenceHandlers
{
    public class PlayerTraitsPersistenceHandler : PersistantMonoBehaviour
    {
        private PlayerTraitsPersistence _playerTraitsPersistence;
        [SerializeField] private PlayerTraits _playerTraits;

        public override void OnModuleLoaded(IPersistenceModuleAccessor accessor)
        {
            _playerTraitsPersistence = accessor.GetValue<PlayerTraitsPersistence>("PlayerTraits");

            if (_playerTraitsPersistence == null) return;
            
            _playerTraits.Constitution = _playerTraitsPersistence.Constitution;
            _playerTraits.Dexterity = _playerTraitsPersistence.Dexterity;
            _playerTraits.Intelligence = _playerTraitsPersistence.Intelligence;
            _playerTraits.Strength = _playerTraitsPersistence.Strength;
            _playerTraits.CurrentHealth = _playerTraitsPersistence.CurrentHealth;
            _playerTraits.PointsLeft = _playerTraitsPersistence.PointsLeft;
            _playerTraits.Level = _playerTraitsPersistence.Level;
            _playerTraits.SetResistancePointsSilent(_playerTraitsPersistence.ResistancePointsGained);
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            _playerTraitsPersistence = new PlayerTraitsPersistence();
            _playerTraitsPersistence.Constitution = _playerTraits.Constitution;
            _playerTraitsPersistence.Dexterity = _playerTraits.Dexterity;
            _playerTraitsPersistence.Intelligence = _playerTraits.Intelligence;
            _playerTraitsPersistence.Strength = _playerTraits.Strength;
            _playerTraitsPersistence.CurrentHealth = _playerTraits.CurrentHealth;
            _playerTraitsPersistence.PointsLeft = _playerTraits.PointsLeft;
            _playerTraitsPersistence.Level = _playerTraits.Level;
            _playerTraitsPersistence.ResistancePointsGained = _playerTraits.ResistancePointsGained;
            _playerTraitsPersistence.MonsterStateResistance = _playerTraits.MonsterStateResistance;
            
            accessor.PersistData<PlayerTraitsPersistence>("PlayerTraits", _playerTraitsPersistence);
        }
    }

    [Serializable]
    public class PlayerTraitsPersistence
    {
        internal float CurrentHealth;
        internal int Strength;
        internal int Dexterity;
        internal int Intelligence;
        internal int Constitution;
        internal int PointsLeft;
        internal int Level;
        internal int ResistancePointsGained;
        internal float MonsterStateResistance;
    }
}