using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public GameObject[] objectsToCheck; // Array of GameObjects to check
    public Image[] imagesToFade; // Array of Images to fade
    public float fadeDuration = 0.5f; // Duration of the fade animation
    public float fadeAmount = 0.5f; // Target alpha value for fading
    public float lowerViewPercentage = 0.1f; // Percentage of the lower part of the view to check

    private Camera mainCamera;
    private Dictionary<GameObject, CanvasGroup> objectImageMap;
    private Dictionary<CanvasGroup, Coroutine> fadeCoroutines;

    void Start()
    {
        mainCamera = Camera.main;
        objectImageMap = new Dictionary<GameObject, CanvasGroup>();
        fadeCoroutines = new Dictionary<CanvasGroup, Coroutine>();

        for (int i = 0; i < objectsToCheck.Length; i++)
        {
            if (i < imagesToFade.Length)
            {
                CanvasGroup canvasGroup = imagesToFade[i].gameObject.AddComponent<CanvasGroup>();
                objectImageMap.Add(objectsToCheck[i], canvasGroup);
                fadeCoroutines.Add(canvasGroup, null);
            }
        }
    }

    void Update()
    {
        foreach (var entry in objectImageMap)
        {
            GameObject obj = entry.Key;
            CanvasGroup canvasGroup = entry.Value;

            Vector3 viewPos = mainCamera.WorldToViewportPoint(obj.transform.position);
            if (viewPos.y < lowerViewPercentage)
            {
                if (fadeCoroutines[canvasGroup] != null)
                {
                    StopCoroutine(fadeCoroutines[canvasGroup]);
                }
                fadeCoroutines[canvasGroup] = StartCoroutine(FadeOut(canvasGroup));
            }
            else
            {
                if (fadeCoroutines[canvasGroup] != null)
                {
                    StopCoroutine(fadeCoroutines[canvasGroup]);
                }
                fadeCoroutines[canvasGroup] = StartCoroutine(FadeIn(canvasGroup));
            }
        }
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, fadeAmount, normalizedTime);
            yield return null;
        }
        canvasGroup.alpha = fadeAmount;
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeDuration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, normalizedTime);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}