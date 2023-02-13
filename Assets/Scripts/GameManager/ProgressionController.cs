using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProgressionController : MonoBehaviour
{
    public Transform player;
    int level;

    public Transform left, right;
    public List<Transform> levelBoundaries;

    public List<Transform> roots;

    public EnemyWave lastLevel;
    // Start is called before the first frame update
    void Start()
    {
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

        [Button]
    public void NextLevel()
    {
        //List<SurfaceType> surface = player.GetComponent<PlayerMovement>().untraversableSurfaces;
        //surface.Remove(surface[surface.Count -1]);

        bool lastLevel = false;
        switch (level)
        {
            case 0:
                right.position = levelBoundaries[level].position;
                break;
            case 1:
                left.position = levelBoundaries[level].position;
                break;
            case 2: //right.position = levelBoundaries[level].position;
                AudioManager.instance.Pause("MainSound");
                AudioManager.instance.Play("Last");
                FinalLevel();
                lastLevel = true;
                break;
        }




        level++;

        if (!lastLevel)
        {
            StartCoroutine(Camera.main.GetComponent<CameraController>().CutScene(roots[level - 1], roots[level], 3.5f, 2f));
            StartCoroutine(CutSceneInitiator());
        }

    }

    public void FinalLevel()
    {
        lastLevel.spawnStart = true;
        GameObject.Find("EnemyManager").GetComponent<EnemySpawn>().finalLevel = true;
    }

    IEnumerator CutSceneInitiator()
    {


        yield return new WaitForSeconds(9f);

        StartCoroutine(Camera.main.GetComponent<CameraController>().CutScene(roots[level], player, 1f));

    }
}
