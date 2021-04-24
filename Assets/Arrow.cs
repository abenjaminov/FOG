using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed; 
    [HideInInspector] public Vector2 WorldMovementDirection;

    private void Update()
    {
        transform.Translate(WorldMovementDirection * (_speed * Time.deltaTime), Space.World);
    }
}
