using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Channels;
using UI.Behaviours;
using UI.Elements;
using UnityEngine;

namespace UI.Screens
{
    public class HotKeyPanel : MonoBehaviour
    {
        private List<HotKeySpot> _hotKeySpots = new List<HotKeySpot>();
        [SerializeField] private DragChannel _dragChannel;
    }
}