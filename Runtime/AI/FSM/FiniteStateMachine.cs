using UnityEngine;

namespace Klem.Core.AI.FSM
{

    /// <summary>
    /// A finite state machine that can be used to manage the states of a game object.
    /// </summary>
    /// <typeparam name="T0">The State Machine Type</typeparam>
    /// <typeparam name="T1">The Context Type</typeparam>
    public abstract class FiniteStateMachine<T0, T1> : MonoBehaviour
    {
        /// <summary>
        /// The current state of the finite state machine.
        /// </summary>
        public BaseState<T0, T1> CurrentState { get; private set; }
        
        
        /// <summary>
        /// Initialize the finite state machine with a starting state.
        /// </summary>
        protected void Initialize(BaseState<T0, T1> startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        
        /// <summary>
        /// Change the current state to the new state.
        /// Exit the current state and enter the new state.
        /// </summary>
        /// <param name="newState"></param>
        protected void ChangeState(BaseState<T0, T1> newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
        
        /// <summary>
        /// Update the current state.
        /// </summary>
        public virtual void Update()
        {
            CurrentState?.Update();
        }
        
        /// <summary>
        /// FixedUpdate the current state.
        /// </summary>
        public virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
        
        /// <summary>
        /// LateUpdate the current state.
        /// </summary>
        public virtual void LateUpdate()
        {
            CurrentState?.LateUpdate();
        }
        
    }
}