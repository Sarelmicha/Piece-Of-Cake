using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
     GameObject catchersHolder;
    [SerializeField] float lineDrawSpeed = 6f;
    [SerializeField] float pathWidth = 0.45f;
    [SerializeField] float timeToLive = 5f;
    [SerializeField] RotationManager rotationManager;

    private LineRenderer lineRenderer;
    private float counter;
    private float dist;
    private int numOfVertices;
    private PathType pathType;
    private int currentLineRenderVerticeIndex = 0;
    private int nextVerticeIndex;
    private bool destinationIndexHasFound = false;
    private bool isStartPositionSet;
    private bool isFinishedFullPath = false;

    Transform origin;
    Transform destiniation;
    Transform startPosition;


    // Start is called before the first frame update
    void Start()
    {
        catchersHolder = GameObject.FindWithTag("Catchers Holder");

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = pathWidth;
        lineRenderer.endWidth = pathWidth;

        numOfVertices = lineRenderer.positionCount;
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {

        if (origin == null || destiniation == null)
        {
            return;
        }

        if (!isStartPositionSet)
        {
            startPosition = origin;
            isStartPositionSet = true;
        }
        

        if (!destinationIndexHasFound && pathType == PathType.Circular)
        {
            SetCircularDestination();
            lineDrawSpeed = 1;
        }

        calculateDistance();

        //Set the origin as first vertic
        lineRenderer.SetPosition(currentLineRenderVerticeIndex, origin.position);

        DrawPath(currentLineRenderVerticeIndex + 1);
        
    }

    private void SetCircularDestination()
    {
        nextVerticeIndex = GetNextVerticeIndex(origin.position);
        destiniation = catchersHolder.transform.GetChild(nextVerticeIndex);
        destinationIndexHasFound = true;
    }

    public int GetNextVerticeIndex(Vector3 vertice)
    {
        int index = GetVerticeIndex(vertice);

        if (index + 1 == numOfVertices)
        {
            return 0;
        }
        else if (index == -1)
        {
            return -1;
        }
        else
        {
            return index + 1; 
        }
           
    }

    public int GetVerticeIndex(Vector3 vertice)
    {
        //Find the index of the origin vetice
        for (int i = 0; i < numOfVertices; i++)
        {
            if (Vector2.Distance(catchersHolder.transform.GetChild(i).position, vertice) < 0.5)
            {
                return i;
            }
        }

        //Vertice not found.
        return -1;
    }

    private void calculateDistance()
    {
         dist = Vector2.Distance(origin.position, destiniation.position);
    }

    public void DrawPath(int position)
    {

        if (position == numOfVertices)
        {
            return;
        }

        if (counter < dist)
        {
            Vector3 pointA = origin.position;
            Vector3 pointB = destiniation.position;

            counter += 1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            //Get the unit vector in the desired direction, 
            //multiply by the desired length and add the starting point
            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            SetPositions(position, pointAlongLine);

        }
       
        else if (pathType == PathType.Circular)
        {

            counter = 0;
            currentLineRenderVerticeIndex++;
            nextVerticeIndex++;
            if (nextVerticeIndex == numOfVertices)
            {
                nextVerticeIndex = 0;
            }

            origin = destiniation;
            destiniation = catchersHolder.transform.GetChild(nextVerticeIndex);
            calculateDistance();
        }   
    }

    private void SetPositions(int position,Vector3 pointAlongLine)
    {
        //Set all positions to the next vertice position to avoid vector.zero bug
        for (int i = position; i < numOfVertices; i++)
        {
            lineRenderer.SetPosition(i, pointAlongLine);
        }
    }


    public void SetOrigin(Transform origin)
    {
        this.origin = origin;
    }

    public void SetDestiniation(Transform destiniation)
    {
        this.destiniation = destiniation;
    }

    public Transform GetOrigin()
    {
        return this.origin;
    }

    public Transform GetStartPosition()
    {
        return this.startPosition;
    }

    public Transform GetDestination()
    {
        return this.destiniation;
    }

    public void SetPathType(PathType pathType)
    {
        this.pathType = pathType;
    }

    public PathType GetPathType()
    {
        return pathType;
    }
}
