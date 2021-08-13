using System;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UI.Screens;
using UnityEngine;

namespace UI.Elements
{
    public class QuestProgressInfo : ProgressBar, IQuestInfoItem
    {
        private ProgressQuest _quest;
        [SerializeField] private QuestsChannel _questsChannel;

        private void Awake()
        {
            _questsChannel.QuestCompleteEvent += QuestCompleteEvent;
        }

        private void QuestCompleteEvent(Quest arg0)
        {
            _quest.ProgressMadeEvent -= ProgressMadeEvent;
        }

        public void SetQuest(Quest quest)
        {
            if (!(quest is ProgressQuest)) return;
            
            _quest = (ProgressQuest) quest;
            _quest.ProgressMadeEvent += ProgressMadeEvent;
            MaxValue = _quest.MaxValue;
            ProgressMadeEvent();
        }

        public string GetQuestId()
        {
            return _quest != null ? _quest.Id : "";
        }

        private void ProgressMadeEvent()
        {
            CurrentValue = _quest.CurrentValue;
            UpdateUI();
        }

        private void OnDestroy()
        {
            _quest.ProgressMadeEvent -= ProgressMadeEvent;
            _questsChannel.QuestCompleteEvent -= QuestCompleteEvent;
        }
    }
}