using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRender;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Checker _groundChecker;
    [SerializeField] private InputHandler _inputHandler;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private bool _doubleJump;
    private bool _jumpInput;
    private int _jumpAmount;
    private int FaceDirection = 1;

    public bool IsGrounded { get; private set; }
    public bool InAir { get; private set; }
    public float InputX { get; private set; }

    private void Update()
    {
        IsGrounded = _groundChecker.IsGrounded;
        InAir = _groundChecker.InAir;
        DoubleJumpChecker();
    }

    void FixedUpdate()
    {
        LogicUpdate();
    }

    private void DoubleJumpChecker()
    {
        if (IsGrounded)
        {
            _doubleJump = false;
            _jumpAmount = 0;
        }
        if (_jumpAmount == 1)
        {
            _doubleJump = true;
        }
        else
        {
            _doubleJump = false;
        }
    }

    private void LogicUpdate()
    {
        InputX = _inputHandler.NormInputX;
        _jumpInput = _inputHandler.JumpInput;
        OnMove();
        if ((_jumpInput && IsGrounded) || (_jumpInput && !IsGrounded && _doubleJump))
        {
            OnJump();
            _jumpAmount++;
        }
    }

    private void OnMove()
    {
        _rigidbody.velocity = new Vector2(InputX * _speed, _rigidbody.velocity.y);
        IfNeedFlip(InputX);
    }

    private void OnJump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
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
}