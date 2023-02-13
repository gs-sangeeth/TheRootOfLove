using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWave : MonoBehaviour
{
    public float timeInterval;
    private float timer;
    public int enemyCount;
    int counter;

    [Header("Dont Use this. Only for last wave")]
    public bool isLastWave;

    [HideInInspector]
    public bool spawnStart;

    EnemySpawn e;
    ProgressionController p;


    // Start is called before the first frame update
    void Start()
    {
      p = GameObject.Find("GameManager").GetComponent<ProgressionController>();
        counter = 0;
        e = GameObject.Find("EnemyManager").GetComponent<EnemySpawn>();
        spawnStart = false;
        timer = timeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if ((timer < 0) && (spawnStart))
        {
            timer = timeInterval;
            e.Spawn();
            counter++;
            if (counter >= enemyCount)
            {
              Destroy(this.gameObject);
              //else
              //  FinishGame();
            }
        }
        timer -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Root")
        {
            spawnStart = true;
        }
    }

    //public void FinishGame()
    //{

    //  StartCoroutine(LastScene());

    //}

    //IEnumerator LastScene()
    //{
    //  StartCoroutine(Camera.main.GetComponent<CameraController>().CutScene(p.right, p.left, 5f));

    //  yield return new WaitForSeconds(5f);

    //  StartCoroutine(Camera.main.GetComponent<CameraController>().CutScene(p.left, GameObject.Find("StartPostion").transform, 3f));

    //  yield return new WaitForSeconds(3f);

    //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

}
