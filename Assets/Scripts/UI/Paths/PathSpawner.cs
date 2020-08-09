using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [SerializeField] Path path;
  
    public void InstanitatePath(Transform origin,Transform destiniation,PathType pathType)
    {
     
        Path newPath = Instantiate(path, transform.position, Quaternion.identity);
        newPath.SetPathType(pathType);
        SetOriginAndDestination(newPath, origin, destiniation);

    }

    public void SetOriginAndDestination(Path path,Transform origin, Transform destiniation)
    {
        path.SetOrigin(origin);
        path.SetDestiniation(destiniation);
    }
}
