using System;
using System.Collections;
using System.Collections.Generic;
using Platformer;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed; 
    [HideInInspector] public Vector2 WorldMovementDirection;
    [HideInInspector] public Traits _traits;

    private void Update()
    {
        transform.Translate(WorldMovementDirection * (_speed * Time.deltaTime), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(Character), out var character))
        {
            ((Character)character).ReceiveDamage(1);
        }
        
        Destroy(gameObject);
    }
}
