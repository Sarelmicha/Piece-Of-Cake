using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPath : MonoBehaviour
{
    GameObject catchersHolder;

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
        catchersHolder = GameObject.FindWithTag("Catchers Holder");

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

        if (Input.GetMouseButtonDown(0) && !isHover && Vector2.Distance(swipeStart, path.GetStartPosition().position) < 1f)
        {

            //Set the swipe start position
            this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Set is hover to be true
            isHover = true;

            origin = swipeStart;
            nextVericeIndex = path.GetNextVerticeIndex(origin);

            if (nextVericeIndex == -1)
            {
                return;
            }

            SetPathDestination();

            lineRenderer.SetPosition(currentLineRenderVerticeIndex++, origin);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            isHover = false;
            currentLineRenderVerticeIndex = 0;
            swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!IsFullPath())
            {
                //TODO - CHANGE THIS
                ResetPath();
            }
        }

        if (isHover)
        {
           DrawHoverPath();
        }
        
    }

    private void ResetPath()
    {
        for (int i = 0; i < numOfVertices; i++)
        {
            lineRenderer.SetPosition(i, path.GetStartPosition().position);
        }
    }

    private void SetPathDestination()
    {
        switch (path.GetPathType())
        {
            case PathType.Circular:
                destination = catchersHolder.transform.GetChild(nextVericeIndex).position;
                break;
            case PathType.Line:
                destination = path.GetDestination().position;
                break;
        }
    }

    private void DrawHoverPath()
    {
     
        swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetPositions(currentLineRenderVerticeIndex, swipeEnd);
       
        //Check if we reached the next vertice
        if (ReachedNextVertice())
        {
            HoverVertice();

            if (!IsFullPath())
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

    private void HoverVertice()
    {
        if (path.GetPathType() == PathType.Circular)
        {
            CatcherController catcher = catchersHolder.transform.GetChild(nextVericeIndex).GetComponent<CatcherController>();
            if (catcher != null)
            {
                catcher.Hover();
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

    private bool IsFullPath()
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

    }

    private bool IsPathVerticesAlign()
    {

        if (path.GetPathType() == PathType.Line)
        {
            return IsLinePathAlign();
        }
        else if (path.GetPathType() == PathType.Circular)
        {
            return isCircularPathAlign(numOfVertices);
        }

        return false;
    }

    private bool isCircularPathAlign(int numOfVertices)
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
        return Vector2.Distance(lineRenderer.GetPosition(0), path.GetOrigin().position) < 0.5f &&
                   Vector2.Distance(lineRenderer.GetPosition(1), path.GetDestination().position) < 0.5f;
    }
}
