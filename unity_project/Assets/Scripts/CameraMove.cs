using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera mainCamera;
    public Transform ballPos;

    private bool ballPast45 = false;

    private float ballPosPresent;
    private float ballPosPrevious;

    private void Start()
    {
        mainCamera = this.GetComponent<Camera>();
    }
    private void Update()
    {
        //If the ball is above 45% of the screen attach the camera to it. Once it starts falling deattach it.
        if (!ballPast45)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(ballPos.position);

            if (viewPos.y > 0.45f)
            {
                this.transform.parent = ballPos;
                ballPast45 = true;

                ballPosPrevious = ballPos.position.y;
            }
        }
        else
        {
            ballPosPresent = ballPos.position.y;
            //-2 is arbitrary. Using 0 sometimes detaches the camera while the ball is not below 45% of the screen
            if (ballPosPresent - ballPosPrevious < -2)
            {
                //ball is falling
                Vector2 viewPos = mainCamera.WorldToViewportPoint(ballPos.position);
                ballPosPrevious = ballPosPresent;
                this.transform.parent = null;

                //check if the ball is below 45% screen height to avoid reparenting the camera immediately
                if (viewPos.y < 0.45f)
                {
                    ballPast45 = false;
                }
            }
            else
            {
                ballPosPrevious = ballPosPresent;
            }
        }
    }
}
