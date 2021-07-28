using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{

    public Button[] levelButtons;
    private float lastUpdate = 0f;

    public void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        Debug.Log("Curret Unlock Levels:" + levelReached.ToString());
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }
    }

    public void Update()
    {
        if (Time.realtimeSinceStartup < lastUpdate + 1f)
        {
            return;
        }
        else
        {
            lastUpdate = Time.realtimeSinceStartup;
        }

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        Debug.Log("Curret Unlock Levels:" + levelReached.ToString());
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }
    }
    public void Select(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
