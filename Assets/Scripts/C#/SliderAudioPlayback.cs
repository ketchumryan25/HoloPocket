using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderAudioPlayback : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the UI Slider
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip clip; // The audio clip to play

    public void Initialize()
    {
        // Ensure the slider range is 0 to 1
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

        // Assign the clip if not already assigned
        if (clip != null)
        {
            audioSource.clip = clip;
        }

        // Register the event listener
        volumeSlider.onValueChanged.AddListener(OnSliderValueChange);
    }

    void OnSliderValueChange(float sliderValue)
    {
        // Calculate the time based on slider value (0.0 to 1.0)
        float playbackTime = sliderValue * audioSource.clip.length;
        
        // Set the audio source time to the calculated value
        audioSource.time = playbackTime;
        
        // Play the audio if it isn't already playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnDisable()
    {
        // Clean up event listener
        volumeSlider.onValueChanged.RemoveListener(OnSliderValueChange);
    }
}   