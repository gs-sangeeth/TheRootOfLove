using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Float")]
public class FloatSO : ScriptableObject
{
    public float value;
    public float maxValue;
    public int level;
    public float growFactor = 1;
    public float shrinkFactor = .1f;
    public List<float> levelData = new();

    public bool isWater;

    public void DecrementBy(float decBy)
    {
        value -= decBy * shrinkFactor;

        if ((isWater) && (value <= 0))
        {
            GameObject.FindGameObjectWithTag("StartPostion").GetComponent<Restartscript>().Death();
        }
    }

    public void IncrementBy(float incBy)
    {
        value += incBy * growFactor;

    }
}
