using UnityEngine;

namespace Klem.Runtime.AI.Steering2D
{
	public struct SteeringOutput
	{
		public Vector2 Linear { get; set; }
		public float Angular { get; set; }
	}
}