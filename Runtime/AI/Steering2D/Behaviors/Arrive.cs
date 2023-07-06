using Klem.Runtime.AI.Steering2D.Interfaces;
using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors
{
	public class Arrive : IBehavior
	{
		private const float StopRadius = 1f;
		private const float SlowRadius = 5f;
		private const float TimeTotarget = 0.1f;
		
		private Transform target;
		private SteeringAgent2D agent;
		
		public Arrive(Transform target, SteeringAgent2D agent)
		{
			this.target = target;
			this.agent = agent;
		}


		public SteeringOutput GetSteering()
		{
			var steering = new SteeringOutput();
			var direction = (Vector2)target.position - agent.Position;
			var distance = direction.magnitude;

			if (distance < StopRadius)
			{
				steering.Linear = Vector2.zero;
				return steering;
			}
			
			float targetSpeed;
			if (distance > SlowRadius)
				targetSpeed = agent.MaxSpeed;
			else
				targetSpeed = agent.MaxSpeed * distance / SlowRadius;
			
			var targetVelocity = direction;
			targetVelocity.Normalize();
			targetVelocity *= targetSpeed;
			
			steering.Linear = targetVelocity - agent.Velocity;
			steering.Linear /= TimeTotarget;
			
			if (steering.Linear.magnitude > agent.MaxSpeed)
			{
				steering.Linear.Normalize();
				steering.Linear *= agent.MaxSpeed;
			}
			
			return steering;
		}
	}
}