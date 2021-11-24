using System.Text;
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
            _playerTraits.Name = playerTraitsPersistence.Name;
            _playerTraits.IsNameSet = playerTraitsPersistence.IsNameSet;
            _playerTraits.SetResistancePointsSilent(playerTraitsPersistence.ResistancePointsGained);
            _playerTraits.SetMonsterResistanceSilent(playerTraitsPersistence.MonsterStateResistance);
        }

        public override void OnModuleClosing(IPersistenceModuleAccessor accessor)
        {
            var playerTraitsPersistence = GetPlayerPersistence();

            accessor.PersistData("PlayerTraits", playerTraitsPersistence);
        }

        private PlayerTraitsPersistence GetPlayerPersistence()
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
                MonsterStateResistance = _playerTraits.MonsterStateResistance,
                Name = _playerTraits.Name,
                IsNameSet =  _playerTraits.IsNameSet
            };
            return playerTraitsPersistence;
        }

        public override void PrintPersistantData()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("##### PLAYER TRAITS PERSISTENCE #####");
            var playerTraitsPersistence = GetPlayerPersistence();

            strBuilder.AppendLine($"Constitution {playerTraitsPersistence.Constitution}");
            strBuilder.AppendLine($"Dexterity {playerTraitsPersistence.Dexterity}");
            strBuilder.AppendLine($"Intelligence {playerTraitsPersistence.Intelligence}");
            strBuilder.AppendLine($"Strength {playerTraitsPersistence.Strength}");
            strBuilder.AppendLine($"CurrentHealth {playerTraitsPersistence.CurrentHealth}");
            strBuilder.AppendLine($"PointsLeft {playerTraitsPersistence.PointsLeft}");
            strBuilder.AppendLine($"Level {playerTraitsPersistence.Level}");
            strBuilder.AppendLine($"ResistancePointsGained {playerTraitsPersistence.ResistancePointsGained}");
            strBuilder.AppendLine($"MonsterStateResistance {playerTraitsPersistence.MonsterStateResistance}");
            strBuilder.AppendLine($"Name {playerTraitsPersistence.Name}");
            
            Debug.Log(strBuilder.ToString());
        }
    }
}