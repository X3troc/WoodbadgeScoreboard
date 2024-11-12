using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the Slider component
    [SerializeField] AudioSource audioSource; // Reference to the AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider = GetComponent<Slider>(); // Get the Slider component
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume); // Add listener for slider value change
            volumeSlider.value = audioSource.volume; // Set slider value to current audio source volume
        }
    }

    // Method to set the volume based on the slider value
    void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}