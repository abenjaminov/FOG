using System;

namespace State
{
    public class Transition
    {
        public IState To;
        private Func<bool> _predicate;
        public string TransitionName;
        private Action _transitionLogic;

        public Transition(IState to, Func<bool> predicate,Action transitionLogic = null, string transitionName = "")
        {
            To = to;
            _predicate = predicate;
            _transitionLogic = transitionLogic;
            TransitionName = transitionName;
        }

        public bool CanMakeTransition()
        {
            return _predicate();
        }

        public void TransitionLogic()
        {
            _transitionLogic?.Invoke();
        }
    }
}