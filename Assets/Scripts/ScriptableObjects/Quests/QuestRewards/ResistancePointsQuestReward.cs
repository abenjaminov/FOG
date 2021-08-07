﻿using System;
using UnityEngine;

namespace ScriptableObjects.Quests.QuestRewards
{
    [Serializable]
    public class ResistancePointsQuestReward : QuestReward
    {
        [SerializeField] private PlayerTraits _playerTraits;
        [SerializeField] private int ResistancePointsReward;
        
        
        public override bool Give()
        {
            _playerTraits.ResistancePointsGained += ResistancePointsReward;
            return true;
        }
    }
}