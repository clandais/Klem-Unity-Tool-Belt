using Klem.Core.AI.FSM;
using UnityEngine;

namespace Klem.Samples.FSM.Scripts.States
{
    public class JumpState : PlayerBaseState
    {
        private Rigidbody2D _rigidbody2D;
        private static readonly int Jump = Animator.StringToHash("Jump");

        public JumpState(PlayerFsm finiteStateMachine, PlayerData context) : base(finiteStateMachine, context)
        {
        }

        public override void Enter()
        {
            Debug.Log($"{Context} Jumping");
            Context.Animator.SetTrigger(Jump);
            Context.IsGrounded = false;
            _rigidbody2D = Context.Rigidbody2D;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,
                Mathf.Sqrt(2 * Context.JumpForce * Mathf.Abs(Physics2D.gravity.y)));
        }

        public override void Update()
        {
            Context.PlayerPhysics.Move(Axis, Context.InAirMovementSpeed);
            if (_rigidbody2D.velocity.y < 0)
            {
                FiniteStateMachine.ChangeState(new FallState(FiniteStateMachine, Context));
            }
        }
    }
}