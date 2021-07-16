using System;
using Game;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerClimb: MonoBehaviour
    {
        [HideInInspector] public bool CanClimb;
        [HideInInspector] public bool IsOnEdge;
        [HideInInspector] public Ladder CurrentLadder;
        [HideInInspector] public LadderEdge CurrentEdge;
        
        [HideInInspector] public bool CanClimbUp;
        [HideInInspector] public bool CanClimbDown;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Ladder>(out var ladder))
            {
                CanClimb = true;
                CurrentLadder = ladder;

                CanClimbUp = CurrentLadder.Center.position.y > transform.position.y;
                CanClimbDown = CurrentLadder.Center.position.y < transform.position.y; 
            }
            
            if (other.TryGetComponent<LadderEdge>(out var ladderEdge))
            {
                CurrentEdge = ladderEdge;
                IsOnEdge = true;
                
                CanClimbUp = CurrentLadder.Center.position.y > ladderEdge.transform.position.y;
                CanClimbDown = CurrentLadder.Center.position.y < ladderEdge.transform.position.y; 
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Ladder>(out var ladder))
            {
                CanClimb = false;
                CurrentLadder = null;
                CanClimbDown = false;
                CanClimbUp = false;
            }
            
            if (other.TryGetComponent<LadderEdge>(out var ladderEdge))
            {
                CurrentEdge = null;
                IsOnEdge = false;
            }
        }

        private void UpdateClimbDirectionOptions()
        {
            
        }
    }
}