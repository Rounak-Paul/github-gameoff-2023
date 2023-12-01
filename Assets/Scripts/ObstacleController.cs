using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private LayerMask handleLayerMask;
    [SerializeField] private List<string> handleTags;
    [SerializeField] private List<Material> materials;

    private ScalableObstacle currentObstacle;
    private MeshRenderer currentHandleMeshRenderer;
    private bool hasMouse = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MouseCheck() != -1)
            {
                ObstacleSelect();
            }
        }
        if (Input.GetMouseButton(0) && hasMouse)
        {
            currentObstacle.MouseHeld();
        }

        if (Input.GetMouseButtonUp(0) && hasMouse)
        {
            ObstacleDeselect();
            
        }
    }

    private int MouseCheck()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, handleLayerMask))
        {
            //Debug.Log("Hit " + hit.collider.name.ToString());
            if (!handleTags.Contains(hit.collider.gameObject.tag)) { return -1; }
            currentObstacle = hit.collider.gameObject.GetComponentInParent<ScalableObstacle>();
            currentHandleMeshRenderer = hit.collider.gameObject.GetComponent<MeshRenderer>();
            hasMouse = true;
            if (hit.collider.gameObject.CompareTag("HandleX"))
            {
                currentObstacle.mouseCheck = 0;
                return 0; 
            }
            currentObstacle.mouseCheck = 1;
            return 1;
        }
        return -1;
    }

    private void ObstacleSelect()
    {
        currentObstacle.MouseDown();
        currentHandleMeshRenderer.material = materials[1];
    }

    private void ObstacleDeselect()
    {
        currentObstacle.MouseUp();
        currentHandleMeshRenderer.material = materials[0];
        hasMouse = false;
    }
}
