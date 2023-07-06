using Klem.Runtime.AI.Steering2D.Interfaces;

namespace Klem.Runtime.AI.Steering2D.Behaviors
{
	public class VelocityMatch : IBehavior
	{
		private SteeringAgent2D agent;
		private SteeringAgent2D target;
		
		public VelocityMatch(SteeringAgent2D agent, SteeringAgent2D target)
		{
			this.agent = agent;
			this.target = target;
		}
		
		
		public SteeringOutput GetSteering()
		{
			var steering = new SteeringOutput
			{
				Linear = target.Velocity - agent.Velocity,
				Angular = 0
			};
			
			return steering;	
		}
	}
}