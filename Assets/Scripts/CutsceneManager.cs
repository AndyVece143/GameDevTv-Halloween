using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public TMP_Text text;
    private int clicks;
    public SpriteRenderer image;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = "Ugh... where am I?";
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCutscene();

        if (Input.GetMouseButtonDown(0))
        {
            clicks++;
            Debug.Log(clicks);
        }

        if (clicks == 10)
        {
            loader.LoadNextLevel("Level1");
        }
    }

    void UpdateCutscene()
    {
        switch (clicks)
        {
            case 0:
                text.text = "Ugh... where am I?";
                break;

            case 1:
                text.text = "Is that... an old TV?";
                break;

            case 2:
                text.text = "I don't know why, but something tells me to get closer to it...";
                break;
            case 3:
                StartCoroutine(ImageScaling(1.5f, 1));
                clicks++;
                break;

            case 4:
                text.text = "Wow... sure is an old TV... not surprising there...";
                break;

            case 5:
                text.text = "It feels like I could fall into the TV if I'm not careful.";
                break;

            case 6:
                text.text = "Oh no! I'm somehow losing my footing!";
                break;

            case 7:
                StartCoroutine(ImageScaling(1.5f, 1));
                clicks++;
                break;

            case 8:
                text.text = "Noooooooooooo!!!";
                break;

            case 9:
                StartCoroutine(ImageScaling(1.5f, 2));
                clicks++;
                break;

        }
    }

    IEnumerator ImageScaling(float targetScale, float duration)
    {
        Vector3 initialScale = image.transform.localScale;
        Vector3 finalScale = initialScale * targetScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            image.transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        image.transform.localScale = finalScale;
    }
}
