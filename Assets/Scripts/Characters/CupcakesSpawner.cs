using System;
using System.Collections;
using UnityEngine;

public class CupcakesSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] cupcakes;
    [SerializeField] GameObject catchersHolder;
   
    [SerializeField] float minSummonTime = 0.5f;
    [SerializeField] float maxSummonTime = 1f;
    [SerializeField] float circleSpawnTime = 0.5f;

    int startCircularIndex;
    int runningCircularIndex;

    private SpawnType spawnType = SpawnType.Regular;

    // Update is called once per frame
    void Start()
    {
         StartCoroutine(CheckSpawn());
    }

    public IEnumerator CheckSpawn()
    {
        while (true)
        {
            switch (spawnType)
            {
                case SpawnType.Regular:
                    yield return RegularSpawn();
                    break;
                case SpawnType.Circular:
                    yield return CircularSpawn();
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }  
    }

    public IEnumerator RegularSpawn()
    {
        while (spawnType == SpawnType.Regular)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSummonTime, maxSummonTime));

            int randomIndex = UnityEngine.Random.Range(0, cupcakes.Length);
            Instantiate(cupcakes[randomIndex], transform.position, Quaternion.identity);
        }
    }

    public IEnumerator CircularSpawn()
    {
         startCircularIndex = UnityEngine.Random.Range(0, catchersHolder.transform.childCount);
         runningCircularIndex = startCircularIndex + 1;
        if (runningCircularIndex == catchersHolder.transform.childCount)
        {
            runningCircularIndex = 0;
        }

        while (runningCircularIndex != startCircularIndex)
        {
            yield return new WaitForSeconds(circleSpawnTime);

            int cupcakeIndex = UnityEngine.Random.Range(0, cupcakes.Length);
            Instantiate(cupcakes[cupcakeIndex], transform.position, Quaternion.identity);
            runningCircularIndex++;

            print("start Index is " + startCircularIndex);
            print("runIndex is " + runningCircularIndex);
      
            if (runningCircularIndex == catchersHolder.transform.childCount)
            {
                runningCircularIndex = 0;
            }
        }

        if (runningCircularIndex == startCircularIndex)
        {
            spawnType = SpawnType.Regular;
            yield return RegularSpawn();
        }
    }

    public SpawnType GetSpawnType()
    {
        return this.spawnType;
    }

    public void SetSpawnType(SpawnType spawnType)
    {
        this.spawnType = spawnType;
    }

    public int GetRunningCircularIndex()
    {
        return this.runningCircularIndex;
    }
}
