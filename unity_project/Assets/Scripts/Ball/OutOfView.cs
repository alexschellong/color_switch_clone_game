using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OutOfView : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject ball;
    private GameObject ballBottom;
    private float ballDistanceCentreToEdge;
    private BallDeath _ballDeath;

    void Start()
    {
        ballDistanceCentreToEdge = ball.GetComponent<CircleCollider2D>().bounds.extents.y;
        ballBottom = new GameObject("ballBottom");
        ballBottom.transform.position = new Vector2(ball.transform.position.x, ball.transform.position.y - ballDistanceCentreToEdge);
        ballBottom.transform.SetParent(ball.transform);

        _ballDeath = ball.GetComponent<BallDeath>();
    }

    //Destroys ball once its lowerbound touches the bottom of the screen
    void Update()
    {
        Vector2 viewPos = mainCamera.WorldToViewportPoint(ballBottom.transform.position);
        if (viewPos.y <= 0.0F)
        {
            _ballDeath.ballDeath();
        }
    }
}

