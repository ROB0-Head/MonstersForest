using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRender;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private InputHandler _inputHandler;
    
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _wallJumpForce;
    [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _wallCheckDistance;

    private bool _doubleJump;
    private bool _jumpInput;
    private int _jumpAmount = 2;

    public bool IsGrounded { get; private set; }
    public bool IsWallSliding{ get; private set; }
    
    private bool _isTouchingWall;
    public float JumpVelocity { get; private set; }
    
    public float InputX { get; private set; }

    private void Update()
    {
        CheckIfWallSliding();
    }

    void FixedUpdate()
    {
        CheckingSurround();
        LogicUpdate();
        WallSliding();
    }

    private void LogicUpdate()
    {
        JumpVelocity = _rigidbody.velocity.y;
        InputX = _inputHandler.NormInputX;
        _jumpInput = _inputHandler.JumpInput;
        OnMove();
        if (_jumpInput)
        {
            OnJump();
        }
    }
    private void CheckingSurround()
    {
        IsGrounded = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckRadius, _whatIsGround);
        _isTouchingWall = Physics2D.Raycast(_wallChecker.position, transform.right, _wallCheckDistance, _whatIsGround);
    }
    private void CheckIfWallSliding()
    {
        if (_isTouchingWall && _rigidbody.velocity.y < 0)
        {
            IsWallSliding = true;
        }
        else
        {
            IsWallSliding = false;
        }
    }
    private void WallSliding()
    {
        if (IsWallSliding)
        {
            if(_rigidbody.velocity.y < -_wallSlideSpeed)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -_wallSlideSpeed);
            }
        }
    }
    private void OnMove()
    {
        _rigidbody.velocity = new Vector2(InputX * _speed, _rigidbody.velocity.y);
        IfNeedFlip(InputX);
    }

    private void OnJump()
    {
        if(IsGrounded)
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        if (!IsGrounded && IsWallSliding)
            _rigidbody.AddForce(Vector2.up * _wallJumpForce, ForceMode2D.Impulse);
        
    }

    private void IfNeedFlip(float horizontValue)
    {
        if (horizontValue > 0.01f)
        {
            _playerSpriteRender.flipX = false;
        }

        if (horizontValue < -0.01f)
        {
            _playerSpriteRender.flipX = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckRadius);

        Gizmos.DrawLine(_wallChecker.position, new Vector3(_wallChecker.position.x + _wallCheckDistance, _wallChecker.position.y, _wallChecker.position.z));
    }
}