using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.
    Made simple to use (drag and drop, done) for regular keyboard layout
    wasd : basic movement
    shift : Makes camera accelerate
    space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/


    public Transform player;
    public float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    public float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    public List<string> boundaryListOrder;
    public List<Transform> boundary;

    private bool canMove;

    void Start()
    {
        canMove = true;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Focus(player, 1.5f);
        }
        // lastMouse = Input.mousePosition - lastMouse ;
        // lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        // lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        // transform.eulerAngles = lastMouse;
        // lastMouse =  Input.mousePosition;
        //Mouse  camera angle done.

        // Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        // mouseWorldPosition.z = 0f;
        // transform.position = mouseWorldPosition;
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        { // only move while a direction key is pressed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                //p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }
        }

        p = p * Time.deltaTime;
        Vector3 newPosition = transform.position;
        //  //If player wants to move on X and Z axis only
        //     // transform.Translate(p);
        //     // newPosition.x = transform.position.x;
        //     // newPosition.z = transform.position.z;
        //     // transform.position = newPosition;
        // } else {
        if (canMove)
            transform.Translate(p);
        // }
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if ((Input.GetKey(KeyCode.W)) && (transform.position.y < boundary[3].position.y))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if ((Input.GetKey(KeyCode.S)) && (transform.position.y > boundary[2].position.y))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if ((Input.GetKey(KeyCode.A)) && (transform.position.x > boundary[0].position.x))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if ((Input.GetKey(KeyCode.D)) && (transform.position.x < boundary[1].position.x))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }

        return p_Velocity;
    }
    public void Focus(Transform focusObject, float timeTaken)
    {
        // this.transform.position = new Vector3(focusObject.position.x, focusObject.position.y, -1);
        StartCoroutine(SmoothLerp(timeTaken, new Vector3(focusObject.position.x, focusObject.position.y, -1)));

    }

    private IEnumerator SmoothLerp(float time, Vector3 finalPos)
    {
        Vector3 startingPos = transform.position;
        //Vector3 finalPos = transform.position + (transform.forward * 5);
        float elapsedTime = 0;

        finalPos = new Vector3(finalPos.x, finalPos.y, transform.position.z);

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator CutScene(Transform tree1, Transform tree2, float duration,float stayDuration = 0f)
    {
        //Debug.Log("HI");
        Focus(tree1, duration);
        canMove = false;
        //disable drag

        yield return new WaitForSeconds(duration + stayDuration);



        //enable drag
        Focus(tree2, duration);

        yield return new WaitForSeconds(duration);

        canMove = true;
    }
}
