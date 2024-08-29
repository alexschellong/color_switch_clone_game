using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject introText;

    public GameObject instances;
    public GameObject gameManager;
    public GameObject playerBall;
    public GameObject scoreText;

    private void Update()
    {
        if (Input.anyKeyDown && this.enabled)
        {
            instances.SetActive(true);
            gameManager.SetActive(true);
            playerBall.SetActive(true);
            scoreText.SetActive(true);

            introText.SetActive(false);

            this.gameObject.SetActive(false);
            this.enabled = false;
        }
    }

}
