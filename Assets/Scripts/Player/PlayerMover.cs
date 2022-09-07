using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRender;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Checker _groundChecker;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _jumpAmount;

    private int _jumpAmountLeft;
    private bool _canJump;

    public bool IsGrounded { get; private set; }
    public bool InAir { get; private set; }
    public float MoveHorizontal { get; private set; }

    private void Update()
    {
        IsGrounded = _groundChecker.IsGrounded;
        InAir = _groundChecker.InAir;
        CheckIfCanJump();
    }

    void FixedUpdate()
    {
        InputMovementLogic();
    }

    private void CheckIfCanJump()
    {
        if (IsGrounded && _rigidbody.velocity.y <= 0.01f)
        {
            _jumpAmountLeft = _jumpAmount;
        }

        /*if (isTouchingWall)
        {
            checkJumpMultiplier = false;
            canWallJump = true;
        }*/

        if (_jumpAmountLeft <= 0)
        {
            _canJump = false;
        }
        else
        {
            _canJump = true;
        }
    }

    private void InputMovementLogic()
    {
        MoveHorizontal = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(MoveHorizontal * _speed, _rigidbody.velocity.y);
        if (Input.GetKey(KeyCode.Space))
            NormalJump();
        Flip(MoveHorizontal);
    }

    private void NormalJump()
    {
        if (_canJump || (_canJump && InAir))
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpAmountLeft--;
        }
    }

    private void Flip(float horizontValue)
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