using System.Collections.Generic;
using System.Linq;
using Abilities;
using ScriptableObjects.Channels;
using UI.Screens;
using UnityEngine;

namespace Entity.Player
{
    public class WorldEntitiyBuffs : MonoBehaviour
    {
        private List<Buff> ActiveBuffs = new List<Buff>();
        [SerializeField] private CombatChannel _combatChannel;

        public void ApplyBuff(Buff buffToApply)
        {
            buffToApply.Use();

            var existingBuff = ActiveBuffs.FirstOrDefault(x => x == buffToApply);
            
            if (existingBuff == null)
            {
                ActiveBuffs.Add(buffToApply);    
            }
            
            buffToApply.TimeUntillBuffEnds = buffToApply.BuffTime;
            
            _combatChannel.OnBuffApplied(buffToApply);
        }

        private void Update()
        {
            if (ActiveBuffs.Count == 0) return;

            for (int i = 0; i < ActiveBuffs.Count; i++)
            {
                ActiveBuffs[i].TimeUntillBuffEnds -= Time.deltaTime;
                
                if (ActiveBuffs[i].TimeUntillBuffEnds <= 0)
                {
                    ActiveBuffs[i].End();
                    _combatChannel.OnBuffExpired(ActiveBuffs[i]);
                    ActiveBuffs.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}