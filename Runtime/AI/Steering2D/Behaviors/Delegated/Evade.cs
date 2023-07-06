using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors.Delegated
{
	public class Evade : Flee
	{
		private SteeringAgent2D targetAgent;
		private float maxPrediction;
		
		public Evade(SteeringAgent2D target, SteeringAgent2D agent, float maxPrediction = 1f) : base(target.Position, agent)
		{
			targetAgent = target;
			this.maxPrediction = maxPrediction;
		}

		public override SteeringOutput GetSteering()
		{
			var direction = targetAgent.Position - agent.Position;
			var distance = direction.magnitude;
			
			var speed = agent.Velocity.magnitude;
			
			float prediction;
			if (speed <= distance / maxPrediction)
				prediction = maxPrediction;
			else
				prediction = distance / speed;
			
			target = targetAgent.Position + targetAgent.Velocity * prediction;
			
			return base.GetSteering();
		}
	}
}