using Klem.Runtime.AI.Steering2D;
using Klem.Runtime.AI.Steering2D.Behaviors;
using Klem.Runtime.AI.Steering2D.Behaviors.Delegated;
using UnityEngine;

namespace Klem.Samples.Samples.Steering.Scripts
{
	public class AlignAgent : MonoBehaviour
	{
	
		private SteeringAgent2D _agent;
		[SerializeField] private SteeringAgent2D _target;
		
		private void Start()
		{
			_agent = GetComponent<SteeringAgent2D>();
			_agent.SetBehavior(new Face(_agent, _target));
		}
		
	}
}