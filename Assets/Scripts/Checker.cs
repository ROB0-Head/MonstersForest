using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool IsWallSliding { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TagChecker(collision, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        TagChecker(collision, false);
    }

    private void TagChecker(Collision2D collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            IsGrounded = value;
        }
        if (collision.gameObject.tag == ("Wall"))
        {
            IsWallSliding = value;
        }
    }
}
