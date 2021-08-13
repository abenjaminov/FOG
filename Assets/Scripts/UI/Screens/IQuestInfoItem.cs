using ScriptableObjects.Quests;

namespace UI.Screens
{
    public interface IQuestInfoItem
    {
        public void SetQuest(Quest quest);
        public string GetQuestId();
    }
}