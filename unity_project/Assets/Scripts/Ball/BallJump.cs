using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJump : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private bool jumping = false;
    private AudioSource jumpSource;

    private void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        jumpSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            jumping = true;
        }
    }

    //Sets velocity to 0 to counter the current gravity acceleration and pushes the ball up. 
    void FixedUpdate()
    {
        if (jumping)
        {
            rigidBody.velocity = new Vector2(0, 0);
            rigidBody.AddForce(new Vector2(0, 700), ForceMode2D.Impulse);
            jumpSource.Play();
            jumping = false;
        }
    }
}
