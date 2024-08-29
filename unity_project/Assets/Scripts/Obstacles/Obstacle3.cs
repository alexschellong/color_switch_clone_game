using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle3 : MonoBehaviour
{
    public GameObject ball;
    private Color[] colorArray;

    private Color yellow = new Color(230 / 255f, 194 / 255f, 41 / 255f);
    private Color orange = new Color(241 / 255f, 113 / 255f, 5 / 255f);
    private Color red = new Color(209 / 255f, 17 / 255f, 73 / 255f);
    private Color blue = new Color(102 / 255f, 16 / 255f, 242 / 255f);

    private SpriteRenderer middleSpikeLeft;
    private SpriteRenderer middleSpikeRight;
    private SpriteRenderer upperSpikeRight;
    private SpriteRenderer lowerSpikeLeft;

    private Transform spikesLeft;
    private Transform spikesRight;

    private bool pause = false;
    private bool spikesClosed = true;
    private float elapsedTime = 0;
    private float speed = 0.009f;

    private float currentPos;
    private float closedPos = 40.3557f;
    private float openPosLeft = -882f;
    private float openPosRight = 882f + 40.3557f;


    //Randomly shuffles the elements in the array
    void Shuffle(int[] items)
    {
        for (int t = 0; t < items.Length; t++)
        {
            int tmp = items[t];
            int r = Random.Range(t, items.Length);
            items[t] = items[r];
            items[r] = tmp;
        }
    }

    //shifts the colors in the array to the right and assigns them to the spikes.
    //The middle spikes share the same color.
    private void changeColor()
    {

        Color lastElement = colorArray[colorArray.Length - 1];

        for (int t = colorArray.Length - 1; t > 0; t--)
        {
            colorArray[t] = colorArray[t - 1];
        }

        colorArray[0] = lastElement;

        upperSpikeRight.color = colorArray[0];
        middleSpikeLeft.color = colorArray[1];
        middleSpikeRight.color = colorArray[1];
        lowerSpikeLeft.color = colorArray[2];

    }

    private void Start()
    {
        //Shuffles the colorArray so that the initial order of colors is randomized
        colorArray = new Color[] { yellow, orange, red, blue };
        int[] indexes = new int[] { 0, 1, 2, 3 };
        Shuffle(indexes);

        Color[] colorArrayMixed = new Color[4];

        for (int t = 0; t < indexes.Length; t++)
        {
            colorArrayMixed[t] = colorArray[indexes[t]];
        }

        colorArray = colorArrayMixed;


        //Adds the reference to the individual obstacles to the player's ball in case of collision. 
        this.transform.GetChild(1).transform.GetChild(1).GetComponent<ObstacleCollision>().ball = ball;
        this.transform.GetChild(0).transform.GetChild(0).GetComponent<ObstacleCollision>().ball = ball;
        this.transform.GetChild(1).transform.GetChild(0).GetComponent<ObstacleCollision>().ball = ball;
        this.transform.GetChild(0).transform.GetChild(1).GetComponent<ObstacleCollision>().ball = ball;

        //Gets the references to the individual obstacles in order to change colors during runtime.
        upperSpikeRight = this.transform.GetChild(1).transform.GetChild(1).GetComponent<SpriteRenderer>();
        middleSpikeLeft = this.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        middleSpikeRight = this.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>();
        lowerSpikeLeft = this.transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>();

        upperSpikeRight.color = colorArray[0];
        middleSpikeLeft.color = colorArray[1];
        middleSpikeRight.color = colorArray[1];
        lowerSpikeLeft.color = colorArray[2];

        ////Gets the references to the individual obstacles in order to move obstacles during runtime.
        spikesLeft = this.transform.GetChild(0);
        spikesRight = this.transform.GetChild(1);
        currentPos = spikesLeft.position.x;
    }

    //Opens or closes the spikes/obstacle after a delay
    private IEnumerator waiter()
    {
        if (spikesClosed)
        {
            yield return new WaitForSeconds(0.6f);
            spikesClosed = false;
            elapsedTime = 0;
            pause = false;
        }
        else
        {
            yield return new WaitForSeconds(2);
            spikesClosed = true;
            elapsedTime = 0;
            pause = false;
        }
    }

    private void Update()
    {
        if (pause == false)
        {
            if (spikesClosed)
            {
                elapsedTime += Time.deltaTime * speed;
                currentPos = Mathf.Lerp(closedPos, openPosRight, 100f * elapsedTime);
                spikesRight.position = new Vector2(currentPos, spikesLeft.position.y);
                currentPos = Mathf.Lerp(closedPos, openPosLeft, 100f * elapsedTime);
                spikesLeft.position = new Vector2(currentPos, spikesLeft.position.y);

                if (currentPos <= openPosLeft)
                {
                    changeColor();
                    pause = true;
                    StartCoroutine(waiter());
                }
            }
            else
            {
                currentPos = Mathf.Lerp(openPosRight, closedPos, 100f * elapsedTime);
                spikesRight.position = new Vector2(currentPos, spikesRight.position.y);
                elapsedTime += Time.deltaTime * speed;
                currentPos = Mathf.Lerp(openPosLeft, closedPos, 100f * elapsedTime);
                spikesLeft.position = new Vector2(currentPos, spikesRight.position.y);

                if (currentPos >= closedPos)
                {
                    pause = true;
                    StartCoroutine(waiter());
                }
            }
        }
    }
}
