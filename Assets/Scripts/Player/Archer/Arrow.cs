using System;
using System.Collections;
using System.Collections.Generic;
using Platformer;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public Character ParentCharacter;
    [SerializeField] private float _speed; 
    [HideInInspector] public Vector2 WorldMovementDirection;
    [HideInInspector] public Traits _traits;
    public float Range;
    [SerializeField] private CombatChannel _combatChannel;

    private void Update()
    {
        transform.Translate(WorldMovementDirection * (_speed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(Character), out var character))
        {
            _combatChannel.OnCharacterHit(ParentCharacter, (Character)character);
        }
        
        Destroy(gameObject);
    }
}
