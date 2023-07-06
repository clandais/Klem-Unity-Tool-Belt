using UnityEngine;

namespace Klem.Samples.FSM.Scripts.States
{
    public class FallState : PlayerBaseState
    {
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Land = Animator.StringToHash("Land");

        public FallState(PlayerFsm finiteStateMachine, PlayerData context) : base(finiteStateMachine, context)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{Context} Falling");

            Context.Animator.SetTrigger(Fall);
        }

        public override void Update()
        {
            if (Context.IsGrounded)
            {
                FiniteStateMachine.ChangeState(new IdleState(FiniteStateMachine, Context));
            }
        }

        public override void Exit()
        {
            Context.Animator.SetTrigger(Land);
        }
    }
}