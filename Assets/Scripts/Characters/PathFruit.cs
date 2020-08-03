using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFruit : MonoBehaviour, ISpecialPower
{
    GameObject catchersHolder;
    [SerializeField] PathType pathType;

    GameObject pathSpawner;
    Transform origin;
    Transform destination;
    
    int index;
    bool isDeadByClicking = false;

    private void Awake()
    {
        pathSpawner = GameObject.FindWithTag("Path Spawner");
        catchersHolder = GameObject.FindWithTag("Catchers Holder");
    }


    private void OnDestroy()
    {
     
        if (isDeadByClicking)
        {
            //TODO - Change path to regular. need to create another cupcake for circular
            pathSpawner.GetComponent<PathSpawner>().InstanitatePath(origin, destination,pathType);
        }
    }

    public void IsDeadByClicking(bool isDeadByClicking)
    {
        this.isDeadByClicking = isDeadByClicking;
    }

    public void SetDestination(Transform destination)
    {
        if (destination.transform.position != origin.transform.position)
        {
            this.destination = destination;
        }

        else
        {
            if (index == catchersHolder.transform.childCount - 1)
            {
                this.destination = catchersHolder.transform.GetChild(0);
            }
            else
            {
                this.destination = catchersHolder.transform.GetChild(index + 1);
            }

           
        } 
    }

    public void InvokeSpecialPower(CatcherController catcher)
    {
        //Set Origin
        origin = catcher.gameObject.transform;
        //Raffle a random index from catchers
        index = Random.Range(0, catchersHolder.transform.childCount);
        //Set destination
        SetDestination(catchersHolder.transform.GetChild(index));
        IsDeadByClicking(true);
        catcher.RightClick();
    }
}
