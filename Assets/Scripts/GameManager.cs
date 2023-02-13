using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public FloatSO waterLevel, lives, nutrients;

    public GameObject MainTree;
    public List<Transform> livesUI;

    private void Start()
    {
        nutrients.value = 0;

        waterLevel.level = 0;
        AudioManager.instance.Play("MainSound");
    }

    private void Update()
    {
        if (nutrients.value >= nutrients.maxValue)
        {
            nutrients.value = 0;
            nutrients.maxValue += 50;
            nutrients.level += 1;

            if(nutrients.level < 4)
            {
              GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().PopFromUntraversableSurfaces();
              waterLevel.level++;
              StartCoroutine(Camera.main.GetComponent<CameraController>().CutScene(MainTree.transform, GameObject.FindGameObjectWithTag("Player").transform, 3f,2f));
              StartCoroutine(MainTree.GetComponent<MainTree>().ChangeTreeSprite(waterLevel.level,3f));
            }

            else
              IncreaseLife();

        }
    }
    public void IncreaseLife()
    {
      lives.value++;
      livesUI[(int)lives.value - 1].gameObject.SetActive(true);

    }
    public void DecreaseLife()
    {

      livesUI[(int)lives.value - 1].gameObject.SetActive(false);
      lives.value--;
    }
}
