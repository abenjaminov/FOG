using ScriptableObjects.Channels;
using ScriptableObjects.GameConfiguration;
using ScriptableObjects.Quests;
using TMPro;
using UI.Behaviours;
using UI.Screens;
using UnityEngine;

namespace UI.Elements.Quests
{
    public class QuestInfoItem : MonoBehaviour, IQuestInfoItem
    {
        private QuestsChannel _questsChannel;
        private RectTransform _rectTransform;
        private QuestInfoLogic<Quest> _questInfoLogic;

        [SerializeField] private TextMeshProUGUI _questName;
        [SerializeField] private SlideFromHorizontalEdge _slider;
        [SerializeField] private TextPhraseMapper _phraseMapper;
        
        private void Awake()
        {
            _questInfoLogic = new QuestInfoLogic<Quest>(this.gameObject, _questName, _slider, _phraseMapper);
        }

        public void SetQuest(Quest quest)
        {
            _questInfoLogic.SetQuest(quest);
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
    }
}