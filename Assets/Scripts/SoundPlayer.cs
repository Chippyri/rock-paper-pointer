using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    public AudioClip winSound;

    [SerializeField]
    public AudioClip loseSound;

    [SerializeField]
    public AudioClip tieSound;

    [SerializeField]
    public AudioClip gameLoseSound;

    [SerializeField]
    public AudioClip gameWinSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRoundWinSound() {
        audioSource.PlayOneShot(winSound, 0.7f);
    }

    public void PlayRoundLoseSound() {
        audioSource.PlayOneShot(loseSound, 0.7f);
    }

    public void PlayRoundTieSound() {
        audioSource.PlayOneShot(tieSound, 0.7f);
    }

    public void PlayGameWinSound() {
        audioSource.PlayOneShot(gameWinSound, 0.7f);
    }

    public void PlayGameLoseSound()
    {
        audioSource.PlayOneShot(gameLoseSound, 0.7f);
    }
}
