using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 target;
    public float range;
    public float speed;

    bool attack;

    public LayerMask root;

    private Collider2D collider;

    public FloatSO nutrientLevel, waterLevel;
    public float drainRate;

    public Sprite deathSprite;

    private bool gonnaDie = false;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    public void Update()
    {
        Vector3 diff = target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


        if (!attack)
        {

            float dist = Vector3.Distance(target, transform.position);
            if (dist <= range)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
        }
        else
        {
            waterLevel.DecrementBy(drainRate * Time.deltaTime);
        }



        CheckEnemyStrangle();
    }

    private void CheckEnemyStrangle()
    {
        //var topRight = new Vector2(collider.bounds.max.x, collider.bounds.max.y);
        //var bottomRight = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
        //var bottomLeft = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
        //var topLeft = new Vector2(collider.bounds.min.x, collider.bounds.max.y);


        //Debug.DrawRay(transform.position, transform.right * 2f, Color.red);
        //Debug.DrawRay(transform.position, -transform.up * 2f, Color.red);
        //Debug.DrawRay(transform.position, -transform.right * 2f, Color.red);
        //Debug.DrawRay(transform.position, transform.up * 2f, Color.red);

        var topRightHit = Physics2D.Raycast(transform.position, transform.right, 2f, root);
        var bottomRightHit = Physics2D.Raycast(transform.position, -transform.up, 2f, root);
        var bottomLeftHit = Physics2D.Raycast(transform.position, -transform.right, 2f, root);
        var topLeftHit = Physics2D.Raycast(transform.position, transform.up, 2f, root);

        if (topRightHit.collider != null && bottomRightHit.collider != null && bottomLeftHit.collider != null && topLeftHit.collider != null)
        {
            if (!gonnaDie)
            {
                gonnaDie = true;
                nutrientLevel.IncrementBy(10);
                AudioManager.instance.Pause("Enemy");

                if (GameObject.Find("EnemyManager").GetComponent<EnemySpawn>().finalLevel)
                {
                    GameObject.Find("EnemyManager").GetComponent<EnemySpawn>().Killed();
                }

                StartCoroutine(Die());
            }
        }

        print(gonnaDie);
    }

    private IEnumerator Die()
    {
        attack = false;
        //yield return new WaitForSeconds(2);

        var spriteObj = transform.GetChild(1).gameObject;
        spriteObj.GetComponent<Animator>().enabled = false;
        spriteObj.GetComponent<SpriteRenderer>().sprite = deathSprite;

        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }



    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Root"))
        {
            attack = true;
            AudioManager.instance.Play("Enemy");
        }
    }
}
