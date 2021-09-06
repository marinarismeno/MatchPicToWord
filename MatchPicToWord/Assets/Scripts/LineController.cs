using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    //public GameObject linePrefab;
    //public GameObject currentLine;

    public LineRenderer lineRenderer;
    //public Transform startPos;
    public Transform endPosition;


    public void StartDrawingLine(Vector3 startPos)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPos);
    }

    public void EndDrawingLine(Vector3 endPos)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, endPos);
    }

    public void KeepDrawing()
    {
        lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9f));
    }

    public void DeleteLine()
    {
        Destroy(gameObject);
    }
    
}
