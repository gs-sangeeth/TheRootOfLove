using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour
{

    public ProgressionController p;
    public Sprite newTree;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Some Some");
        if (collider.tag == "Root")
        {
            p.NextLevel();

            StartCoroutine(ChangeTreeSprite());
            this.transform.GetChild(0).gameObject.SetActive(false);

            GetComponent<PolygonCollider2D>().enabled = false;
            //gameObject.SetActive(false);
        }
        //SceneManager.LoadScene(buildindex);
    }

    IEnumerator ChangeTreeSprite()
    {
        yield return new WaitForSeconds(3.5f);
        GetComponent<SpriteRenderer>().sprite = newTree;
    }
}
