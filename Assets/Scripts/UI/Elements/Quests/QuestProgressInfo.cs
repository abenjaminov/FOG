using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Quests;
using TMPro;
using UI.Behaviours;
using UI.Screens;
using UnityEngine;

namespace UI.Elements.Quests
{
    public class QuestProgressInfo : ProgressBar, IQuestInfoItem
    {
        [SerializeField] private QuestsChannel _questsChannel;
        private RectTransform _rectTransform;

        private QuestInfoLogic<ProgressQuest> _questInfoLogic;
        [SerializeField] private TextMeshProUGUI _questName;

        [SerializeField] private SlideFromHorizontalEdge _slider;
        [SerializeField] private TextPhraseMapper _phraseMapper;
        
        private void Awake()
        {
            _questsChannel.QuestCompleteEvent += QuestCompleteEvent;

            _questInfoLogic = new QuestInfoLogic<ProgressQuest>(this.gameObject, _questName, _slider, _phraseMapper);
        }

        private void QuestCompleteEvent(Quest arg0)
        {
            _questInfoLogic.Quest.ProgressMadeEvent -= ProgressMadeEvent;
        }

        public void SetQuest(Quest quest)
        {
            var progressQuest = quest as ProgressQuest;
            _questInfoLogic.SetQuest(progressQuest);
            _questInfoLogic.Quest.ProgressMadeEvent += ProgressMadeEvent;
            MaxValue = _questInfoLogic.Quest.MaxValue;
            ProgressMadeEvent();
        }

        public string GetQuestId()
        {
            return _questInfoLogic.GetQuestId();
        }

        public Vector2 GetSize()
        {
            return _questInfoLogic.GetSize();
        }

        public void SetLocalPosition(Vector3 position)
        {
            _questInfoLogic.SetLocalPosition(position);
        }

        private void ProgressMadeEvent()
        {
            CurrentValue = _questInfoLogic.Quest.CurrentValue;
            UpdateUI();
        }

        private void OnDestroy()
        {
            _questInfoLogic.Quest.ProgressMadeEvent -= ProgressMadeEvent;
            _questsChannel.QuestCompleteEvent -= QuestCompleteEvent;
        }
    }
}