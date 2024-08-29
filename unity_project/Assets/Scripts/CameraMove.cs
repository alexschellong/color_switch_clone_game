using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera mainCamera;
    public Transform ballPos;

    private bool ballPast45 = false;
    private bool jumpInput = false;

    private float ballPosPresent;
    private float ballPosPrevious;

    private void Start()
    {
        mainCamera = this.GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            jumpInput = true;
        }

        //If the ball is above 45% of the screen attach the camera to it. Once it starts falling deattach it.
        if (!ballPast45 && jumpInput)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(ballPos.position);

            if (viewPos.y > 0.45f)
            {
                this.transform.parent = ballPos;
                ballPast45 = true;
                jumpInput = false;
                ballPosPrevious = ballPos.position.y;
            }
        }
        else
        {
            ballPosPresent = ballPos.position.y;
            //ball is falling
            if (ballPosPresent - ballPosPrevious < 0)
            {
                Vector2 viewPos = mainCamera.WorldToViewportPoint(ballPos.position);
                ballPosPrevious = ballPosPresent;
                this.transform.parent = null;

                ballPast45 = false;
                
            }
            else
            {
                ballPosPrevious = ballPosPresent;
            }
        }
    }
}
