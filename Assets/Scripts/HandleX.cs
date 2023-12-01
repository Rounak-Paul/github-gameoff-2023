using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleX : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private GameObject shadowBody;
    [SerializeField] private GameObject cubeBody;
    [SerializeField] private LayerMask clickableLayerMask;
    [SerializeField] private Material material;

    [Header("Stats")]
    [SerializeField] private float velocityMultiplier = 10f;

    private bool hasMouse = false;
    private Vector3 mousePosition;
    private Rigidbody cubeRb;

    private void Start()
    {
        cubeRb = cubeBody.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButton(0) && hasMouse)
        {
            MouseHeld();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    private void MouseUp()
    {
        material.SetColor("_HandleColor", Color.white);

        cubeRb.velocity = Vector3.zero;
        cubeRb.gameObject.isStatic = true;
        cubeRb.isKinematic = true;

        hasMouse = false;
    }

    private void MouseHeld()
    {
        Vector3 currentMousePos = new(Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition).x,
                                                                     cubeBody.transform.position.y,
                                                                     cubeBody.transform.position.z);

        cubeRb.velocity = (currentMousePos - cubeBody.transform.position) * velocityMultiplier;

        shadowBody.transform.SetPositionAndRotation(new(cubeBody.transform.position.x,
                                                        shadowBody.transform.position.y,
                                                        shadowBody.transform.position.z), Quaternion.identity);
    }

    private void MouseDown()
    {
        if (!MouseCheck()) { return; }

        cubeRb.gameObject.isStatic = false;
        cubeRb.isKinematic = false;

        material.SetColor("_HandleColor", Color.red);

        mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(cubeBody.transform.position);
    }

    private bool MouseCheck()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, clickableLayerMask))
        {
            if (!hit.collider.gameObject == gameObject) { return false; }
            hasMouse = true;
            return hasMouse;
        }
        return false;
    }
}
