using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }
    public bool InAir { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grounded(collision, true, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Grounded(collision, false, true);
    }

    private void Grounded(Collision2D collision, bool groundValue, bool airValue)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            IsGrounded = groundValue;
            InAir = airValue;
        }
    }
}
