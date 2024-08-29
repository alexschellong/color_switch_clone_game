using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameOver : MonoBehaviour
{
    public GameObject scoreTxt;
    public GameObject text;
    public GameObject instances;
    public ScoreCounter score;

    public Camera mainCamera;
    public GameObject ball;
    public LevelGenerator levelGenerator;

    private bool doneWaiting = false;

    public void GameOverFunc()
    {
        StartCoroutine(waiter());
    }

    void Update()
    {
        //Displays ingame interface after the first input. And resets initial values.
        if (Input.anyKeyDown && doneWaiting)
        {
            mainCamera.transform.position = new Vector3(0, 0, -359f);

            ball.transform.transform.position = new Vector2(0, -740);
            ball.SetActive(true);
            ball.GetComponent<firstJump>().restart();

            levelGenerator.generateSegment(ball.transform.position.y);

            score.score = 0;
            scoreTxt.GetComponent<TextMeshProUGUI>().text = "0";

            scoreTxt.SetActive(true);
            text.SetActive(false);

            mainCamera.GetComponent<CameraMove>().enabled = true;

            doneWaiting = false;
            this.enabled = false;
        }
    }

    //Destroys previously generated obstacles and hides ingame interface and objects. Displays gameover text.
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1.2f);
        scoreTxt.SetActive(false);
        text.SetActive(true);

        foreach (Transform child in instances.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        doneWaiting = true;
    }
}
