using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public GameObject ball;
    private BallDeath _ballDeath;

    private SpriteRenderer spriteRendererBall;
    private SpriteRenderer spriteRendererObstacle;

    private void Start()
    {
        spriteRendererObstacle = this.gameObject.GetComponent<SpriteRenderer>();

        spriteRendererBall = ball.GetComponent<SpriteRenderer>();
        _ballDeath = ball.GetComponent<BallDeath>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 3)
        {
            if (spriteRendererObstacle.color != spriteRendererBall.color)
            {
                _ballDeath.ballDeath();
            }
        }
    }
}
