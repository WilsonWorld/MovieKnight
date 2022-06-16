using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (Input.GetAxis("Horizontal") < 0.0f) {
            rb.AddForce(Vector2.left * m_speed * Time.fixedDeltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            rb.AddForce(Vector2.right * m_speed * Time.fixedDeltaTime);
        }
        if (Input.GetAxis("Vertical") < 0.0f)
        {
            rb.AddForce(Vector2.down * m_speed * Time.fixedDeltaTime);
        }
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            rb.AddForce(Vector2.up * m_speed * Time.fixedDeltaTime);
        }
    }
}
