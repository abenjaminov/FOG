using System;

namespace ScriptableObjects.Quests.QuestRewards
{
    [Serializable]
    public abstract class QuestReward
    {
        public bool ApplyReward;
        public abstract bool Give();
    }
}