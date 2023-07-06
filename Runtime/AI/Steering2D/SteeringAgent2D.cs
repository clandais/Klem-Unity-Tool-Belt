using System;
using Klem.Runtime.AI.Steering2D.Interfaces;
using UnityEngine;

namespace Klem.Runtime.AI.Steering2D
{
	[AddComponentMenu("Klem/AI/Steering Agent 2D")]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(CircleCollider2D))]
	public class SteeringAgent2D : MonoBehaviour
	{
		[SerializeField] private float rotationSpeed = 1f;

		private void OnValidate()
		{
			if (rb)
				Rotation = rotationSpeed;
		}

		public Vector2 Position => rb.position;
		public float Orientation => rb.rotation;

		public Vector2 Velocity
		{
			get => rb.velocity;
			private set => rb.velocity = value;
		}

		public float Rotation
		{
			get => rb.angularVelocity;
			private set => rb.angularVelocity = value;
		}



		
		[SerializeField] private float maxSpeed;
		public float MaxSpeed => maxSpeed;

		[SerializeField] private float maxAcceleration;
		public float MaxAcceleration => maxAngularAcceleration;
		public Vector2 OrientationAsVector => new(Mathf.Cos(Orientation), Mathf.Sin(Orientation));

		private Rigidbody2D rb;
		
		private IBehavior steeringBehavior;
		
		
		public float maxRotation;
		public float maxAngularAcceleration;
		public float targetRotationRadius = 33f / 2f;
		public float slowRotationRadius = 33f;

		[SerializeField] private Vector2 wanderOffset = new Vector2(0, 0.5f);
		public Vector2 WanderOffset => wanderOffset;
		
		[SerializeField] private float wanderRadius = 1f;
		public float WanderRadius => wanderRadius;
		
		[SerializeField] private float wanderRate = 0.5f;
		public float WanderRate => wanderRate;
        
		
		private void Awake()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update()
		{
			
			if (steeringBehavior == null)
			{
				return;
			}
            
			ApplySteering(steeringBehavior.GetSteering(), Time.deltaTime);
		}
		
		public void SetBehavior(IBehavior behavior)
		{
			steeringBehavior = behavior;
		}

		public void ApplySteering(SteeringOutput steeringOutput, float deltaTime)
		{
			Velocity += steeringOutput.Linear * deltaTime;
            Rotation += steeringOutput.Angular * deltaTime;
            
            CheckVelocityAndRotation();
            
            rb.velocity = Velocity;
            rb.angularVelocity = Rotation;
		}

		private void CheckVelocityAndRotation()
		{
			if (Velocity.magnitude > maxSpeed)
			{
				Velocity = Velocity.normalized * maxSpeed;
			}
			
			if (Math.Abs(Rotation) > maxRotation)
			{
				Rotation = Math.Sign(Rotation) * maxRotation;
			}
		}


		private void OnDrawGizmos()
		{
			if (!rb)
				return;
			
			// Draw velocity
			Gizmos.color = Color.blue;
            Gizmos.DrawLine(Position, Position + Velocity);
            
            // Draw orientation
            Gizmos.color = Color.green;
            Gizmos.DrawLine(Position, Position + (Vector2)(Quaternion.Euler(0, 0, Orientation) * Vector2.right));
			
            
            // Draw target rotation
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Position, Position + (Vector2)(Quaternion.Euler(0, 0,  Orientation + Rotation) * Vector2.right));
            
		}
	}
}