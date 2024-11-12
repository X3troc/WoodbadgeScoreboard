using UnityEngine;

public class SystemSettings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the target frame rate to 100
        Application.targetFrameRate = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Exit the game when the Shift + ESC key combination is hit
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}