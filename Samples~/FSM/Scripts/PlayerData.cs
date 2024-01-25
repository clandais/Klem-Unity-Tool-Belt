using System;
using System.Collections.Generic;
using Klem.Samples.FSM.Scripts;
using UnityEngine;

namespace Klem.Samples
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider2D capsuleCollider2D;

        private PlayerPhysics _playerPhysics;
        public PlayerPhysics PlayerPhysics => _playerPhysics;

        private bool _isGrounded;

        public bool IsGrounded
        {
            get
            {
                _isGrounded = _playerPhysics.GroundCheck();
                return _isGrounded;
            }
            set => _isGrounded = value;
        }

        public Animator Animator { get; private set; }
        public Rigidbody2D Rigidbody2D { get; private set; }

        [SerializeField] private float runSpeed = 5f;
        public float RunSpeed => runSpeed;

        [SerializeField] private float inAirMovementSpeed = 5f;
        public float InAirMovementSpeed => inAirMovementSpeed;

        [SerializeField] private float jumpForce = 5f;
        public float JumpForce => jumpForce;

        private void Start()
        {
            _playerPhysics = GetComponent<PlayerPhysics>();
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }
    }
}