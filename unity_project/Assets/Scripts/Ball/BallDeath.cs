using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallDeath : MonoBehaviour
{
    public ParticleSystem deathParticles;
    public AudioClip deathSound;
    public GameObject audioSource;
    public GameOver _gameOver;

    public TextMeshProUGUI scoreTxtFinal;
    public TextMeshProUGUI scoreTxtInGame;

    public GameObject mainCamera;

    public void ballDeath()
    {
        //forwards the ingame score to the gameover interface
        scoreTxtFinal.text = scoreTxtInGame.text;

        Instantiate(deathParticles, this.transform.position, this.transform.rotation);
        audioSource.GetComponent<AudioSource>().clip = deathSound;
        Instantiate(audioSource);

        mainCamera.GetComponent<CameraMove>().enabled = false;
        mainCamera.transform.parent = null;

        _gameOver.enabled = true;
        _gameOver.GameOverFunc();

        gameObject.SetActive(false);
    }
}
