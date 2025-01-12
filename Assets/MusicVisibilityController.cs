using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class MusicVisibilityController : MonoBehaviour
{
    public Slider volumeSlider; // Reference to the Slider component
    public TextMeshProUGUI musicTimerText; // Reference to the TextMeshPro component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool isActive = volumeSlider.gameObject.activeSelf;
        volumeSlider.gameObject.SetActive(!isActive);
        musicTimerText.gameObject.SetActive(!isActive);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle visibility of the TextMeshPro component and volume slider when the V key is pressed
        if (Input.GetKeyDown(KeyCode.V))
        {
            bool isActive = volumeSlider.gameObject.activeSelf;
            volumeSlider.gameObject.SetActive(!isActive);
            musicTimerText.gameObject.SetActive(!isActive);
        }
    }
}