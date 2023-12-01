using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleZ : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float scaleMultiplier = 1f;

    [Header("Assignables")]
    [SerializeField] private GameObject mainBody;
    [SerializeField] private GameObject shadowBody;

    private GameObject lightSource;
    private Vector3 mousePosition;
    private float shadowToLSDist, mainBodyHeight; // LS = LightSource Gameobject
    private float scalingFactor, scaleAdjustment;

    private void Start()
    {
        lightSource = GameObject.Find("LightSource");
        shadowToLSDist = Vector3.Distance(shadowBody.transform.position, lightSource.transform.position);
        mainBodyHeight = mainBody.transform.localScale.y;
        scalingFactor = ScalingFactorCalc();
        scaleAdjustment = -scalingFactor;
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(mainBody.transform.position);
    }

    private void OnMouseDrag()
    {
        DragZ();
    }

    private void DragZ()
    {
        mainBody.transform.position = new Vector3(mainBody.transform.position.x,
                                                  mainBody.transform.position.y,
                                                  Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).z);

        scalingFactor = ScalingFactorCalc();

        shadowBody.transform.localScale = (mainBody.transform.localScale) + (scaleMultiplier * (scalingFactor + scaleAdjustment) * Vector3.one);
    }

    private float ScalingFactorCalc()
    {
        return ((shadowToLSDist * mainBodyHeight) / (Mathf.Abs(mainBody.transform.position.z - lightSource.transform.position.z)));
    }
}
