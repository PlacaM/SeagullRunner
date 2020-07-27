using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    private bool bIsPressing;
    private LineRenderer lineRenderer;

    private Vector2 startTouchScreeen,startTouchWorld;
    private Vector2 endTouchScreeen, endTouchWorld;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        bIsPressing = false;
        startTouchWorld = transform.position;
    }
     

    void SetStartPoint(Vector2 touchPosition)
    {
        startTouchScreeen = touchPosition;
        startTouchWorld = Camera.main.ScreenToWorldPoint(touchPosition);
         
    }
    void SetEndPoint(Vector2 touchPosition)
    {
        endTouchScreeen = touchPosition;
        endTouchWorld = Camera.main.ScreenToWorldPoint(touchPosition);

    }

    public void ResetLineRenderer()
    {
        lineRenderer.positionCount = 0;
    }
    void UpdateLineRenderer()
    {
         
        

        if (lineRenderer.positionCount < 2)
            lineRenderer.positionCount = 2;


        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endTouchWorld);
         
    }


    public void DrawLine(Vector2 startPosition,Vector2 endPosition)
    {
        startTouchWorld = startPosition;
        endTouchWorld = endPosition;
        UpdateLineRenderer();
    }

    public void DrawLine( Vector2 endPosition)
    {
        DrawLine(startTouchWorld, endPosition);

    }
    public void DrawLine(Vector2 direction,float distance)
    {
        DrawLine(startTouchWorld, startTouchWorld + direction * distance);

    }
    
}
