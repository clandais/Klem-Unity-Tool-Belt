using System;
using Klem.Runtime.AI.Steering2D;
using Klem.Runtime.AI.Steering2D.Behaviors;
using Klem.Runtime.AI.Steering2D.Behaviors.Delegated;
using UnityEngine;

namespace Klem.Samples.Samples.Steering.Scripts
{
	public class SeekAgent : MonoBehaviour
	{
		[SerializeField] private Transform target;
		private SteeringAgent2D agent;
		private Camera cam;
		
		
		private void Start()
		{
			cam = Camera.main;
			agent = GetComponent<SteeringAgent2D>();
			agent.SetBehavior(
				new Wander( 
					agent, null
					));
		}

		private void LateUpdate()
		{
			target.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}