using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ColorSwitcher : MonoBehaviour
{
    public AudioClip switchSound;
    public GameObject audioSource;

    private float degreesPerSec = 540f;

    public BallColor _ballColor;
    private void Update()
    {
        //rotation
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 3)
        {
            _ballColor.changeColor();
            audioSource.GetComponent<AudioSource>().clip = switchSound;
            Instantiate(audioSource);
            Destroy(this.gameObject);
        }
    }

}
