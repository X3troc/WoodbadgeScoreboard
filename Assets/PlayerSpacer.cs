using UnityEngine;
using System.Collections;

public class HorizontalDistributor : MonoBehaviour
{
    public GameObject[] objectsToDistribute; // Array of GameObjects to distribute
    public float spacingDuration = 1f; // Duration for the spacing animation
    public float FrequencyTime = 2f; // Frequency time for recalculating and moving objects

    private Camera mainCamera;
    private Vector3[] targetPositions;

    void Start()
    {
        mainCamera = Camera.main;
        targetPositions = new Vector3[objectsToDistribute.Length];
        StartCoroutine(RecalculateAndMoveObjects());
    }

    IEnumerator RecalculateAndMoveObjects()
    {
        while (true)
        {
            CalculateTargetPositions();
            yield return new WaitForSeconds(FrequencyTime);
        }
    }

    void Update()
    {
        for (int i = 0; i < objectsToDistribute.Length; i++)
        {
            Vector3 currentPosition = objectsToDistribute[i].transform.position;
            Vector3 targetPosition = targetPositions[i];
            objectsToDistribute[i].transform.position = new Vector3(
                Mathf.Lerp(currentPosition.x, targetPosition.x, Time.deltaTime / spacingDuration),
                currentPosition.y,
                currentPosition.z
            );
        }
    }

    void CalculateTargetPositions()
    {
        float screenWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        float startX = mainCamera.transform.position.x - screenWidth / 2f;
        float spacing = screenWidth / (objectsToDistribute.Length + 1);

        for (int i = 0; i < objectsToDistribute.Length; i++)
        {
            float targetX = startX + spacing * (i + 1);
            targetPositions[i] = new Vector3(targetX, objectsToDistribute[i].transform.position.y, objectsToDistribute[i].transform.position.z);
        }
    }
}