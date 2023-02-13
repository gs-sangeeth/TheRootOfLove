using UnityEngine;

public class Surface : MonoBehaviour
{
    public SurfaceType surfaceType;
}

public enum SurfaceType
{
    easy,
    normal,
    hard,
    extreme,
    sky,
}