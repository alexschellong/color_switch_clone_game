using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Obstacle1 : MonoBehaviour
{
    public GameObject ball;
    private SpriteRenderer[] rectangles;
    private Color[] colorArray;

    private Color yellow = new Color(230 / 255f, 194 / 255f, 41 / 255f);
    private Color orange = new Color(241 / 255f, 113 / 255f, 5 / 255f);
    private Color red = new Color(209 / 255f, 17 / 255f, 73 / 255f);
    private Color blue = new Color(102 / 255f, 16 / 255f, 242 / 255f);

    //randomly shuffles elements in the array
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
        rectangles = new SpriteRenderer[this.transform.childCount - 2];

        for (int t = 0; t < rectangles.Length; t++)
        {
            //give reference for the player ball to the collision script
            Transform rect = this.transform.GetChild(t);
            rect.GetComponent<ObstacleCollision>().ball = ball;

            rectangles[t] = rect.GetComponent<SpriteRenderer>();
            rectangles[t].color = colorArray[t];
        }

        //Starts a recursive coroutine that shifts the rectangle colors every second to the right
        StartCoroutine(changeColor());
    }

    private IEnumerator changeColor()
    {
        yield return new WaitForSeconds(1);

        Color lastElement = colorArray[colorArray.Length - 1];

        for (int t = colorArray.Length - 1; t > 0; t--)
        {
            colorArray[t] = colorArray[t - 1];
        }

        colorArray[0] = lastElement;

        for (int t = 0; t < rectangles.Length; t++)
        {
            rectangles[t].color = colorArray[t];
        }

        StartCoroutine(changeColor());
    }
}
