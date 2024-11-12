using System.Collections;
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
    private CanvasGroup[] imageCanvasGroups;

    void Start()
    {
        mainCamera = Camera.main;
        imageCanvasGroups = new CanvasGroup[imagesToFade.Length];

        for (int i = 0; i < imagesToFade.Length; i++)
        {
            imageCanvasGroups[i] = imagesToFade[i].gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        bool shouldFade = false;

        foreach (GameObject obj in objectsToCheck)
        {
            Vector3 viewPos = mainCamera.WorldToViewportPoint(obj.transform.position);
            if (viewPos.y < lowerViewPercentage)
            {
                shouldFade = true;
                break;
            }
        }

        for (int i = 0; i < imagesToFade.Length; i++)
        {
            if (shouldFade)
            {
                StartCoroutine(FadeOut(imageCanvasGroups[i]));
            }
            else
            {
                StartCoroutine(FadeIn(imageCanvasGroups[i]));
            }
        }
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float startAlpha = canvasGroup.alpha;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, fadeAmount, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = fadeAmount;
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float startAlpha = canvasGroup.alpha;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}