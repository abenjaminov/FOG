using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Quests List", menuName = "Game Configuration/Quests")]
    public class QuestsList : ScriptableObject
    {
        [SerializeField] private List<Quest> Quests;

        public List<Quest> GetAllRunningQuests()
        {
            return Quests.Where(x => x.State != QuestState.PendingActive).ToList();
        }
    }
}