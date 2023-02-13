using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public int upperLimit = 6;
    public int lowerLimit = 0;

    public int RandomValue;


    
    // Start is called before the first frame update
    void Start()
    {

        RandomValue = Random.Range(lowerLimit, upperLimit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D c)
    {
        //  Debug.Log(c.collider.name);

        if (c.collider.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log(RandomValue);
        }
        


    }
}
