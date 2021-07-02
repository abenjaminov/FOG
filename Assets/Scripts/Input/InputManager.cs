using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using UnityEngine;

namespace Platformer
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputChannel _inputChannel;
        
        private void Update()
        {
            var keysDown = _inputChannel.MappedKeyDownActions.Keys.ToList();
            
            foreach (var keyMapped in keysDown)
            {
                if (Input.GetKeyDown(keyMapped))
                {
                    _inputChannel.OnKeyDown(keyMapped);
                }
            }    
            
            var keysUp = _inputChannel.MappedKeyUpActions.Keys.ToList();
            
            foreach (var keyMapped in keysUp)
            {
                if (Input.GetKeyUp(keyMapped))
                {
                    _inputChannel.OnKeyUp(keyMapped);
                }
            }
        }
    }
}