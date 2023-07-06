using Klem.Runtime.AI.Steering2D.Interfaces;
using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors
{
	public class Align : IBehavior
	{
		protected readonly SteeringAgent2D Agent;
		protected readonly SteeringAgent2D TargetAgent;
		
		protected Vector2 Target;

		public Align(SteeringAgent2D agent, SteeringAgent2D targetAgent)
		{
			Agent = agent;
			TargetAgent = targetAgent;
		}

		public virtual SteeringOutput GetSteering()
		{
			var steering = new SteeringOutput();
			float deltaAngle = GetDeltaAngle();

			float absDeltaAngle = Mathf.Abs(deltaAngle);

			if (absDeltaAngle < Agent.targetRotationRadius) return steering;

			float targetRotation;
			if (absDeltaAngle > Agent.slowRotationRadius)
				targetRotation = Agent.maxRotation * Mathf.Sign(deltaAngle);
			else
				targetRotation = Agent.maxRotation * deltaAngle / Agent.slowRotationRadius;
            

			steering.Angular = GetSteeringAngular(targetRotation);
			steering.Linear = Vector2.zero;

			return steering;
		}

		protected virtual float GetDeltaAngle()
		{
			return Mathf.DeltaAngle(
				Agent.Orientation, TargetAgent.Orientation);
		}

		protected float GetSteeringAngular(float targetRotation)
		{
			float steeringAngular = targetRotation - Agent.Rotation;
			float angularAcceleration = Mathf.Abs(steeringAngular);

			if (angularAcceleration > Agent.maxAngularAcceleration)
				steeringAngular *= Agent.maxAngularAcceleration / steeringAngular * Mathf.Sign(steeringAngular);
			return steeringAngular;
		}
	}
}