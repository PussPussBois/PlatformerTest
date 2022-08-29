using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerTest
{
   public class PlayerMovement : MonoBehaviour
   {
      [SerializeField] private int _maxJumps;
      [SerializeField] private float _speed;
      [SerializeField] private float _jumpForce;
      [SerializeField] private Transform _groundCheck;
      [SerializeField] private LayerMask _groundLayer;

      private bool _facingRight = true;
      private bool _jumping = false;
      private bool _grounded;

      private Rigidbody2D _rb;
      private float _moveDirection;
      private int _jumpCount;

      private void Awake()
      {
         _rb = GetComponent<Rigidbody2D>();
      }

      private void Start()
      {
         _jumpCount = _maxJumps;
      }

      private void Update()
      {
         ProcessInput();
         Animate();
      }

      private void FixedUpdate()
      {
         _grounded = Physics2D.OverlapCircle(_groundCheck.position, 0.5f, _groundLayer);

         if (_grounded)
         {
            _jumpCount = _maxJumps;
         }

         Move();
      }

      private void ProcessInput()
      {
         _moveDirection = Input.GetAxis("Horizontal");

         if (Input.GetButtonDown("Jump"))
         {
            _jumping = true;
         }
      }

      private void Move()
      {
         _rb.velocity = new Vector2(_speed * _moveDirection, _rb.velocity.y);

         if (_jumping && _jumpCount > 0)
         {
            _rb.AddForce(new Vector2(0, _jumpForce));
            _jumpCount--;
         }

         _jumping = false;
      }

      private void Animate()
      {
         if ((_facingRight && _moveDirection < 0) || (!_facingRight && _moveDirection > 0))
         {
            FlipCharacter();
         }
      }

      private void FlipCharacter()
      {
         _facingRight = !_facingRight;
         transform.Rotate(0f, 180f, 0f);
      }
   }
}
