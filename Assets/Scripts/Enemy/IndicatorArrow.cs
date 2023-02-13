using UnityEngine;

public class IndicatorArrow : MonoBehaviour
{
    public Transform enemy;

    private void Update()
    {
        Vector3 diff = enemy.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
