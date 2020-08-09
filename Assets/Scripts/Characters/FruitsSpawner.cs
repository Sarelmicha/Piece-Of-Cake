using System;
using System.Collections;
using UnityEngine;

public class FruitsSpawner : MonoBehaviour
{

    [SerializeField] GameObject[] cupcakes;
    [SerializeField] GameObject catchersHolder;
   
    [SerializeField] float minSummonTime = 0.5f;
    [SerializeField] float maxSummonTime = 1f;
    [SerializeField] float circleSpawnTime = 0.5f;
    [SerializeField] UIController uiController;

    int startCircularIndex;
    int runningCircularIndex;
    int cupcakeIndex;

    private SpawnType spawnType = SpawnType.Regular;

    private void Awake()
    {
        uiController = GameObject.FindWithTag("UI Controller").GetComponent<UIController>();
    }

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

            SetCupcakeIndex();

            Instantiate(cupcakes[cupcakeIndex], transform.position, Quaternion.identity);
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

            SetCupcakeIndex();
            Instantiate(cupcakes[cupcakeIndex], transform.position, Quaternion.identity);
            runningCircularIndex++;
      
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

    private void SetCupcakeIndex()
    {

        switch (uiController.GetGameMode())
        {
            case GameMode.Regular:
                cupcakeIndex = UnityEngine.Random.Range(0, cupcakes.Length);
                break;
            case GameMode.DoubleTwo:
                //Raffle a random index without the bonus fruit
                cupcakeIndex = UnityEngine.Random.Range(0, cupcakes.Length - 1);
                break;
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
