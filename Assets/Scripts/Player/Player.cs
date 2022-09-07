using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    
    private Vector2 _workspace;
    
    public Animator Animator { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public int FaceDirection { get; private set; }

    private void Awake()
    {

    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        FaceDirection = 1;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
    }

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity,CurrentVelocity.y);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    public void SetVelocityY(float velocity)
    {
        Rigidbody.AddForce(Vector2.up * 1, ForceMode2D.Impulse);
        /*_workspace.Set(CurrentVelocity.x,velocity);
        Rigidbody.velocity = _workspace;
        CurrentVelocity = _workspace;*/
    }

    public void IfShouldFlip(float InputX)
    {
        if (InputX != 0 && InputX != FaceDirection)
            Flip();
    }
    
    private void Flip()
    {
        FaceDirection *= -1;
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}