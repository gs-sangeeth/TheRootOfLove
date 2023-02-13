using System.Collections;
using UnityEngine;

public class RandomTentacles : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MoveRandomly(Random.Range(.5f, 1.2f)));
    }

    private IEnumerator MoveRandomly(float time)
    {
        float elapsedTime = 0;

        var angle = Random.Range(90f, 270f);

        while (elapsedTime < time)
        {
            var pos = RandomCircle(transform.position, 1, angle);
            for (int i = 0; i < Random.Range(2, 6); i++)
            {
                transform.position = Vector2.MoveTowards(transform.position, pos, Random.Range(.2f, .5f) * Time.deltaTime);
                yield return null;
            }
            elapsedTime += Time.deltaTime;
        }
    }

    Vector2 RandomCircle(Vector2 center, float radius, float fov)
    {
        float ang = Random.Range(fov - 90, fov + 90);

        Vector2 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);

        return pos;
    }
}
