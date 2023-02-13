using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restartscript : MonoBehaviour
{
    private PlayerMovement playerscript;
    private GameObject player;
    public FloatSO waterLevel, lives;
    public GameObject restartUI;

    public List<Transform> livesUI;

    private void Start()
    {
        lives.maxValue = livesUI.Count;
        lives.value = lives.maxValue;
        player = GameObject.FindGameObjectWithTag("Player");
        playerscript = player.GetComponent<PlayerMovement>();
    }

    public void Death()
    {

        GameObject.Find("GameManager").GetComponent<GameManager>().DecreaseLife();


        playerscript.RemoveTrail();
        player.transform.position = transform.position;
        waterLevel.value = 50f;
        playerscript.CreateTrail(transform.position);

        if (lives.value <= 0)
        {
            lives.value = lives.maxValue;

            restartUI.SetActive(true);

        }

        Camera.main.GetComponent<CameraController>().Focus(player.transform, 1.5f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        lives.value = 0;

    }
}
