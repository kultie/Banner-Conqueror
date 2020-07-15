using System;
using System.Collections.Generic;
namespace Kultie.StateMachine
{
    public class StateMachine<T1, T2> : StateMachineBase<T2> where T1 : System.Enum where T2 : StateContextBase
    {
        private Dictionary<T1, IState<T2>> states;

        public IState<T2> currentState;
        T1 currentStateName;

        public StateMachine(Dictionary<T1, IState<T2>> states)
        {
            this.states = states;
        }

        public void Change(T1 name, T2 context)
        {
            if (!states.ContainsKey(name))
            {
                return;
            }

            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = states[name];
            currentStateName = name;
            currentState.Enter(context);
        }

        public void Change(object lastState)
        {
            throw new NotImplementedException();
        }

        public override void Update(float dt)
        {
            if (currentState != null)
            {
                currentState.Update(dt);
            }
        }

        public T1 CurrentState()
        {
            return currentStateName;
        }
    }

    public abstract class StateBase<T> : IState<T> where T : StateContextBase
    {
        protected T context;
        public void Update(float dt)
        {
            OnUpdate(dt);
        }
        public void Enter(T context)
        {
            this.context = context;
            OnEnter();
        }
        public void Exit()
        {
            OnExit();
        }
        protected abstract void OnEnter();
        protected abstract void OnExit();
        protected abstract void OnUpdate(float dt);
    }
}


