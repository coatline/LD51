using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject settings;

    public void ChangeScene(string name) => SceneManager.LoadScene(name);

    public void DisplaySettings()
    {
        settings.SetActive(true);
        title.SetActive(false);
    }

    public void DisplayTitle()
    {
        settings.SetActive(false);
        title.SetActive(true);
    }
}
