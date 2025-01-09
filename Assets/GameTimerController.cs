using System.Collections;
using UnityEngine;
using TMPro;

public class GameTimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro text object
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip startSound; // Sound to play when the timer starts
    public AudioClip endSound; // Sound to play when the timer hits 0
    public GameObject musicSelectorDropDown; // Reference to the MusicSelectorDropDown object
    public GameObject startParticleSystemObject; // Reference to the GameObject with start ParticleSystems
    public GameObject stopParticleSystemObject; // Reference to the GameObject with stop ParticleSystems

    private MusicSelectorPlayer musicSelectorPlayer; // Reference to the MusicSelectorPlayer script
    private ParticleSystem[] startParticleSystems; // Array of start ParticleSystems on the GameObject
    private ParticleSystem[] stopParticleSystems; // Array of stop ParticleSystems on the GameObject
    private float currentTime = 0f; // Current time of the timer
    private bool isRunning = false; // Flag to check if the timer is running

    // Set timer to 0:00:00
    void Start()
    {
        // Set the timer to 30
        currentTime = 30f;
        UpdateTimerText();
        // Hide the timer
        timerText.gameObject.SetActive(!timerText.gameObject.activeSelf);

        // Find the MusicSelectorPlayer script
        musicSelectorPlayer = musicSelectorDropDown.GetComponent<MusicSelectorPlayer>();

        // Get all ParticleSystems on the start and stop GameObjects
        startParticleSystems = startParticleSystemObject.GetComponentsInChildren<ParticleSystem>();
        stopParticleSystems = stopParticleSystemObject.GetComponentsInChildren<ParticleSystem>();
        
        //Prewarm the particle systems
        //WarmParticleSystems();
    }

    private void WarmParticleSystems()
    {
        PlayParticleSystems(startParticleSystems); // Play start particle systems
        PlayParticleSystems(stopParticleSystems); // Play stop particle systems
    }

    void Update()
    {
        // Add 30 seconds to the timer
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentTime += 30f;
            UpdateTimerText();
        }

        // Reset the timer to 0:00:00
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentTime = 0f;
            isRunning = false;
            UpdateTimerText();
            musicSelectorPlayer.StopAudio(); // Stop the music
            PlayParticleSystems(stopParticleSystems); // Play stop particle systems
        }

        // Start the timer
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(StartTimerAfterSound());
        }

        // Pause the timer
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isRunning = false;
            musicSelectorPlayer.StopAudio(); // Stop the music
            PlayParticleSystems(stopParticleSystems); // Play stop particle systems
        }

        // Toggle timer visibility
        if (Input.GetKeyDown(KeyCode.T))
        {
            timerText.gameObject.SetActive(!timerText.gameObject.activeSelf);
        }

        // Update the timer if it is running
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                isRunning = false;
                audioSource.PlayOneShot(endSound); // Play end sound
                musicSelectorPlayer.StopAudio(); // Stop the music
                PlayParticleSystems(stopParticleSystems); // Play stop particle systems
            }
            UpdateTimerText();
        }
    }

    private IEnumerator StartTimerAfterSound()
    {
        audioSource.PlayOneShot(startSound); // Play start sound
        yield return new WaitForSeconds(startSound.length); // Wait for the sound to finish
        isRunning = true; // Start the timer
        musicSelectorPlayer.StartAudio(); // Start the music
        PlayParticleSystems(startParticleSystems); // Play start particle systems
    }

    private void UpdateTimerText()
    {
        // Format the time as minutes:seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void PlayParticleSystems(ParticleSystem[] particleSystems)
    {
        foreach (var ps in particleSystems)
        {
            ps.Play();
        }
    }
}