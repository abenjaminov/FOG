using System;
using System.Collections.Generic;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests.QuestRewards;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    public abstract class Quest : ScriptableObject
    {
        [Header("General Quest")] 
        public string Id;
        [SerializeField] private string _name;
        
        [Header("Rewards")]
        public ResistancePointsQuestReward RPReward;
        public InventoryItemQuestReward ItemReward;

        [Header("Configuration")]
        public Quest DependencyQuest;
        
        public QuestState State;
        public int RequiredLevel;
        public List<Quest> NextQuests;
        [SerializeField] protected bool _completeOnSpot;
        
        [Header("Integration")]
        [SerializeField] protected QuestsChannel _questsChannel;

        public virtual string GetName()
        {
            return _name;
        }
        
        protected virtual void OnEnable()
        {
            if (State == QuestState.Active || (_completeOnSpot && State == QuestState.PendingComplete))
            {
                Activate();
            }
        }

        private void ApplyRewards()
        {
            if(RPReward.ApplyReward) RPReward.Reward();
            if (ItemReward.ApplyReward) ItemReward.Reward();
                
        }

        public virtual void Complete()
        {
            if (!_completeOnSpot && (State == QuestState.Active || State == QuestState.PendingActive))
            {
                State = QuestState.PendingComplete;

                _questsChannel.OnQuestPendingComplete(this);
            }
            else if(_completeOnSpot || State == QuestState.PendingComplete)
            {
                State = QuestState.Completed;

                ApplyRewards();
                
                _questsChannel.OnQuestCompleted(this);
            
                if (NextQuests.Count <= 0) return;
            
                foreach (var quest in NextQuests)
                {
                    quest.Activate();   
                }
            }
        }

        public virtual void Activate()
        {
            State = QuestState.Active;
            _questsChannel.OnQuestAssigned(this);
        }

        public virtual void ResetQuest()
        {
            State = QuestState.PendingActive;
        }
    }

    public enum QuestState
    {
        PendingActive,
        Active,
        PendingComplete,
        Completed
    }
}