using UnityEngine;

public class ScaleRelativeToCamera : MonoBehaviour
{
    public GameObject[] objectsToScale; // Array of GameObjects to scale
    public float baseOrthographicSize = 5f; // Base orthographic size for scaling

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float scaleFactor = mainCamera.orthographicSize / baseOrthographicSize;

        foreach (GameObject obj in objectsToScale)
        {
            obj.transform.localScale = new Vector3(scaleFactor, scaleFactor, obj.transform.localScale.z);
            //obj.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor / 2);
        }
    }
}