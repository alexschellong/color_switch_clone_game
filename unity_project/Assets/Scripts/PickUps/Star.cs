using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Star : MonoBehaviour
{
    public ParticleSystem starParticles;
    public AudioClip starSound;
    public GameObject audioSource;

    public TextMeshProUGUI scoreTxtInGame;
    public ScoreCounter score;

    private bool downScale = true;
    private float currentScale;
    private float maxScale = 29.45f;
    private float minScale = 18.6f;
    private float elapsedTime = 0;
    private float speed = 0.01f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 3)
        {
            //increases the player's score upon pickup on screen
            score.score += 1;
            scoreTxtInGame.text = score.score.ToString();

            Instantiate(starParticles, this.transform.position, this.transform.rotation);

            audioSource.GetComponent<AudioSource>().clip = starSound;
            Instantiate(audioSource);

            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentScale = this.transform.localScale.x;
    }

    private void Update()
    {
        //Scaling animation
        if (downScale)
        {
            elapsedTime += Time.deltaTime * speed;
            currentScale = Mathf.Lerp(maxScale, minScale, 100f * elapsedTime);
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (currentScale <= minScale)
            {
                downScale = false;
                elapsedTime = 0;
            }
        }

        else if (!downScale)
        {
            elapsedTime += Time.deltaTime * speed;
            currentScale = Mathf.Lerp(minScale, maxScale, 100f * elapsedTime);
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

            if (currentScale >= maxScale)
            {
                downScale = true;
                elapsedTime = 0;
            }
        }
    }
}
