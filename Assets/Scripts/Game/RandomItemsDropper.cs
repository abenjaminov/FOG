using System.Collections.Generic;
using Helpers;
using ScriptableObjects.Quests;
using ScriptableObjects.Traits;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class RandomItemsDropper : Dropper
    {
        [SerializeField] private List<DropItem> _dropItems;
        [SerializeField] private Traits _dropperTraits;
        [SerializeField] private float multiDropOffset;

        public void Drop()
        {
            var numberOfInstantiated = 0;

            foreach (var dropItem in _dropItems)
            {
                if (dropItem.AssosiatedQuest != null &&
                    dropItem.AssosiatedQuest.State != QuestState.Active) continue;
                
                var randomNumber = Random.Range(0f, 1f);

                if (randomNumber > dropItem.Percentage) continue;
                
                var offsetValue = (numberOfInstantiated + 1) / 2;
                float offset = numberOfInstantiated % 2 == 0 ? offsetValue : -offsetValue;
                var amount = DropsHelper.GetDropAmount(dropItem.ItemMetaData, _dropperTraits);
                    
                InstantiateDrop(dropItem.ItemMetaData, amount, offset * multiDropOffset);

                numberOfInstantiated++;
            }
        }
    }
}