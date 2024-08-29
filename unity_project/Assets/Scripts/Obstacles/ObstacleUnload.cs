using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUnload : MonoBehaviour
{
    public Transform upperBound;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //destroys the obstacle once its upper bound is below the camera
        Vector2 viewPos = mainCamera.WorldToViewportPoint(upperBound.transform.position);
        if (viewPos.y < 0.0F)
        {
            Destroy(upperBound.parent.gameObject);
        }
    }
}
