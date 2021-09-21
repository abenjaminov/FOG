using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Quests;
using UI.Elements.Quests;
using UnityEngine;

namespace UI.Screens
{
    public class QuestTrackerPanel : MonoBehaviour
    {
        [SerializeField] private QuestsChannel _questsChannel;
        [SerializeField] private QuestsList _questsList;
        private List<IQuestInfoItem> _questInfos = new List<IQuestInfoItem>();

        [SerializeField] private QuestProgressInfo _progressQuestInfoPrefab;
        [SerializeField] private QuestInfoItem _noProgressQuestInfoPrefab;
        [SerializeField] private float _topOffset;
        
        private float _totalHeightTaken;
        private const float Margin = 3;
        
        private void Awake()
        {
            _questsChannel.QuestActivatedEvent += QuestActivatedEvent;
            _questsChannel.QuestCompleteEvent += QuestCompleteEvent;
            _totalHeightTaken = -_topOffset - Margin;

            CheckQuestsList();
        }

        private void CheckQuestsList()
        {
            var runningQuests = _questsList.GetAllRunningQuests().Take(3);

            foreach (var runningQuest in runningQuests)
            {
                QuestActivatedEvent(runningQuest);
            }
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
            this.SetActive(true);
            
            StartCoroutine(QuestActivatedSequence(activatedQuest));
        }

        IEnumerator QuestActivatedSequence(Quest activatedQuest)
        {
            if (activatedQuest is ProgressQuest progressQuest)
            {
                var infoItem = Instantiate(_progressQuestInfoPrefab, this.transform);

                yield return new WaitForEndOfFrame();
                
                infoItem.SetQuest(progressQuest);
                
                AddInfoItem(infoItem);
            }
            else
            {
                var infoItem = Instantiate(_noProgressQuestInfoPrefab, this.transform);
                
                yield return new WaitForEndOfFrame();
                
                infoItem.SetQuest(activatedQuest);
                
                AddInfoItem(infoItem);
            }
            
            yield break;
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
            infoItem.SetLocalPosition(new Vector3(-size.x, _totalHeightTaken, 0));
            _totalHeightTaken -= size.y;
        }

        private void RearangeInfoItems()
        {
            _totalHeightTaken = -_topOffset - Margin;

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