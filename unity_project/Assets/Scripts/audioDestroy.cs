using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioDestroy : MonoBehaviour
{
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
