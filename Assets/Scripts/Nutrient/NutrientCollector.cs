using UnityEngine;

public class NutrientCollector : MonoBehaviour
{
    public int lowerLimit = 6;
    public int upperLimit = 10;

    private int RandomValue;

    public FloatSO nutrientLevel;

    public GameObject collectEffect;


    void Start()
    {

        RandomValue = Random.Range(lowerLimit, upperLimit);

        nutrientLevel.value = 20;
        nutrientLevel.maxValue = 100;
        nutrientLevel.level = 0;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("something");
        if (other.CompareTag("Root"))
        {
            AudioManager.instance.Play("nutrient");
            nutrientLevel.IncrementBy(RandomValue);
            //nutrientLevel.value += 1;
            //nutrientLevel.value += ;
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
