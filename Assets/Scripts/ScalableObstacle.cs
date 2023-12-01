using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableObstacle : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private GameObject shadow;

    [Header("Stats")]
    [SerializeField] private float velocityMultiplier = 10f;
    [SerializeField] private float scaleMultiplier = 1f;

    private Rigidbody rb;
    private GameObject lightSource;
    private Vector3 mousePosition;

    private float shadowToLSDist, height;
    private float scalingFactor, scaleAdjustment;

    [HideInInspector] public int mouseCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lightSource = GameObject.Find("LightSource");
        shadowToLSDist = Mathf.Abs(shadow.transform.position.z - lightSource.transform.position.z);
        height = transform.localScale.y;
        scalingFactor = ScalingFactorCalc();
        scaleAdjustment = -scalingFactor;
    }

    private float ScalingFactorCalc()
    {
        return ((shadowToLSDist * height) / (Mathf.Abs(transform.position.z - lightSource.transform.position.z)));
    }

    public void MouseDown()
    {
        if (mouseCheck == -1) { return; }

        rb.isKinematic = false;

        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }

    public void MouseHeld()
    {
        Vector3 currentMousePos;
        if (mouseCheck == 0)
        {
            currentMousePos = new(Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).x,
                                                                 transform.position.y,
                                                                 transform.position.z);

            rb.velocity = (currentMousePos - transform.position) * velocityMultiplier;
            return;
        }
        currentMousePos = new(transform.position.x,
                              transform.position.y,
                              Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).z);
        rb.velocity = (currentMousePos - transform.position) * velocityMultiplier;

        shadow.transform.SetPositionAndRotation(new(shadow.transform.position.x,
                                                    shadow.transform.position.y, 
                                                    0.0f), Quaternion.identity);

        scalingFactor = ScalingFactorCalc();

        shadow.transform.localScale = (1 + (scaleMultiplier * (scalingFactor + scaleAdjustment))) * Vector3.one;

    }

    public void MouseUp()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
