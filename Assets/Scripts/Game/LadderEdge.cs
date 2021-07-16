using System;
using UnityEngine;

namespace Game
{
    public class LadderEdge : MonoBehaviour
    {
        public EdgeType Type;
    }

    [Serializable]
    public enum EdgeType
    {
        Upper,
        Lower
    }
}