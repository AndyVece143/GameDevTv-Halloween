using TMPro;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public TMP_Text text;
    private int clicks;
    public SpriteRenderer image;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

        if (clicks == 5)
        {
            loader.LoadNextLevel("Title");
        }
    }

    void UpdateCutscene()
    {
        switch(clicks)
        {
            case 0:
                text.text = "Ugh... I'm back?";
                break;

            case 1:
                text.text = "What a strange adventure...";
                break;

            case 2:
                text.text = "Can't believe I fell into this old TV.";
                break;

            case 3:
                text.text = "That's it! I'm never setting foot near an old TV ever again!";
                break;

            case 4:
                text.text = "Flatscreens for life!";
                break;
        }
    }
}
