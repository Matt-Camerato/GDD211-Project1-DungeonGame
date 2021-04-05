using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameplayMusic_loop;
    [SerializeField] private AudioSource winAudio;

    private AudioSource audioSource;

    private bool doneIntro = false;
    private bool fadedIn = false;
    private bool turnedDownForWin = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fadedIn)
        {
            audioSource.volume += Time.deltaTime * 0.02f;
            if(audioSource.volume > 0.25f)
            {
                fadedIn = true;
            }
        }

        if (!doneIntro)
        {
            if (!audioSource.isPlaying)
            {
                doneIntro = true;
            }
        }
        else if (!audioSource.loop)
        {
            audioSource.loop = true;
            audioSource.clip = gameplayMusic_loop;
            audioSource.Play();
        }

        if (!turnedDownForWin)
        {
            if (winAudio.isPlaying)
            {
                audioSource.volume = 0.1f;
                turnedDownForWin = true;
            }
        }
        else
        {
            if(audioSource.volume < 0.25f)
            {
                audioSource.volume += Time.deltaTime * 0.02f;
            }
        }
            
    }
}
