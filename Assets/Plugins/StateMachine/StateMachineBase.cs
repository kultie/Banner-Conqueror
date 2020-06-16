using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kultie.StateMachine
{
    public class StateContextBase
    {

    }

    public abstract class StateMachineBase<T> where T : StateContextBase
    {
        private static EmptyState<T> emptyState;

        public static IState<T> Empty()
        {
            return emptyState;
        }

        public abstract void Update(float dt);

    }

    public interface IState<T> where T : StateContextBase
    {
        void Enter(T context);
        void Update(float dt);
        void Exit();
    }

    class EmptyState<T> : IState<T> where T : StateContextBase
    {

        public void Update(float dt)
        {

        }
        public void Enter(T context)
        {

        }
        public void Exit()
        {

        }
    }
}