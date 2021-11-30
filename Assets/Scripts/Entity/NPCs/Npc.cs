using System;
using ScriptableObjects.Channels;
using TMPro;
using UI.Mouse;
using UnityEngine;

namespace Entity.NPCs
{
    public abstract class Npc : MonoBehaviour, IDoubleClickHandler
    {
        public string NpcId;
        [SerializeField] protected NpcChannel _npcChannel;
        
        [Header("Visuals")] 
        [SerializeField] protected GameObject _npcVisuals;
        [SerializeField] protected  bool faceLeft;
        [SerializeField] protected  string _name;
        [SerializeField] protected  TextMeshProUGUI _nameText;

        protected virtual void Awake()
        {
            var visuals = Instantiate(_npcVisuals, Vector3.zero, Quaternion.Euler(0, faceLeft ? 180 : 0, 0), this.transform);
            visuals.transform.localPosition = Vector3.zero;
            _nameText.SetText(_name);
        }

        public abstract void HandleDoubleClick();
    }
}