using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors.Delegated
{
	public class LookWhereYoureGoing : Align
	{
		public LookWhereYoureGoing(SteeringAgent2D agent) : base(agent, null)
		{
		}


		protected override float GetDeltaAngle()
		{
			var direction = Agent.Velocity;
			direction.Normalize();
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			return Mathf.DeltaAngle(Agent.Orientation, angle);
		}
	}
}