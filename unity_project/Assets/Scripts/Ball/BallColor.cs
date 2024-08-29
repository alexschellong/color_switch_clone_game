using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour
{
    private Color yellow = new Color(230 / 255f, 194 / 255f, 41 / 255f);
    private Color orange = new Color(241 / 255f, 113 / 255f, 5 / 255f);
    private Color red = new Color(209 / 255f, 17 / 255f, 73 / 255f);
    private Color blue = new Color(102 / 255f, 16 / 255f, 242 / 255f);

    private Color[] colorArray;
    private int colorNumber;

    void Start()
    {
        colorArray = new Color[] { yellow, orange, red, blue };
        colorNumber = Random.Range(0, 4);
        this.gameObject.GetComponent<SpriteRenderer>().color = colorArray[colorNumber];
    }

    public void changeColor()
    {
        //finds the previously used index number for colors and swaps it with the last index number.
        //The color is then randomly selected from the remaining index numbers.
        int[] numberArray = new int[] { 0, 1, 2, 3 };
        for (int i = 0; i < numberArray.Length; i++)
        {
            if (numberArray[i] == colorNumber && i < numberArray.Length - 1)
            {
                int lastNumber = numberArray[3];
                numberArray[3] = numberArray[i];
                numberArray[i] = lastNumber;
            }
        }

        colorNumber = numberArray[Random.Range(0, 3)];
        this.gameObject.GetComponent<SpriteRenderer>().color = colorArray[colorNumber];
    }
}
