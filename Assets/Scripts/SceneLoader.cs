using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int lastLevelBuildIndex;

    private int curBuildIndex;

    private void OnEnable()
    {
        curBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SettingsManager.EventChangeScene += Loader;
        ExitPoint.EventLevelCleared += NextScene;
    }

    private void OnDisable()
    {
        SettingsManager.EventChangeScene -= Loader;
        ExitPoint.EventLevelCleared -= NextScene;
    }

    public void Loader(int index)
    {
        if (index < 0)
        {
            Quit();
            return;
        } 
        SceneManager.LoadScene(index);
    }

    public void NextScene()
    {
        if (curBuildIndex < lastLevelBuildIndex)
        {
            Loader(curBuildIndex+1);
            return;
        }
        Loader(1);
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
