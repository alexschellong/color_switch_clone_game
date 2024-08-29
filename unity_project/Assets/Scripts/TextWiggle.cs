using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TextWiggle : MonoBehaviour
{
    private Transform text;

    private bool switchingRot = true;
    private float currentRot;
    public float maxRot;
    private float elapsedTimeRot = 0;
    public float speedRot;

    private bool switchingScale = true;
    private float currentScale;
    public float maxScale;
    public float minScale;
    private float elapsedTimeScale = 0;
    public float speedScale;

    private void Start()
    {
        text = this.transform;
        currentScale = maxScale;
        currentRot = text.transform.localRotation.z;
    }

    //rotation animation
    private void rotate()
    {
        if (switchingRot)
        {
            elapsedTimeRot += Time.deltaTime * speedRot;
            currentRot = Mathf.Lerp(maxRot, -maxRot, 100f * elapsedTimeRot);
            text.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRot));

            if (currentRot <= -maxRot)
            {
                switchingRot = false;
                elapsedTimeRot = 0;
            }
        }
        else if (!switchingRot)
        {
            elapsedTimeRot += Time.deltaTime * speedRot;
            currentRot = Mathf.Lerp(-maxRot, maxRot, 100f * elapsedTimeRot);
            text.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, currentRot));

            if (currentRot >= maxRot)
            {
                switchingRot = true;
                elapsedTimeRot = 0;
            }
        }
    }

    //scaling up and down animation
    private void scale()
    {
        if (switchingScale)
        {
            elapsedTimeScale += Time.deltaTime * speedScale;
            currentScale = Mathf.Lerp(maxScale, minScale, 100f * elapsedTimeScale);
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (currentScale <= minScale)
            {
                switchingScale = false;
                elapsedTimeScale = 0;
            }
        }
        else if (!switchingScale)
        {
            elapsedTimeScale += Time.deltaTime * speedScale;
            currentScale = Mathf.Lerp(minScale, maxScale, 100f * elapsedTimeScale);
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (currentScale >= maxScale)
            {
                switchingScale = true;
                elapsedTimeScale = 0;
            }
        }
    }

    private void Update()
    {
        scale();
        rotate();
    }
}
