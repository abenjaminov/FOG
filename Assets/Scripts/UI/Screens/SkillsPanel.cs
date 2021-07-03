using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using ScriptableObjects.Channels;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class SkillsPanel : MonoBehaviour
    {
        [SerializeField] private SkillIcon _skillIconPrefab;
        [SerializeField] private CombatChannel _combatChannel;

        private readonly List<SkillIcon> _visibleSkillIcons = new List<SkillIcon>();

        private void Awake()
        {
            _combatChannel.BuffAppliedEvent += BuffAppliedEvent;
            _combatChannel.BuffExpiredEvent += BuffExpiredEvent;
        }

        private void OnDestroy()
        {
            _combatChannel.BuffAppliedEvent -= BuffAppliedEvent;
            _combatChannel.BuffExpiredEvent -= BuffExpiredEvent;
        }

        private void BuffExpiredEvent(Buff buff)
        {
            var existingBuff = _visibleSkillIcons.FirstOrDefault(x => x.Buff == buff);

            if (existingBuff != null)
            {
                Destroy(existingBuff.gameObject);
            }

            _visibleSkillIcons.Remove(existingBuff);
        }

        private void BuffAppliedEvent(Buff buff)
        {
            var existingBuff = _visibleSkillIcons.FirstOrDefault(x => x.Buff == buff);
            
            if (existingBuff == null)
            {
                ShowNewBuff(buff, -50 * _visibleSkillIcons.Count);
            }

            ResetOffsets();
        }

        private void ResetOffsets()
        {
            for (int i = 0; i < _visibleSkillIcons.Count; i++)
            {
                _visibleSkillIcons[i].SetOffset(-50 * i);
            }
        }
        
        private void ShowNewBuff(Buff buff, float offset)
        {
            var skillIcon = Instantiate<SkillIcon>(_skillIconPrefab, Vector3.zero, Quaternion.identity, this.transform);

            skillIcon.SetBuff(buff);
            skillIcon.SetOffset(offset);
            _visibleSkillIcons.Add(skillIcon);
        }
    }
}
