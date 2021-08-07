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
        public string Name;
        public ResistancePointsQuestReward RPReward;
        
        public List<Quest> NextQuests;
        public QuestState State;
        public int RequiredLevel;
        public List<QuestReward> QuestRewards;
        [SerializeField] protected QuestsChannel _questsChannel;

        protected virtual void OnEnable()
        {
            _questsChannel.QuestActiveEvent += QuestActiveEvent;
            _questsChannel.QuestCompletedEvent += QuestCompletedEvent;

            if (State == QuestState.Active)
            {
                QuestActive();
            }
        }

        private void OnDisable()
        {
            _questsChannel.QuestActiveEvent -= QuestActiveEvent;
            _questsChannel.QuestCompletedEvent -= QuestCompletedEvent;
        }

        public void GiveRewards()
        {
            if(RPReward.ApplyReward)
                RPReward.Give();
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
            Debug.Log(Name + " Assigned");
            QuestActive();
        }
        
        protected abstract void QuestActive();

        protected void Complete()
        {
            _questsChannel.OnQuestCompleted(this);

            Debug.Log(Name + " Completed");
            
            if (NextQuests.Count <= 0) return;
            
            foreach (var quest in NextQuests)
            {
                _questsChannel.AssignQuest(quest);    
            }
        }
    }

    public enum QuestState
    {
        Pending,
        Active,
        Completed
    }
}