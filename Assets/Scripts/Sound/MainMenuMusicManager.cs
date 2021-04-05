using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip TitleScreenMusic_Loop;

    private AudioSource audioSource;
    private bool doneIntro = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
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
            audioSource.clip = TitleScreenMusic_Loop;
            audioSource.Play();
        }
    }
}
