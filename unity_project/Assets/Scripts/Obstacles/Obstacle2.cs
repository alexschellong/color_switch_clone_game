using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{

    public GameObject ball;

    private SpriteRenderer[] arcs;
    private Color[] colorArray;

    private Color yellow = new Color(230 / 255f, 194 / 255f, 41 / 255f);
    private Color orange = new Color(241 / 255f, 113 / 255f, 5 / 255f);
    private Color red = new Color(209 / 255f, 17 / 255f, 73 / 255f);
    private Color blue = new Color(102 / 255f, 16 / 255f, 242 / 255f);

    private bool randomSwitch;

    private float degreesPerSec = 90f;
    private Transform rotatable;

    private bool switching = true;
    private float currentScale;
    private float maxScale = 1.1f;
    private float minScale = 0.85f;
    private float elapsedTime = 0;
    private float speed = 0.009f;

    private float currentPos;
    private float leftPos = -224f;
    private float rightPos = 228f;

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

    private void Start()
    {
        rotatable = this.transform.GetChild(0);

        randomSwitch = (Random.value > 0.5f);

        currentScale = this.transform.localScale.x;
        currentPos = this.transform.localPosition.x;

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

        //Fills the rectangles array with the sprite components and assigns the colorArray colors to the obstacle
        arcs = new SpriteRenderer[this.transform.GetChild(0).childCount];

        for (int t = 0; t < arcs.Length; t++)
        {
            //give reference for the player ball to the collision script
            Transform arc = this.transform.GetChild(0).GetChild(t);
            arc.GetComponent<ObstacleCollision>().ball = ball;
            //
            arcs[t] = arc.GetComponent<SpriteRenderer>();
            arcs[t].color = colorArray[t];
        }
    }

    private void Update()
    {
        //rotation animation
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = rotatable.localRotation.eulerAngles.z;
        rotatable.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));

        if (randomSwitch)
        {
            //scaling up and down animation
            if (switching)
            {
                elapsedTime += Time.deltaTime * speed;
                currentScale = Mathf.Lerp(maxScale, minScale, 100f * elapsedTime);
                this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                if (currentScale <= minScale)
                {
                    switching = false;
                    elapsedTime = 0;
                }
            }

            else if (!switching)
            {
                elapsedTime += Time.deltaTime * speed;
                currentScale = Mathf.Lerp(minScale, maxScale, 100f * elapsedTime);
                this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

                if (currentScale >= maxScale)
                {
                    switching = true;
                    elapsedTime = 0;
                }
            }
        }
        else
        {
            //Moving left to right animation
            if (switching)
            {
                elapsedTime += Time.deltaTime * speed;
                currentPos = Mathf.Lerp(rightPos, leftPos, 100f * elapsedTime);
                this.transform.position = new Vector2(currentPos, this.transform.position.y);

                if (currentPos <= leftPos)
                {
                    switching = false;
                    elapsedTime = 0;
                }
            }

            else if (!switching)
            {
                elapsedTime += Time.deltaTime * speed;
                currentPos = Mathf.Lerp(leftPos, rightPos, 100f * elapsedTime);
                this.transform.position = new Vector2(currentPos, this.transform.position.y);

                if (currentPos >= rightPos)
                {
                    switching = true;
                    elapsedTime = 0;
                }
            }
        }
    }
}
