using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SpawnObject
{
    // for debug :
    public string Name;

    public GameObject Prefab;
    [Range(0f, 100f)] public float Chance = 100f;

    [HideInInspector] public double _weight;
}


public class SpawnWithProbability : MonoBehaviour
{
    [SerializeField] private SpawnObject[] objects;

    private double accumulatedWeights;
    private System.Random rand = new System.Random();



    private void Awake()
    {
        CalculateWeights();
    }




    // Start is called before the first frame update
    void Start()
    {

        //for (int i = 0; i < 200; i++)
        SpawnRandomObject(new Vector2(Random.Range(-2f, 1f), Random.Range(-1f, 2f)));

      //  SpawnRandomObject();

    }


    private void SpawnRandomObject(Vector2 pos)
    {
        SpawnObject randomEnemy = objects[GetRandomSpawnIndex()];

        Instantiate(randomEnemy.Prefab, transform);

        // This line is not required (debug) :
        //Debug.Log("<color=" + randomEnemy.Name + ">●</color> Chance: <b>" + randomEnemy.Chance + "</b>%");
    }

    private int GetRandomSpawnIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;

        for (int i = 0; i < objects.Length; i++)
            if (objects[i]._weight >= r)
                return i;

        return 0;
    }


    private void CalculateWeights()
    {
        accumulatedWeights = 0f;
        foreach (SpawnObject enemy in objects)
        {
            accumulatedWeights += enemy.Chance;
            enemy._weight = accumulatedWeights;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //SpawnRandomEnemy(new Vector2(0, 0));
    }
}
