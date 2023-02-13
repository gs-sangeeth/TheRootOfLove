using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    private Vector2 lastClickedPos;

    [HideInInspector]
    public bool moving;

    public LayerMask root;
    private float splitTimer;

    public GameObject trail;

    [HideInInspector]
    public List<Vector2> playerPositionHistory;

    public FloatSO waterLevel;
    public float startWaterlevel = 30f;

    public LayerMask surfaces;

    private GameObject restart;

    public List<SurfaceType> untraversableSurfaces;


    private bool justDead = false;

    public GameObject smallTentacle;
    public float minTentacleSpawnTime = 1f;
    public float maxTentacleSpawnTime = 4f;

    private GameObject oldTrailsParent;

    private void Awake()
    {
        restart = GameObject.FindGameObjectWithTag("StartPostion");
        transform.position = restart.transform.position;
    }

    private void Start()
    {
        CreateTrail(transform.position);
        StartCoroutine(StorePlayerPositions());

        waterLevel.value = startWaterlevel;

        StartCoroutine(SpawnTentacles());

        oldTrailsParent = new GameObject("TrailCollidersParent");
    }

    private void Update()
    {
        // Rotate player to look at cursor
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        // Look for untraversable surfaces
        Debug.DrawRay(transform.position, transform.up * .2f, Color.red);
        var surfaceDetectHit = Physics2D.Raycast(transform.position, transform.up, .2f, surfaces);
        if (surfaceDetectHit.collider != null)
        {
            if (untraversableSurfaces.Contains(surfaceDetectHit.collider.gameObject.GetComponent<Surface>().surfaceType))
            {
                StartCoroutine(TurnBack(rot_z));
            }
        }

        //Movement and Input

        if (Input.GetMouseButtonUp(0))
        {
            justDead = false;
        }

        if (Input.GetMouseButton(0))
        {
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mousePointFar = new Vector3(mousePoint.x, mousePoint.y, 10);

            var hit = Physics2D.Raycast(mousePoint, mousePointFar, 10f, root);
            if (hit.collider != null && splitTimer == 0 && !moving)
            {
                splitTimer = .5f;
                RemoveTrail();
                transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
                CreateTrail(new Vector2(mousePoint.x, mousePoint.y));
            }
            else if (!justDead)
            {
                moving = true;
            }
        }
        else
        {
            moving = false;
        }

        if (splitTimer > 0)
        {
            splitTimer -= Time.deltaTime;
        }
        else
        {
            splitTimer = 0;
        }

        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + transform.up, speed * Time.deltaTime);
            if (!AudioManager.instance.IsPlaying("root"))
            {
                AudioManager.instance.Play("root");
            }
        }
        else
        {
            moving = false;
            if (AudioManager.instance.IsPlaying("root"))
            {
                AudioManager.instance.Pause("root");
            }
        }


    }

    private void FixedUpdate()
    {
        if (moving)
        {
            TrackWaterLevel();
        }
    }

    private void TrackWaterLevel()
    {
        if (waterLevel.value > 0)
        {
            waterLevel.DecrementBy(waterLevel.levelData[waterLevel.level]);
        }
        else
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        print("Dead");
        restart.GetComponent<Restartscript>().Death();
        justDead = true;
        moving = false;
    }

    public void RemoveTrail()
    {
        if (transform.childCount > 0)
        {
            GameObject trail = transform.GetChild(0).gameObject;
            trail.transform.parent = oldTrailsParent.transform;
            var trailCollider = trail.GetComponent<TrailCollisions>();
            trailCollider.trailCollider.gameObject.layer = LayerMask.NameToLayer("Root");
            trailCollider.trailCollider.gameObject.transform.parent = oldTrailsParent.transform;
            trailCollider.isActiveTrail = false;
        }
    }

    public void CreateTrail(Vector2 pos)
    {
        RemoveTrail();

        var newTrail = Instantiate(trail, pos, Quaternion.identity);
        newTrail.transform.parent = transform;
    }

    IEnumerator StorePlayerPositions()
    {
        if (playerPositionHistory.Count == 0)
        {
            playerPositionHistory.Insert(0, transform.position);
        }
        while (true)
        {
            if (playerPositionHistory[0] != (Vector2)transform.position)
            {
                playerPositionHistory.Insert(0, transform.position);
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator TurnBack(float rot_z)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
        yield return StartCoroutine(SmoothLerp(.2f));
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

    }

    private IEnumerator SmoothLerp(float time)
    {
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + (transform.up * .2f);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void PopFromUntraversableSurfaces()
    {
        untraversableSurfaces.RemoveAt(0);
    }

    IEnumerator SpawnTentacles()
    {
        var tentacleParent = new GameObject("TentacleParent");
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTentacleSpawnTime, maxTentacleSpawnTime));
            if (moving)
            {
                var tentacle = Instantiate(smallTentacle, transform.position, Quaternion.identity);
                tentacle.transform.parent = tentacleParent.transform;
            }
        }
    }
}
