using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public Transform player;
    public List<Vector2> root;
    public Transform enemy;

    public float timeInterval;
    private float timer;

    public Vector2 point;

    public float minDistance, maxDistance;

    public List<Transform> spawnedEnemies;

    public int killed;
    public bool finalLevel;

    public int killRequired;

    void Start()
    {
        timer = timeInterval;
        spawnedEnemies = new List<Transform>();
    }

    void Update()
    {
        // if (timer < 0)
        // {
        //     timer = timeInterval;
        //     Spawn();
        // }
        // timer -= Time.deltaTime;


    }

    public void Spawn()
    {
        root = player.GetComponent<PlayerMovement>().playerPositionHistory;
        point = root[Random.Range(0, root.Count)];

        Vector2 spawnPoint = new(Random.Range(minDistance, maxDistance), Random.Range(minDistance, maxDistance));
        Transform temp = Instantiate(enemy, new Vector3(point.x + spawnPoint.x * EitherOneOrMinusOne(), point.y + spawnPoint.y * EitherOneOrMinusOne()), Quaternion.identity);

        temp.GetComponent<EnemyController>().target = new Vector3(point.x, point.y, 0);
        spawnedEnemies.Add(temp);
    }

    public void Killed()
    {
      killed++;

      if(killed >= killRequired)
      {
        SceneManager.LoadScene(2);
      }
    }

    int EitherOneOrMinusOne()
    {
        var res = Random.Range(0, 2);
        if (res == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}
