using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GridMaker : MonoBehaviour
{

    public GameObject[] itemsToPickFrom;
    public int gridX;
    public int gridY;
    public float gridSpacingOffset = 1f;
    public Vector2 parentVec;





    

    // Start is called before the first frame update
    void Start()
    {

        SpawnGrid();

       // parentVec =  new Vector2(transform.parent.position.x, transform.parent.position.y);
    }


    [Button]
    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector2 spawnPosition = new Vector2(x * gridSpacingOffset, y * gridSpacingOffset);

                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length);
        GameObject clone = Instantiate(itemsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn);
        clone.transform.SetParent(transform, false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
