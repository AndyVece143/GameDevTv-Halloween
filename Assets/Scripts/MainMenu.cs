using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LevelLoader loader;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        loader.LoadNextLevel("Cutscene");
    }

    public void CreditsButton()
    {
        loader.LoadNextLevel("Credits");
    }

    public void TitleButton()
    {
        loader.LoadNextLevel("Title");
    }
}
