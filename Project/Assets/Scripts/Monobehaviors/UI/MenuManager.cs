using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] Image transitioner;
    [SerializeField] GameObject settings;

    public void ChangeScene(string name) => SceneManager.LoadScene(name);

    public void DisplaySettings()
    {
        settings.SetActive(true);
        title.SetActive(false);
    }

    IEnumerator DoFade(float speed)
    {
        while (transitioner.color.a < 1)
        {
            transitioner.color = new Color(transitioner.color.r, transitioner.color.g, transitioner.color.b, transitioner.color.a + Time.deltaTime * speed);
        }

        //while (transitioner.color.a >= 1)
        //{
        //    transitioner.color = new Color(transitioner.color.r, transitioner.color.g, transitioner.color.b, transitioner.color.a - Time.deltaTime * speed);
        //}

        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Game");
    }

    public void ChangeToGameScene()
    {
        StartCoroutine(DoFade(1));
    }

    public void DisplayTitle()
    {
        settings.SetActive(false);
        title.SetActive(true);
    }
}
