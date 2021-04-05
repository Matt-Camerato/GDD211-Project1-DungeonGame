using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private AudioClip Damage;
    [SerializeField] private AudioClip PlayerDeath;
    [SerializeField] private AudioClip EnemyKilled;
    [SerializeField] private AudioClip Lever;
    [SerializeField] private AudioClip Button;
    [SerializeField] private AudioClip PressurePlate;
    [SerializeField] private AudioClip DoorsOpen;
    [SerializeField] private AudioClip KeyCollect;
    [SerializeField] private AudioClip WizardShoot;

    public static SoundEffectManager instance;
    private AudioSource audioSource;
    private AudioSource winAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
        winAudio = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void DamageSFX() => audioSource.PlayOneShot(Damage, 0.4f);
    public void PlayerDeathSFX() => audioSource.PlayOneShot(PlayerDeath, 0.3f);
    public void EnemyKilledSFX() => audioSource.PlayOneShot(EnemyKilled, 0.3f);
    public void LeverSFX() => audioSource.PlayOneShot(Lever, 0.3f);
    public void ButtonSFX() => audioSource.PlayOneShot(Button, 0.4f);
    public void PressurePlateSFX() => audioSource.PlayOneShot(PressurePlate, 0.3f);
    public void DoorsOpenSFX() => audioSource.PlayOneShot(DoorsOpen, 0.2f);
    public void KeyCollectSFX() => audioSource.PlayOneShot(KeyCollect, 0.3f);
    public void WizardShootSFX() => audioSource.PlayOneShot(WizardShoot, 0.3f);
    public void WinSFX() => winAudio.Play();

}
