using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] buttonSets;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject exitPrompt;

    private int currentBtnIndex = 0;

    public static event Action<int> EventChangeScene;

    public void SwitchButtonSet(int index)
    {
        buttonSets[currentBtnIndex].SetActive(false);
        buttonSets[index].SetActive(true);
        currentBtnIndex = index;
    }

    public void ChangeScene(int index)
    {
        EventChangeScene?.Invoke(index);
    }

    public void RetryScene()
    {
        Time.timeScale = 1f;
        EventChangeScene?.Invoke(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitPrompt()
    {
        exitPrompt.SetActive(true);
        menu.SetActive(false);
    }

    public void Menu()
    {
        menu.SetActive(true);
        exitPrompt.SetActive(false);
    }
}
