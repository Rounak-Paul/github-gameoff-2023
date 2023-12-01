using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Key : MonoBehaviour
{
    public static int keyCount = 0;

    [SerializeField] private float maxDistance;
    [SerializeField] private float lineWidth;
    [SerializeField] private Color lineBaseColor;
    [SerializeField] private Color lineCritColor;

    [SerializeField] private float distance;

    private GameObject player = null;
    private LineRenderer lineRenderer;
    private bool hasThisKey;

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    private void Update()
    {
        //Debug.Log("Key Count: " + keyCount);

        if (player == null) { return; }
        
        distance = Vector2.Distance(transform.position, player.transform.position);
        DrawLine(transform.position, player.transform.position);

        float distanceNormalized = distance / maxDistance;

        if (distanceNormalized <= 1)
        {
            //Debug.Log("Crit:" + distance);
            SetLineColor(new Color(distanceNormalized, 1f - distanceNormalized, 0f, 1f));
        }
        else
        {
            hasThisKey = false;
            keyCount--;
            player = null;
            DrawLine(transform.position, transform.position);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasThisKey) { return; }
        if (collision.gameObject.CompareTag("Player"))
        {
            hasThisKey = true;
            keyCount++;
            player = collision.gameObject;
        }
    }

    private void DrawLine(Vector3 pos1, Vector3 pos2)
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, pos1);
        lineRenderer.SetPosition(1, pos2);
    }

    private void SetLineColor(Color c)
    {
        lineRenderer.startColor = c;
        lineRenderer.endColor = c;
    }
}
