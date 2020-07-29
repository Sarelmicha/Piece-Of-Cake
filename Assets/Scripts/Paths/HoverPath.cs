using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPath : MonoBehaviour
{
    [SerializeField] GameObject catchersHolder;

    private Vector2 swipeStart;
    private Vector2 swipeEnd;
    private LineRenderer lineRenderer;
    private bool isHover = false;
    private Path path;
    private float counter;
    private Vector2 origin;
    private Vector2 destination;
    private int nextVericeIndex;
    private int currentLineRenderVerticeIndex = 0;
    private int numOfVertices;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.45f;
        lineRenderer.endWidth = 0.45f;
        path = GetComponentInParent<Path>();
        numOfVertices = catchersHolder.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
          
        this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !isHover && Vector2.Distance(swipeStart, path.GetStartPosition()) < 1f) 
        {

            //Set the swipe start position
            this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Set is hover to be true
            isHover = true;

            origin = swipeStart;
            nextVericeIndex = path.GetNextVerticeIndex(origin);
            print(nextVericeIndex);
            if (nextVericeIndex == -1)
            {
                return;
            }
            destination = catchersHolder.transform.GetChild(nextVericeIndex).position;
            lineRenderer.SetPosition(currentLineRenderVerticeIndex++, origin);
        }
       
        else if (Input.GetMouseButtonUp(0))
        {
            isHover = false;
            swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!CheckFullPath())
            {
                Destroy(transform.parent.gameObject);
            }
        }

        if (isHover)
        {
           DrawHoverPath();
        }
        
    }

    private void DrawHoverPath()
    {
        /*if (Vector2.Distance(lineRenderer.GetPosition(1), destination) < 0.5)
        {
            return;
        }*/

        swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetPositions(currentLineRenderVerticeIndex, swipeEnd);
       

        //Check if we reached the next vertice
        if (ReachedNextVertice())
        {
            if (!CheckFullPath())
            {
                currentLineRenderVerticeIndex++;
                nextVericeIndex++;
                if (nextVericeIndex == numOfVertices)
                {
                    nextVericeIndex = 0;
                }
                origin = destination;
                destination = catchersHolder.transform.GetChild(nextVericeIndex).position;
            }
        }
    }

    private bool ReachedNextVertice()
    {
        return Vector2.Distance(destination, swipeEnd) < 0.5f;
    }

    private void SetPositions(int position, Vector3 pointAlongLine)
    {
        for (int i = position; i < numOfVertices; i++)
        {
            lineRenderer.SetPosition(i, pointAlongLine);
        }
    }

    private bool CheckFullPath()
    {

        if (path == null)
        {
            return false;
        }

        if (IsPathVerticesAlign())
        {
            //Award score
            Destroy(transform.parent.gameObject);
            return true;
        }

        return false;
        //lineRenderer.SetPosition(1, origin);
    }

    private bool IsPathVerticesAlign()
    {

        if (path.GetPathType() == PathType.Line)
        {
            return IsLinePathAlign();
        }
        else if (path.GetPathType() == PathType.Circular)
        {
            return isCircularPathAlign();
        }

        return false;
    }

    private bool isCircularPathAlign()
    {
        int startVerticeIndex = path.GetVerticeIndex(lineRenderer.GetPosition(0));

        if (startVerticeIndex == -1)
        {
            return false;
        }

        for (int i = 0; i < numOfVertices; i++)
        {

            if (Vector2.Distance(lineRenderer.GetPosition(i), catchersHolder.transform.GetChild(startVerticeIndex++).position) > 0.5f)
            {
                return false;
            }

            if (startVerticeIndex == numOfVertices)
            {
                startVerticeIndex = 0;
            }

        }

        return true;
    }

    private bool IsLinePathAlign()
    {
        return Vector2.Distance(lineRenderer.GetPosition(0), path.GetOrigin()) < 0.5f &&
                   Vector2.Distance(lineRenderer.GetPosition(1), path.GetDestination()) < 0.5f;
    }
}
