using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [Header("BGM")]
    public List<AudioClip> bgmList;
    public AudioClip currentClip;
    public AudioClip nextClip;
    public bool isPlaying;
    public bool haveNextClip;
    private int lastClipIndex;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Reference the AudioManager's musicSource
        if (AudioManager.Instance != null)
        {
            musicSource = AudioManager.Instance.musicSource;
        }
        else
        {
            Debug.LogError("AudioManager not found!");
        }
    }

    private void Start()
    {
        StartCoroutine(MusicLoop());
    }

    private IEnumerator MusicLoop()
    {
        while (true)
        {
            if (nextClip == null)
            {
                GetNextClip();
            }
            yield return new WaitUntil(() => haveNextClip);
            PlayNextClip();
            // Wait until the clip is playing
            yield return new WaitUntil(() => musicSource.isPlaying);
            nextClip = null;
            haveNextClip = false;
            // Wait until halfway through
            yield return new WaitUntil(() => musicSource.time >= musicSource.clip.length / 2);
            // Queue next clip
            GetNextClip();

            // Wait until current clip ends
            yield return new WaitUntil(() => !musicSource.isPlaying);
        }
    }

    private void PlayNextClip()
    {
        musicSource.Stop();
        currentClip = nextClip;
        lastClipIndex = bgmList.IndexOf(currentClip);
        musicSource.clip = currentClip;
        musicSource.Play();
        //AudioManager.Instance.PlayMusic(currentClip);
        isPlaying = true;
    }

    private void GetNextClip()
    {
        int index = GetRandomClipIndex();
        nextClip = bgmList[index];
        lastClipIndex = index;
        haveNextClip = true;
        // Auto-play next clip after current ends
        // (This is handled in the loop)
    }

    private int GetRandomClipIndex()
    {
        int index;
        do
        {
            index = Random.Range(0, bgmList.Count);
        } while (index == lastClipIndex && bgmList.Count > 1);
        return index;
    }
}