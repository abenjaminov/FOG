using ScriptableObjects.Quests;
using TMPro;
using UI.Screens;
using UnityEngine;

namespace UI.Elements.Quests
{
    public class QuestInfoLogic<T> where T : Quest
    {
        public T Quest;
        private RectTransform _rectTransform;
        private GameObject _questInfoItem;
        
        [SerializeField] private TextMeshProUGUI _questName;

        public QuestInfoLogic(GameObject questInfoItem, TextMeshProUGUI questName)
        {
            _questInfoItem = questInfoItem;
            _rectTransform = questInfoItem.GetComponent<RectTransform>();
            _questName = questName;
        }

        public void SetQuest(T quest)
        {
            Quest = quest;
            _questName.text = quest.Name;
        }

        public string GetQuestId()
        {
            return Quest != null ? Quest.Id : "";
        }

        public Vector2 GetSize()
        {
            return _rectTransform.sizeDelta;
        }

        public void SetLocalPosition(Vector3 position)
        {
            _questInfoItem.transform.localPosition = position;
        }
    }
}