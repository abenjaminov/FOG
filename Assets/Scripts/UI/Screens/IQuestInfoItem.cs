using ScriptableObjects.Quests;
using UnityEngine;

namespace UI.Screens
{
    public interface IQuestInfoItem
    {
        void SetQuest(Quest quest);
        string GetQuestId();
        Vector2 GetSize();
        void SetLocalPosition(Vector3 position);
    }
}