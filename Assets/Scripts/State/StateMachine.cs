using System;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
    public class StateMachine
    {
        private Dictionary<Type, List<Transition>> _stateTransitions;
        private List<Transition> _anyTransition;
        private IState _currentState;
        private List<Transition> _currentTransitions;
        private static List<Transition> NoStateTransitions;
        private bool _showDebug;
        
        static StateMachine()
        {
            NoStateTransitions = new List<Transition>();
            
        }
        
        public StateMachine(bool showDebug = false)
        {
            _stateTransitions = new Dictionary<Type, List<Transition>>();
            _anyTransition = new List<Transition>();
            _showDebug = showDebug;
        }

        public void Tick()
        {
            var transition = GetViableTransition();

            if (transition != null)
            {
                if(_showDebug && transition.TransitionName != "")
                    Debug.Log(transition.TransitionName);
                
                transition.TransitionLogic();
                SetState(transition.To);
            }
            
            _currentState?.Tick();
        }

        public void AddTransition(IState to, Func<bool> predicate,IState from = null,
            Action transitionLogic = null,  string transitionName = "")
        {
            var transition = new Transition(to, predicate,transitionLogic, transitionName);
            
            if (from == null)
            {
                _anyTransition.Add(transition);    
            }
            else
            {
                if (!_stateTransitions.TryGetValue(from.GetType(), out var transitions))
                {
                    transitions = new List<Transition>();
                    _stateTransitions.Add(from.GetType(), transitions);
                }
                
                transitions.Add(transition);
            }
        }

        public void SetState(IState state)
        {
            if (_currentState == state)
                return;
            
            _currentState?.OnExit();
            _currentState = state;
            if(_showDebug)
                Debug.Log(_currentState.GetType());

            if (!_stateTransitions.TryGetValue(state.GetType(), out _currentTransitions))
            {
                _currentTransitions = NoStateTransitions;
            }
            
            _currentState.OnEnter();
        }
        
        Transition GetViableTransition()
        {
            foreach (var transition in _anyTransition)
            {
                if (transition.CanMakeTransition())
                    return transition;
            }

            foreach (var transition in _currentTransitions)
            {
                if (transition.CanMakeTransition())
                     return transition;
            }

            return null;
        }
    }
}