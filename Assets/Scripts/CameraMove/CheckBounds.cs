using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBounds : MonoBehaviour
{
    public bool hiting = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Just overlapped a collider 2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Do something

        if (collision.gameObject.tag == "Bounds")
        {
            hiting = true;
        }
    }

    //Overlapping a collider 2D
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Do something
        if (collision.gameObject.tag == "Bounds")
        {
            hiting = true;
        }
    }

    //Just stop overlapping a collider 2D
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Do something
        if (collision.gameObject.tag == "Bounds")
        {
            hiting = false;
        }
    }


}

