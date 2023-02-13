using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleChange : MonoBehaviour
{
    public GameObject[] slides;
    int clickcount;

    private void Start()
    {
        clickcount = 0;
    }
    private void OnMouseDown()
    {

        clickcount++;

        if (clickcount >= slides.Length)
        {
            SceneManager.LoadScene("AllLevels");
        }
        else
        {
            for (int i = 0; i < slides.Length; i++)
            {
                if (i == clickcount)
                {
                    slides[i].SetActive(true);
                }
                else
                {
                    slides[i].SetActive(false);

                }
            }
        }



    }

}
