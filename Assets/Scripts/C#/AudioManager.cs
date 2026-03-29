using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;
    [Range(0f, 1f)] public float musicVolume = 1f;

    [Header("SFX")]
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeMusicSource();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
    }

    private void InitializeMusicSource()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // Creates a temporary AudioSource, plays the clip, then destroys it
    public void PlaySFX(AudioClip clip, string type)
    {
        float multi = 0f;
        switch (type)
        {
            case "Brick":
                multi = 0.75f;
                break;
            case "Ball":
                multi = 0.95f;
                break;
            case "Projectile":
                multi = 0.8f;
                break;
            case "Lose":
                multi = 1f;
                break;
            case "Victory":
                multi = 1f;
                break;
            case "Button":
                multi = 1.1f;
                break;
            case "Static":
                multi = 0.7f;
                break;
        }
        float volume = sfxVolume * multi;
        StartCoroutine(PlaySFXRoutine(clip, volume));
    }

    private IEnumerator PlaySFXRoutine(AudioClip clip, float volume)
    {
        GameObject tempGO = new GameObject("TempSFX");
        AudioSource sfxSource = tempGO.AddComponent<AudioSource>();
        sfxSource.clip = clip;
        sfxSource.volume = volume;
        sfxSource.Play();

        // Wait until clip finishes
        yield return new WaitForSeconds(clip.length);
        Destroy(tempGO);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (musicSource != null)
            musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}