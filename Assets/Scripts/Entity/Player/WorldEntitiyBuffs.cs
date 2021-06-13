using System.Collections.Generic;
using System.Linq;
using Abilities;
using UI.Screens;
using UnityEngine;

namespace Entity.Player
{
    public class WorldEntitiyBuffs : MonoBehaviour
    {
        private List<Buff> ActiveBuffs = new List<Buff>();
        [SerializeField] private SkillsPanel _skillsPanel;

        public void ApplyBuff(Buff buffToApply)
        {
            buffToApply.Use();

            var existingBuff = ActiveBuffs.FirstOrDefault(x => x == buffToApply);
            
            if (existingBuff == null)
            {
                ActiveBuffs.Add(buffToApply);    
            }
            
            buffToApply.TimeUntillBuffEnds = buffToApply.BuffTime;
            
            _skillsPanel.SetBuffs(ActiveBuffs);
        }

        private void Update()
        {
            if (ActiveBuffs.Count == 0) return;
            var currentBuffCount = ActiveBuffs.Count;
            
            for (int i = 0; i < ActiveBuffs.Count; i++)
            {
                ActiveBuffs[i].TimeUntillBuffEnds -= Time.deltaTime;
                
                if (ActiveBuffs[i].TimeUntillBuffEnds <= 0)
                {
                    ActiveBuffs[i].End();
                    ActiveBuffs.RemoveAt(i);
                    i--;
                }
            }
            
            if(currentBuffCount != ActiveBuffs.Count)
                _skillsPanel.SetBuffs(ActiveBuffs);
        }
    }
}