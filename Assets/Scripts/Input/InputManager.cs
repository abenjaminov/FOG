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
            foreach (var keyMapped in _inputChannel.MappedKeyDownActions.Keys)
            {
                if (Input.GetKeyDown(keyMapped))
                {
                    _inputChannel.OnKeyDown(keyMapped);
                }
                
                if (Input.GetKeyUp(keyMapped))
                {
                    _inputChannel.OnKeyUp(keyMapped);
                }
            }
        }
    }
}