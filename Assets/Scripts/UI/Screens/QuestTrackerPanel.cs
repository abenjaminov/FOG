using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UI.Elements;
using UI.Elements.Quests;
using UnityEngine;

namespace UI.Screens
{
    public class QuestTrackerPanel : MonoBehaviour
    {
        [SerializeField] private QuestsChannel _questsChannel;
        private List<IQuestInfoItem> _questInfos = new List<IQuestInfoItem>();

        [SerializeField] private QuestProgressInfo _progressQuestInfoPrefab;
        [SerializeField] private QuestInfoItem _noProgressQuestInfoPrefab;

        private float _totalHeightTaken = 0;
        private const float Margin = 3;
        
        private void Awake()
        {
            _questsChannel.QuestActivatedEvent += QuestActivatedEvent;
            _questsChannel.QuestCompleteEvent += QuestCompleteEvent;
            _totalHeightTaken = -Margin;
        }

        private void QuestCompleteEvent(Quest completedQuest)
        {
            var questInfo = _questInfos.FirstOrDefault(x => x.GetQuestId() == completedQuest.Id);

            if (questInfo != null)
            {
                Destroy(((MonoBehaviour) questInfo).gameObject);
                _questInfos.Remove(questInfo);
                
                RearangeInfoItems();
            }
        }

        private void QuestActivatedEvent(Quest activatedQuest)
        {
            if (activatedQuest is ProgressQuest progressQuest)
            {
                var infoItem = Instantiate(_progressQuestInfoPrefab, this.transform);

                infoItem.SetQuest(progressQuest);
                
                AddInfoItem(infoItem);
            }
            else
            {
                var infoItem = Instantiate(_noProgressQuestInfoPrefab, this.transform);
                infoItem.SetQuest(activatedQuest);
                
                AddInfoItem(infoItem);
            }
        }

        private void AddInfoItem(IQuestInfoItem infoItem)
        {
            SetInfoItemPosition(infoItem);

            _questInfos.Add(infoItem);
        }

        private void SetInfoItemPosition(IQuestInfoItem infoItem)
        {
            var size = infoItem.GetSize();
            _totalHeightTaken -= Margin;
            infoItem.SetLocalPosition(new Vector3(0, _totalHeightTaken, 0));
            _totalHeightTaken -= size.y;
        }

        private void RearangeInfoItems()
        {
            _totalHeightTaken = -Margin;

            foreach (var infoItem in _questInfos)
            {
                SetInfoItemPosition(infoItem);
            }
        }

        private void OnDestroy()
        {
            _questsChannel.QuestActivatedEvent -= QuestActivatedEvent;
            _questsChannel.QuestCompleteEvent -= QuestCompleteEvent;
        }
    }
}