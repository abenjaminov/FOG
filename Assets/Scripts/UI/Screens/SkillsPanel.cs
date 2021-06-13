using System.Collections.Generic;
using Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class SkillsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _skillIconPrefab;

        private List<GameObject> _visibleSkillIcons = new List<GameObject>();

        public void SetBuffs(List<Buff> activeBuffs)
        {
            foreach (var skillIcon in _visibleSkillIcons)
            {
                Destroy(skillIcon.gameObject);
            }
            _visibleSkillIcons.Clear();

            for (int i = 0; i < activeBuffs.Count; i++)
            {
                var newSkillIcon = Instantiate(_skillIconPrefab, Vector3.zero, Quaternion.identity, this.transform);
                var image = newSkillIcon.GetComponent<Image>();
                image.rectTransform.localPosition = new Vector3(-50 * i, 0, 0);
                image.sprite = activeBuffs[i].BuffSprite;
                _visibleSkillIcons.Add(newSkillIcon);
            }
        }
    }
}
