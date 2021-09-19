using System;
using Assets.HeroEditor.Common.CommonScripts;
using ScriptableObjects.Channels;
using ScriptableObjects.Quests;
using UnityEngine;

namespace Game.Behaviours
{
    public class ActiveStateOnQuestState : MonoBehaviour
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private bool _activeState;
        [SerializeField] private QuestState _questState;

        [SerializeField] private QuestsChannel _questsChannel;

        private void Awake()
        {
            _questsChannel.QuestStateChangedEvent += QuestStateChangedEvent;

            if (_quest.State == _questState)
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
            if (_quest.Id == quest.Id && quest.State == _questState)
            {
                this.SetActive(_activeState);
            }
        }
    }
}