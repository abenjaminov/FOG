using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Mouse
{
    public class ClickSystem : MonoBehaviour
    {
        private float _timeBetweenClicks = 0;
        private int _numberOfClicks;
        [SerializeField] private float _doubleClickTime;
        private float previousClickTime;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _numberOfClicks++;
                previousClickTime = Time.time;

                if ((Time.time - previousClickTime) < _doubleClickTime && _numberOfClicks == 2)
                {
                    HandleDoubleClick();
                    _numberOfClicks = 0;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                HandleRightClick();
            }
            
            if (_numberOfClicks == 1 && (Time.time - previousClickTime) > _doubleClickTime)
            {
                HandleSingleClick();
                _numberOfClicks = 0;
            }
        }

        private static void HandleSingleClick()
        {
            var clickableObjects = GetClickableObjects<ISingleClickHandler>();

            if (!clickableObjects.Any()) return;
            
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.HandleClick();
            }
        }

        private static List<RaycastHit2D> GetRaycastHit()
        {
            var hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero).ToList();
            hit.AddRange(Physics2D.RaycastAll(Input.mousePosition, Vector2.zero));
            return hit;
        }

        private static List<T> GetClickableObjects<T>()
        {
            var hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero).ToList();
            hit.AddRange(Physics2D.RaycastAll(Input.mousePosition, Vector2.zero));
            
            var clickableObjects = hit.Select(x => x.collider.GetComponent<T>()).Where(x => x != null).ToList();

            return clickableObjects;
        }
        
        private void HandleDoubleClick()
        {
            var clickableObjects = GetClickableObjects<IDoubleClickHandler>();

            if (!clickableObjects.Any()) return;
            
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.HandleDoubleClick();
            }
        }

        private void HandleRightClick()
        {
            var clickableObjects = GetClickableObjects<IRightClickHandler>();
            
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.HandleRightClick();
            }
        }
    }
}