using Klem.Core.AI.FSM;

namespace Klem.Samples.FSM.Scripts
{
    public abstract class PlayerBaseState : BaseState<PlayerFsm, PlayerData>
    {
        protected float Axis;

        protected PlayerBaseState(PlayerFsm finiteStateMachine, PlayerData context) : base(finiteStateMachine, context)
        {
        }

        public virtual void HandleAxisInput(float axis)
        {
            Axis = axis;
        }
    }
}