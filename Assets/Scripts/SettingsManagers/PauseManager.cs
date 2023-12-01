using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private List<int> nonPauseableSceneBuildIndexes;
    [SerializeField] private GameObject mainButtonSet;

    private bool canPause;
    public static event Action EventOnPause;
    public static event Action<bool> EventChangePauseState;

    private void OnEnable()
    {
        canPause = !nonPauseableSceneBuildIndexes.Contains(SceneManager.GetActiveScene().buildIndex);
        EventOnPause += Pause;
    }

    private void OnDisable()
    {
        EventOnPause = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            EventOnPause?.Invoke();
        }
    }
    
    public void Pause()
    {
        //Debug.Log("Pause Method Invoked!");
        EventChangePauseState?.Invoke(true);
        Time.timeScale = 0f;
        mainButtonSet.SetActive(true);
        EventOnPause -= Pause;
    }

    public void Resume()
    {
        //Debug.Log("Resume Method Invoked!");
        EventChangePauseState?.Invoke(false);
        Time.timeScale = 1f;
        mainButtonSet.SetActive(false);
        EventOnPause += Pause;
    }
}
