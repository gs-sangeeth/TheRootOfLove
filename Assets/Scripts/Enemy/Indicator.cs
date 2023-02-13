using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject ind;
    private GameObject target;
    private Renderer rb;
    public LayerMask camBound;

    void Start()
    {
        rb = GetComponent<Renderer>();
        target = Camera.main.gameObject;
    }

    void Update()
    {
        if (rb.isVisible == false)
        {
            if (!ind.activeSelf)
            {
                ind.SetActive(true);
            }


            Vector2 direction = target.transform.position - transform.position;
            Debug.DrawRay(transform.position, direction, Color.red);
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction,1000f, camBound);
            if (ray.collider != null)
            {
                //print("collided");
                //Instantiate(ind, ray.point, Quaternion.identity);
                ind.transform.position = ray.point;
            }
        }
        else
        {
            if (ind.activeSelf)
            {
                ind.SetActive(false);
            }
        }
    }
}
