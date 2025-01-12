using UnityEngine;

public class WinController : MonoBehaviour
{
    public AudioClip audioClip; // Reference to the audio file
    private AudioSource audioSource; // Reference to the AudioSource component
    private bool isPlaying = false; // Flag to check if audio is playing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip; // Assign the audio clip to the audio source
    }

    // Update is called once per frame
    void Update()
    {
        // Play or stop and rewind the audio clip when the W key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isPlaying)
            {
                // Stop and rewind the sound if W is pressed again
                audioSource.Stop();
                audioSource.time = 0f;
                isPlaying = false;
            }
            else
            {
                // Play the audio clip
                audioSource.Play();
                isPlaying = true;
            }
        }
    }
}