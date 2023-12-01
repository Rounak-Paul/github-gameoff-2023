using System;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] LayerMask sideLayerMask;
    [SerializeField] LayerMask topLayerMask;
    
    private float layerMaskChangeDelay;
    private bool canSwitch = true;
    private bool isPaused = false;

    UnityEvent switchCam;

    private void OnEnable()
    {
        PauseManager.EventChangePauseState += PauseState;
    }
    private void OnDisable()
    {
        PauseManager.EventChangePauseState -= PauseState;
    }

    private void Start()
    {
        SwitchCamInit();
        layerMaskChangeDelay = Camera.main.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isPaused)
        {
            switchCam.Invoke();
        }
    }

    private void PauseState(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    private void SwitchCamInit()
    {
        if (switchCam == null)
        {
            switchCam = new UnityEvent();
        }
        switchCam.RemoveAllListeners();
        switchCam.AddListener(SwitchToSideCam);
        switchCam.Invoke();
    }

    private void SwitchToSideCam()
    {
        if (!canSwitch) { return; }
        playerRb.drag = 1f;
        cameras[1].Priority = 0;
        StartCoroutine(SwitchCullingMask(sideLayerMask, layerMaskChangeDelay));
        switchCam.RemoveListener(SwitchToSideCam);
        switchCam.AddListener(SwitchToTopCam);
    }

    private void SwitchToTopCam()
    {
        if (!canSwitch) { return; }
        playerRb.drag = 9000f;
        cameras[1].Priority = 2;
        Camera.main.cullingMask = topLayerMask;
        StartCoroutine(SwitchCullingMask(topLayerMask, 0f, layerMaskChangeDelay));
        switchCam.RemoveListener(SwitchToTopCam);
        switchCam.AddListener(SwitchToSideCam);
    }

    private IEnumerator SwitchCullingMask(LayerMask layerMask, float switchDelay, float layerMaskDelay = 0f)
    {
        canSwitch = false;
        yield return new WaitForSecondsRealtime(layerMaskDelay);
        Camera.main.cullingMask = layerMask;
        yield return new WaitForSecondsRealtime(switchDelay);
        canSwitch = true;
    }
}
