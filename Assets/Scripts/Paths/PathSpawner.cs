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
        newPath.SetOrigin(origin.position);
        newPath.SetDestiniation(destiniation.position);

    }
}
