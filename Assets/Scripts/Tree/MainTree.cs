using System.Collections;
using UnityEngine;

public class MainTree : MonoBehaviour
{
    public IEnumerator ChangeTreeSprite(int level,float waitTime = 3f)
    {
        yield return new WaitForSeconds(waitTime);
        int i = 0;
        foreach(Transform sprites in transform)
        {
            if(level == i)
            {
                sprites.gameObject.SetActive(true);
            }
            else
            {
                sprites.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
