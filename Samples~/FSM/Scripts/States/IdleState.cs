using UnityEngine;

namespace Klem.Samples.FSM.Scripts.States
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerFsm finiteStateMachine, PlayerData context) : base(finiteStateMachine, context)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{Context} Idle");
        }

        public override void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                FiniteStateMachine.ChangeState(new JumpState(FiniteStateMachine, Context));
            }
        }

        public override void HandleAxisInput(float axis)
        {
            if (Mathf.Abs(axis) > 0.1f)
            {
                FiniteStateMachine.ChangeState(new RunState(FiniteStateMachine, Context));
            }
        }
    }
}