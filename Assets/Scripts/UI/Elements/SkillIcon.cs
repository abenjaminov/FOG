using System;
using Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private Image _image;

        public Buff Buff;
        
        private void Awake()
        {
            _originHeight = _coverTransform.sizeDelta.y;
            _originWidth = _coverTransform.sizeDelta.x;
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            _activeSkillTime += Time.deltaTime;
            _coverTransform.sizeDelta = new Vector2(_originWidth, 
                                                  Mathf.Lerp(_originHeight, 0, 
                                                  (Buff.BuffTime - Buff.TimeUntillBuffEnds) / Buff.BuffTime));
            
            _timeText.SetText(GetTimeText());
        }

        public void SetBuff(Buff buff)
        {
            Buff = buff;
            _image.sprite = buff.BuffSprite;
            SkillTime = buff.BuffTime;
        }

        public void SetOffset(float offset)
        {
            _image.rectTransform.localPosition = new Vector3(offset, 0, 0);
        }

        private string GetTimeText()
        {
            var timeLeft = Buff.TimeUntillBuffEnds;
            var textTimeNumber = 0;
            if (timeLeft >= 60)
            {
                textTimeNumber = (int)Mathf.Ceil(timeLeft / 60);
            }
            else
            {
                textTimeNumber = (int) Mathf.Ceil(timeLeft);
            }

            return textTimeNumber.ToString();
        }
    }
}