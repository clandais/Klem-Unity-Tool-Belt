using System.Collections.Generic;
using UnityEngine;

namespace Klem.Samples.FSM.Scripts
{
    public class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private ContactFilter2D groundContactFilter;
        [SerializeField] private ContactFilter2D movementContactFilter;

        private Rigidbody2D _rigidbody2D;
        private CapsuleCollider2D _capsuleCollider2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        }

        private void FixedUpdate()
        {
            ApplyGravity();
        }

        public bool GroundCheck()
        {
            var colliders = new List<Collider2D>();
            var hits = Physics2D.OverlapCapsule(_capsuleCollider2D.bounds.center, _capsuleCollider2D.size,
                _capsuleCollider2D.direction, 0, groundContactFilter, colliders);

            return hits > 0;
        }

        private void ApplyGravity()
        {
            var velocity = _rigidbody2D.velocity;
            velocity = new Vector2(velocity.x, velocity.y + Physics2D.gravity.y * Time.fixedDeltaTime);
            _rigidbody2D.velocity = velocity;
        }

        public void Move(float axis, float speed)
        {
            var velocity = _rigidbody2D.velocity;
            velocity.x = axis * speed;

            var colliders = new List<Collider2D>();

            var hit = Physics2D.OverlapCapsule(
                _capsuleCollider2D.bounds.center + new Vector3(velocity.x, velocity.y, 0f) * Time.fixedDeltaTime,
                _capsuleCollider2D.size, _capsuleCollider2D.direction, 0f, movementContactFilter, colliders);

            if (hit > 0)
            {
                velocity.x -= velocity.x;
            }

            _rigidbody2D.velocity = velocity;
        }
    }
}