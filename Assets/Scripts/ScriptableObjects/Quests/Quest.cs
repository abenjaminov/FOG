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
        public string Name;
        public ResistancePointsQuestReward RPReward;
        
        public List<Quest> NextQuests;
        public QuestState State;
        public int RequiredLevel;
        [SerializeField] protected QuestsChannel _questsChannel;
        [SerializeField] protected bool _completeOnSpot;

        protected virtual void OnEnable()
        {
            _questsChannel.QuestActivatedEvent += QuestActiveEvent;
            _questsChannel.QuestCompleteEvent += QuestCompletedEvent;
            
            if (State == QuestState.Active)
            {
                QuestActive();
            }
        }

        private void OnDisable()
        {
            _questsChannel.QuestActivatedEvent -= QuestActiveEvent;
            _questsChannel.QuestCompleteEvent -= QuestCompletedEvent;
        }

        public void ApplyRewards()
        {
            if(RPReward.ApplyReward)
                RPReward.Reward();
        }
        
        private void QuestCompletedEvent(Quest completedQuest)
        {
            if (completedQuest != this) return;
            
            State = QuestState.Completed;
            QuestCompleted();
        }

        protected abstract void QuestCompleted();
        
        private void QuestActiveEvent(Quest activeQuest)
        {
            if (activeQuest != this) return;
            
            State = QuestState.Active;
            QuestActive();
        }
        
        protected abstract void QuestActive();

        protected void Complete()
        {
            if (!_completeOnSpot && State == QuestState.Active)
            {
                State = QuestState.PendingComplete;
            }
            else if(_completeOnSpot || State == QuestState.PendingComplete)
            {
                _questsChannel.CompleteQuest(this);

                Debug.Log(Name + " Completed");
            
                this.ApplyRewards();
            
                if (NextQuests.Count <= 0) return;
            
                foreach (var quest in NextQuests)
                {
                    _questsChannel.AssignQuest(quest);    
                }
            }
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