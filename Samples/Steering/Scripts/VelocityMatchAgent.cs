using System;
using Klem.Runtime.AI.Steering2D;
using Klem.Runtime.AI.Steering2D.Behaviors;
using Klem.Runtime.AI.Steering2D.Behaviors.Delegated;
using UnityEngine;
using UnityEngine.Serialization;

namespace Klem.Samples.Samples.Steering.Scripts
{
	
	[AddComponentMenu("Klem Samples/Steering/Velocity Match Agent")]
	public class VelocityMatchAgent : MonoBehaviour
	{
		private SteeringAgent2D _agent;
		[SerializeField] private SteeringAgent2D target;


		private void Start()
		{
			_agent = GetComponent<SteeringAgent2D>();
			_agent.SetBehavior(new Pursue( target, _agent));
		}
	}
}