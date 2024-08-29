using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstJump : MonoBehaviour
{
    private Rigidbody2D ballRS;

    private void Start()
    {
        ballRS = this.GetComponent<Rigidbody2D>();
    }

    public void restart()
    {
        ballRS.gravityScale = 0;
        this.enabled = true;
    }


    //Releases the ball after the first input at the start of the game. Otherwise it would fall right away.
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            ballRS.gravityScale = 100f;
            this.enabled = false;
        }
    }
}
