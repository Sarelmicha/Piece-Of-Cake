using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    GameObject catchersHolder;
    RotationManager rotationManager;
    [SerializeField] float lineDrawSpeed = 6f;
    [SerializeField] float pathWidth = 0.45f;
    [SerializeField] float timeToLive = 5f;
    [SerializeField] int scorePoints = 50;

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
    Transform destination;
    Transform startPosition;


    // Start is called before the first frame update
    void Start()
    {
        rotationManager = GameObject.FindWithTag("Rotation Manager").GetComponent<RotationManager>();
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

        if (origin == null || destination == null)
        {
            return;
        }

     
        print("dest  from up is " + destination.position);


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

       //SetPositions(currentLineRenderVerticeIndex + 1, destination.position);

       DrawPath(currentLineRenderVerticeIndex + 1);
        
    }

    private void SetCircularDestination()
    {
        nextVerticeIndex = GetNextVerticeIndex(origin.position);
        destination = catchersHolder.transform.GetChild(nextVerticeIndex);
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
         dist = Vector2.Distance(origin.position, destination.position);
    }

    public void DrawPath(int position)
    {

        if (position == numOfVertices)
        {
            return;
        }


        if (rotationManager.IsRotate())
        {
            DrawPathDuringRotation(position);
            return;
        }

        DrawPathDuringStatic(position);
    }

    private void DrawPathDuringStatic(int position)
    {
        if (counter < dist)
        {

            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;


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

            origin = destination;
            destination = catchersHolder.transform.GetChild(nextVerticeIndex);
            calculateDistance();
        }
    }

    private void DrawPathDuringRotation(int position)
    {
        SetPositions(position, destination.position);
       

        if (pathType == PathType.Circular)
        {
            int catcherIndex = GetVerticeIndex(destination.position);
            //Update all vertices
            for (int i = 1; i < catchersHolder.transform.childCount; i++)
            {
                SetPositions(i, catchersHolder.transform.GetChild(catcherIndex++).position);
                if (catcherIndex == numOfVertices)
                {
                    catcherIndex = 0;
                }
            }

        }
        return;
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
        this.destination = destiniation;
    }

    public int GetScorePoints()
    {
        if (pathType == PathType.Line)
        {
            return scorePoints;
        }
        else if (pathType == PathType.Circular)
        {
            return scorePoints * 2;
        }

        return 0;
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
        return this.destination;
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
