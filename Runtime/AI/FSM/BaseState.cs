namespace Klem.Core.AI.FSM
{
    /// <summary>
    /// A base state that can be used to manage the states of a game object.
    /// </summary>
    /// <typeparam name="T0">The State Machine type</typeparam>
    /// <typeparam name="T1">The Context Type</typeparam>
    public abstract class BaseState<T0, T1> 
    {
        /// <summary>
        /// The finite state machine that this state belongs to.
        /// </summary>
        protected T0 FiniteStateMachine;
        
        /// <summary>
        /// The context that this state belongs to.
        /// </summary>
        protected T1 Data;

        /// <summary>
        /// Create a new state.
        /// </summary>
        /// <param name="finiteStateMachine"></param>
        /// <param name="data"></param>
        protected BaseState(T0 finiteStateMachine, T1 data)
        {
            FiniteStateMachine = finiteStateMachine;
            Data = data;
        }

        /// <summary>
        /// Enter the state.
        /// </summary>
        public virtual void Enter() {}
        
        
        /// <summary>
        /// Update the state.
        /// </summary>
        public virtual void Update() {}
        
        /// <summary>
        /// FixedUpdate the state.
        /// </summary>
        public virtual void FixedUpdate() {}
        
        /// <summary>
        /// LateUpdate the state.
        /// </summary>
        public virtual void LateUpdate() {}
        
        /// <summary>
        /// Exit the state.
        /// </summary>
        public virtual void Exit() {}
    }
}