using System;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private RectTransform _coverTransform;
        [SerializeField] private TextMeshProUGUI _timeText;
        [HideInInspector] public float SkillTime;
        private float _activeSkillTime;
        private float _originHeight;
        private float _originWidth;

        private void Awake()
        {
            _originHeight = _coverTransform.sizeDelta.y;
            _originWidth = _coverTransform.sizeDelta.x;
        }

        private void Update()
        {
            _activeSkillTime += Time.deltaTime;
            _coverTransform.sizeDelta = new Vector2(_originWidth, Mathf.Lerp(_originHeight, 0, _activeSkillTime / SkillTime));
            
            _timeText.SetText(GetTimeText());
        }

        private string GetTimeText()
        {
            var timeLeft = SkillTime - _activeSkillTime;
            var textTimeNumber = 0;
            if (timeLeft >= 60)
            {
                textTimeNumber = (int)Mathf.Floor(timeLeft / 60);
            }
            else
            {
                textTimeNumber = (int) Mathf.Floor(timeLeft);
            }

            return textTimeNumber.ToString();
        }
    }
}