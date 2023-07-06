using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors.Delegated
{
	public class Face : Align
	{
		public Face(SteeringAgent2D agent, SteeringAgent2D targetAgent) : base(agent, targetAgent)
		{
		}

		public override SteeringOutput GetSteering()
		{
			if (TargetAgent)
				Target = TargetAgent.Position;
			return base.GetSteering();
		}


		protected override float GetDeltaAngle()
		{
			var direction = Target - Agent.Position;
			direction.Normalize();
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			return Mathf.DeltaAngle(Agent.Orientation, angle);
		}
	}
}