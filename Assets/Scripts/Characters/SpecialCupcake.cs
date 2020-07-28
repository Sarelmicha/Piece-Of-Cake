using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCupcake : MonoBehaviour, ISpecialPower
{
    GameObject pathSpawner;
    [SerializeField] GameObject catchersHolder;
    Transform origin;
    Transform destination;
    int index;



    bool isDeadByClicking = false;

    private void Awake()
    {
        pathSpawner = GameObject.FindWithTag("Path Spawner");
    }


    private void OnDestroy()
    {
     
        if (isDeadByClicking)
        {

            //pathSpawner.GetComponent<PathSpawner>().InstanitatePath(origin, destination);
            pathSpawner.GetComponent<PathSpawner>().InstantiateCircularPath(origin, destination);

        }
    }

    public void IsDeadByClicking(bool isDeadByClicking)
    {
        this.isDeadByClicking = isDeadByClicking;
    }

    public void SetOrigin(Transform origin)
    {
        if (origin.transform.position != destination.transform.position)
        {
            this.origin = origin;
        }

        else
        {
            this.origin = catchersHolder.transform.GetChild(index + 1);
        } 
    }

    public void InvokeSpecialPower(GameObject catcher)
    {

        index = Random.Range(0, catchersHolder.transform.childCount);
        destination = catchersHolder.transform.GetChild(index);

        SetOrigin(catcher.gameObject.transform);
        IsDeadByClicking(true);
        catcher.GetComponent<Animator>().SetTrigger("rightClick");
    }
}
