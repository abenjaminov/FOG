using System;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using TMPro;
using UI.Elements.Quests;
using UI.Screens;
using UnityEngine;

namespace UI.Elements
{
    public class QuestInfoItem : MonoBehaviour, IQuestInfoItem
    {
        private QuestsChannel _questsChannel;
        private RectTransform _rectTransform;
        private QuestInfoLogic<Quest> _questInfoLogic;

        [SerializeField] private TextMeshProUGUI _questName;

        private void Awake()
        {
            _questInfoLogic = new QuestInfoLogic<Quest>(this.gameObject, _questName);
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