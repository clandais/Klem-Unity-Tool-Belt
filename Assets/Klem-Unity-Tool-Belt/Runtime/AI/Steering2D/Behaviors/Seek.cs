using System.Numerics;
using Klem.Runtime.AI.Steering2D.Interfaces;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Klem.Runtime.AI.Steering2D.Behaviors
{
	public class Seek : IBehavior
	{
		protected Vector2 target;
		protected SteeringAgent2D agent;
		
		public Seek(Vector2 target, SteeringAgent2D agent)
		{
			this.target = target;
			this.agent = agent;
		}
		
		
		public virtual SteeringOutput GetSteering()
		{
			var steering = new SteeringOutput
			{
				Linear = target - agent.Position,
				Angular = 0
			};
			
			steering.Linear = steering.Linear.normalized * agent.MaxAcceleration;
			return steering;
		}
	}
}