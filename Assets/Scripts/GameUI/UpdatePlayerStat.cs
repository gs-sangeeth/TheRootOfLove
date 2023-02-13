using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerStat : MonoBehaviour
{
  public FloatSO level;
  public Slider levelUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      levelUI.value = (level.value/level.maxValue);
    }
}
