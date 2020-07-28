using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPath : MonoBehaviour
{

    private Vector2 swipeStart;
    private Vector2 swipeEnd;
    private LineRenderer lineRenderer;
    private bool isHover = false;
    private float dist;
    private Path path;
    private float counter;
    private Vector2 origin;
    private Vector2 destination;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.45f;
        lineRenderer.endWidth = 0.45f;
        path = GetComponentInParent<Path>();
    }

    // Update is called once per frame
    void Update()
    {

        this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distanceFromPath = Vector2.Distance(swipeStart, path.GetOrigin().position);

        if (Input.GetMouseButtonDown(0) && !isHover && distanceFromPath < 1f) 
        {

            //Set the swipe start position
            this.swipeStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Set is hover to be true
            isHover = true;

            //Set the line render config
            origin = swipeStart;
            destination = path.GetDestination().position;

            lineRenderer.SetPosition(0, origin);
            dist = Vector2.Distance(origin, destination);

        }
       
        else if (Input.GetMouseButtonUp(0))
        {
            isHover = false;
            swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CheckFullPath();
        }

        print(isHover);

        if (isHover)
        {
           DrawHoverPath();
        }
        
    }

    private void DrawHoverPath()
    {
        if (Vector2.Distance(lineRenderer.GetPosition(1), destination) < 0.5)
        {
            return;
        }
        swipeEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, swipeEnd);
    }

    private void CheckFullPath()
    {

        if (path == null)
        {
            return;
        }

        if (Vector2.Distance(lineRenderer.GetPosition(0), path.GetOrigin().position) < 1 &&
           Vector2.Distance(lineRenderer.GetPosition(1), path.GetDestination().position) < 1)
        {
            //Award score
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }

        lineRenderer.SetPosition(1, origin);
    }
}
