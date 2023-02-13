using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{

    private Vector3 Origin;
    private Vector3 Difference;
    public Transform player;

    private bool drag = false;

    public List<Transform> boundary;

    Vector3 p_Velocity = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        //    ResetCamera = Camera.main.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference * 1f;
        }

        if (Input.GetMouseButton(1))
            Camera.main.transform.GetComponent<CameraController>().Focus(player, 1.5f);





    }



}
