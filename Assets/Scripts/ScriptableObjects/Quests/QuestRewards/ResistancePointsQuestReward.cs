using System;
using ScriptableObjects.Traits;
using UnityEngine;

namespace ScriptableObjects.Quests.QuestRewards
{
    [Serializable]
    public class ResistancePointsQuestReward : QuestReward
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private int ResistancePointsReward;
        
        
        public override bool Reward()
        {
            _playerTraits.ResistancePointsGained += ResistancePointsReward;
            return true;
        }
    }
}