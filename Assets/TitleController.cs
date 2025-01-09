using UnityEngine;
using TMPro;

public class TitleController : MonoBehaviour
{
    public TextMeshProUGUI titleText; // Reference to the TextMeshPro text object
    public AudioSource audioSource; // Reference to the AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle visibility of the TextMeshPro object and play sound when the Y key is pressed
        if (Input.GetKeyDown(KeyCode.Y))
        {
            titleText.gameObject.SetActive(!titleText.gameObject.activeSelf);
            //audioSource.PlayOneShot(audioSource.clip);
        }
    }
}