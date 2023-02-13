using UnityEngine;

public class Restrictobstacle : MonoBehaviour
{

    
    //public float speed = 10f;

    //private Vector2 lastClickedPos;
    //public bool moving;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        moving = true;
    //    }

    //    if (moving && (Vector2)transform.position != lastClickedPos)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, speed * Time.deltaTime);
    //    }
    //    else
    //    {
    //        moving = false;
    //    }
    //}

    
    //private void OnCollisionStay2D(Collision2D collision)
    //{
        

    //    if (collision.gameObject.CompareTag("Obstacle"))
    //    {
    //       Playerscript.moving  = false;
            
    //    }


       

    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Root"))
    //    {
    //        print("rootFound");
    //        Playerscript.moving = false;

    //    }

    //    if(collision.gameObject.CompareTag("Water"))
    //    {
    //        print("waterFound");
    //        Destroy(collision.gameObject);

    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }

}
