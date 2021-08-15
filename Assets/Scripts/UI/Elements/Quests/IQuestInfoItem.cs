using ScriptableObjects.Quests;
using UnityEngine;

namespace UI.Elements.Quests
{
    public interface IQuestInfoItem
    {
        void SetQuest(Quest quest);
        string GetQuestId();
        Vector2 GetSize();
        void SetLocalPosition(Vector3 position);
    }
}