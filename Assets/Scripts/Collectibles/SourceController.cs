using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceController : MonoBehaviour
{
  public float minValue, maxValue;
  public float valueSet, currentValue, speed;
  bool drawingWater;

  public AudioSource WaterSlurp;
    public AudioClip clip;

  public FloatSO waterLevel;
    bool played;

    // Start is called before the first frame update
    void Start()
    {
      valueSet = Random.Range(minValue, maxValue);
      currentValue = valueSet;
      drawingWater = false;
        played = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
      if(drawingWater)
      {
           

            if (waterLevel.value < 100)
        {
          currentValue-=Time.deltaTime * speed;
          waterLevel.IncrementBy(Time.deltaTime * speed);

          Color color = this.transform.GetChild(0).GetComponent<SpriteRenderer>().color ;
          color.a = (currentValue / valueSet);
          this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = color ;

      //  this.transform.GetChild(0).localScale = new Vector3(((currentValue / valueSet)), ((currentValue / valueSet)), ((currentValue / valueSet)));

        if(currentValue <= 1 )
          Destroy(this.gameObject);
        }
      }

    }

    public void OnTriggerEnter2D(Collider2D c)
    {
      //Debug.Log("something");
      if(c.tag == "Root")
      {
            //ebug.Log("ss");
            drawingWater = true;

            if (!played)
            {
                played = true;
                // WaterSlurp.PlayOneShot(clip);
                // WaterSlurp.Play();
                AudioManager.instance.Play("Water");
            }

        }
    }


}
