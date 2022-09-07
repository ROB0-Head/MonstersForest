using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMover _mover;

    private void Update()
    {
        _animator.SetBool("Idle",_mover.InputX == 0 && _mover.IsGrounded);
        _animator.SetBool("Move",_mover.InputX != 0 && _mover.IsGrounded);
        _animator.SetBool("Jump",(!_mover.IsGrounded && _mover.InputX != 0) || !_mover.IsGrounded);
    }
}