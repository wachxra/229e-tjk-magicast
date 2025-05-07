using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioSource bgmAudio;

    [Header("Sound Effects")]
    public AudioSource walkAudio;
    public AudioSource jumpAudio;
    public AudioSource playerHittingAudio;
    public AudioSource hurtAudio;
    public AudioSource pickupItemAudio;
    public AudioSource bloodPotionAudio;
    public AudioSource manaPotionAudio;
    public AudioSource doorAudio;
    public AudioSource bossAudio;
    public AudioSource water;
    public AudioSource die;

    // BGM Controls
    public void PlayBGM()
    {
        if (!bgmAudio.isPlaying)
            bgmAudio.Play();
    }

    public void StopBGM()
    {
        if (bgmAudio.isPlaying)
            bgmAudio.Stop();
    }

    public void ChangeBGM(AudioClip newClip, bool loop = true)
    {
        bgmAudio.clip = newClip;
        bgmAudio.loop = loop;
        bgmAudio.Play();
    }

    // SFX Controls
    public void PlayWalk() => walkAudio.Play();
    public void PlayJump() => jumpAudio.Play();
    public void PlayHit() => playerHittingAudio.Play();
    public void PlayHurt() => hurtAudio.Play();
    public void PlayItem() => pickupItemAudio.Play();
    public void PlayPotion() => bloodPotionAudio.Play();
    public void PlayManaPotion() => manaPotionAudio.Play();
    public void PlayDoor() => doorAudio.Play();
    public void PlayBoss() => bossAudio.Play();
    public void PlayWater() => water.Play();
    public void PlayDie() => die.Play();
}