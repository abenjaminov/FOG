using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using ScriptableObjects.Chat;
using ScriptableObjects.Quests;
using UnityEngine;

namespace ScriptableObjects.GameConfiguration
{
    [CreateAssetMenu(fileName = "Quests List", menuName = "Game Configuration/Quests")]
    public class QuestsList : ScriptableObject
    {
        [SerializeField] private List<Quest> AllQuests;
        [HideInInspector] public Dictionary<string, Quest> QuestsMap;

        private void OnEnable()
        {
#if UNITY_EDITOR
            var questAssets = AssetsHelper.GetAllAssets<Quest>();

            AllQuests ??= new List<Quest>();
            AllQuests.Clear();

            UnityEditor.AssetDatabase.Refresh();
            
            foreach (var quest in questAssets)
            {
                if (string.IsNullOrEmpty(quest.Id))
                {
                    quest.Id = Guid.NewGuid().ToString();
                    UnityEditor.EditorUtility.SetDirty(quest);
                }
                AllQuests.Add(quest);
            }
            
            UnityEditor.AssetDatabase.SaveAssets();
#endif
            QuestsMap = AllQuests.ToDictionary(x => x.Id, x => x);
        }

        public List<Quest> GetAllRunningQuests()
        {
            var runningQuests = AllQuests
                .Where(x => x.State == QuestState.Active || x.State == QuestState.PendingComplete).ToList();

            return runningQuests;
        }

        public void ResetQuests()
        {
            foreach (var quest in AllQuests)
            {
                quest.ResetQuest();
            }
        }
    }
}