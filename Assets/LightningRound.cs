using UnityEngine;

public class LightningRound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public ParticleSystem particleSystem; // Reference to the ParticleSystem component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        // Get the ParticleSystem component from the child object
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Play the AudioSource as a one-shot and the ParticleSystem when the L key is pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            audioSource.PlayOneShot(audioSource.clip);
            particleSystem.Play();
        }
    }
}