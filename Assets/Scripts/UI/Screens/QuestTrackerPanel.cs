using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class QuestTrackerPanel : MonoBehaviour
    {
        [SerializeField] private QuestsChannel _questsChannel;
        private List<IQuestInfoItem> _questInfos = new List<IQuestInfoItem>();

        [SerializeField] private QuestProgressInfo _progressQuestInfoPrefab;
        [SerializeField] private QuestInfoItem _noProgressQuestInfoPrefab;
        
        private void Awake()
        {
            _questsChannel.QuestActivatedEvent += QuestActivatedEvent;
            _questsChannel.QuestCompleteEvent += QuestCompleteEvent;
        }

        private void QuestCompleteEvent(Quest completedQuest)
        {
            var questInfo = _questInfos.FirstOrDefault(x => x.GetQuestId() == completedQuest.Id);

            if (questInfo != null)
            {
                Destroy((MonoBehaviour) questInfo);
                _questInfos.Remove(questInfo);
            }
        }

        private void QuestActivatedEvent(Quest activatedQuest)
        {
            if (activatedQuest is ProgressQuest progressQuest)
            {
                var infoItem = Instantiate(_progressQuestInfoPrefab, this.transform);
                infoItem.transform.localPosition = new Vector3(0, -10, 0);
                
                infoItem.SetQuest(progressQuest);
                
                _questInfos.Add(infoItem);
            }
        }

        private void OnDestroy()
        {
            _questsChannel.QuestActivatedEvent -= QuestActivatedEvent;
            _questsChannel.QuestCompleteEvent -= QuestCompleteEvent;
        }
    }
}