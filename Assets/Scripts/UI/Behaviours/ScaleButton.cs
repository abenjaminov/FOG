using Assets.HeroEditor.Common.CommonScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Behaviours
{
    public class ScaleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _mouseOverScale;
        [SerializeField] private float _mouseDownScale;
        [SerializeField] private float _mouseLeaveScale;
        [SerializeField] private Shadow _shadow;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = new Vector3(_mouseOverScale, _mouseOverScale, 1);
            _shadow.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = new Vector3(_mouseLeaveScale, _mouseLeaveScale, 1);
            _shadow.enabled = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = new Vector3(_mouseDownScale, _mouseDownScale, 1);
            _shadow.enabled = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = new Vector3(_mouseOverScale, _mouseOverScale, 1);
            _shadow.enabled = true;
        }
    }
}