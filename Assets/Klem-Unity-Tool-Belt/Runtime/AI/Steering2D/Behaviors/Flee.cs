using Klem.Runtime.AI.Steering2D.Interfaces;
using UnityEngine;

namespace Klem.Runtime.AI.Steering2D.Behaviors
{
	public class Flee : IBehavior
	{
		protected Vector2 target;
		protected SteeringAgent2D agent;
		
		public Flee(Vector2 target, SteeringAgent2D agent)
		{
			this.target = target;
			this.agent = agent;
		}
		
		public virtual SteeringOutput GetSteering()
		{
			var steering = new SteeringOutput
			{
				Linear = agent.Position - target,
				Angular = 0
			};
			
			steering.Linear = steering.Linear.normalized * agent.MaxAcceleration;
			return steering;
		}
	}
}