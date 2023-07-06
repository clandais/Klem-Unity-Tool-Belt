using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors.Delegated
{
	public class Wander : Face
	{
		private float _wanderOrientation;
		public Wander(SteeringAgent2D agent, SteeringAgent2D targetAgent) : base(agent, targetAgent)
		{
		}

		public override SteeringOutput GetSteering()
		{
			_wanderOrientation += Random.Range(-1f, 1f) * Agent.WanderRate;
			var targetOrientation = _wanderOrientation + Agent.Orientation;
			
			Target = Agent.Position + Agent.WanderOffset * Agent.OrientationAsVector;
			Target += Agent.WanderRadius * new Vector2(Mathf.Cos(targetOrientation * Mathf.Deg2Rad), Mathf.Sin(targetOrientation * Mathf.Deg2Rad));
			
			Debug.DrawLine(Agent.Position, Target, Color.cyan);
			
			var result = base.GetSteering();
			
			result.Linear = Agent.MaxAcceleration * Agent.OrientationAsVector;
			
			return result;
			
		}
	}
}