using UnityEngine;

namespace Klem.Samples.FSM.Scripts.States
{
    public class RunState : PlayerBaseState
    {
        public RunState(PlayerFsm finiteStateMachine, PlayerData context) : base(finiteStateMachine, context)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{Context} Running");
        }

        public override void Update()
        {
            Context.PlayerPhysics.Move(Axis, Context.RunSpeed);

            if (Input.GetButtonDown("Jump"))
            {
                FiniteStateMachine.ChangeState(new JumpState(FiniteStateMachine, Context));
            }
        }

        public override void HandleAxisInput(float axis)
        {
            if (Mathf.Abs(axis) < float.Epsilon)
            {
                FiniteStateMachine.ChangeState(new IdleState(FiniteStateMachine, Context));
                return;
            }

            base.HandleAxisInput(axis);
        }
    }
}