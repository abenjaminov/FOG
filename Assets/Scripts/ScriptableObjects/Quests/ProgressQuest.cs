using System;
using UnityEngine;

namespace ScriptableObjects.Quests
{
    public abstract class ProgressQuest : Quest
    {
        [HideInInspector] public float MaxValue;
        [HideInInspector] public  float CurrentValue;

        public Action ProgressMadeEvent;

        protected void ProgressMade(int currentValue)
        {
            CurrentValue = currentValue;
            ProgressMadeEvent?.Invoke();
        }
    }
}