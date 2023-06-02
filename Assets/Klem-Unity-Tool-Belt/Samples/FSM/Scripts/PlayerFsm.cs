using System;
using Klem.Core.AI.FSM;
using Klem.Samples.FSM.Scripts.States;
using UnityEngine;

namespace Klem.Samples.FSM.Scripts
{
    public class PlayerFsm : FiniteStateMachine<PlayerFsm, PlayerData>
    {
        private void Awake()
        {
            Initialize(new IdleState(this, GetComponent<PlayerData>()));
        }

        protected override void Update()
        {
            base.Update();
            (CurrentState as PlayerBaseState)?.HandleAxisInput(Input.GetAxis("Horizontal"));
        }
    }
}