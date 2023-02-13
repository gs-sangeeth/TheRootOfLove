using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        print("button");
    }
    public void Credits()
    {
        SceneManager.LoadScene(2);
        //print("button");
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
        //print("button");
    }
}
