using System;

namespace State
{
    public class Transition
    {
        public IState To;
        private Func<bool> _predicate;

        public Transition(IState to, Func<bool> predicate)
        {
            To = to;
            _predicate = predicate;
        }

        public bool CanMakeTransition()
        {
            return _predicate();
        }
    }
}