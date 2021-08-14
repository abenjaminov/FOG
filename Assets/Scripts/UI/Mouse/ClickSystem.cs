using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Mouse
{
    public class ClickSystem : MonoBehaviour
    {
        private float _timeBetweenClicks;
        private int _numberOfClicks;
        [SerializeField] private float _doubleClickTime;
        private void Update()
        {
            if (_numberOfClicks == 1)
            {
                _timeBetweenClicks += Time.deltaTime;

                if (_timeBetweenClicks > _doubleClickTime)
                {
                    _numberOfClicks = 0;
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _numberOfClicks++;
                List<RaycastHit2D> hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero).ToList();
                hit.AddRange(Physics2D.RaycastAll(Input.mousePosition, Vector2.zero));

                if (_timeBetweenClicks < _doubleClickTime && _numberOfClicks == 2)
                {
                    var clickableObjects = hit.Select(x => x.collider.GetComponent<IDoubleClickHandler>()).Where(x => x != null).ToList();
                    
                    if (clickableObjects.Any())
                    {
                        foreach (var clickableObject in clickableObjects)
                        {
                            clickableObject.HandleDoubleClick();
                        }
                    }

                    _numberOfClicks = 0;
                }
                else if(_numberOfClicks == 1)
                {
                    var clickableObjects = 
                            hit.Select(x => x.collider.GetComponent<IClickHandler>()).Where(x => x != null).ToList();
                    
                    if (clickableObjects.Any())
                    {
                        foreach (var clickableObject in clickableObjects)
                        {
                            clickableObject.HandleClick();
                        }
                    }
                }

                _timeBetweenClicks = 0;
            }
        }
    }
}