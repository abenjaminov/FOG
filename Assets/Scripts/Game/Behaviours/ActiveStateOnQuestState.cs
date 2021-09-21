using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UnityEngine;

namespace Game.Behaviours
{
    public class ActiveStateOnQuestState : MonoBehaviour
    {
        [SerializeField] private List<QuestStateMap> _quests;
        [SerializeField] private bool _activeState;

        [SerializeField] private QuestsChannel _questsChannel;

        private void Awake()
        {
            _questsChannel.QuestStateChangedEvent += QuestStateChangedEvent;

            UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            if (_quests.Any(q => q.State == q.Quest.State))
            {
                this.SetActive(_activeState);
            }
            else
            {
                this.SetActive(!_activeState);
            }
        }

        private void QuestStateChangedEvent(Quest quest)
        {
            UpdateActiveState();
        }

        private void OnDestroy()
        {
            _questsChannel.QuestStateChangedEvent -= QuestStateChangedEvent;
        }
    }

    [Serializable]
    public class QuestStateMap
    {
        public Quest Quest;
        public QuestState State;
    }
}