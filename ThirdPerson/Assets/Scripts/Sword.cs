using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public static int moveSpeed = 4;
    public Vector3 userDirection = Vector3.right;
    bool playerHasThrown = false;
    bool playerHasSword = true;

    public float throwForce = 50;
    public Transform target, curve_point;
    private Vector3 old_pos;
    private bool isReturning = false;
    private float time = 0.0f;

    Rigidbody sword;

    float timeSinceButtonHeld = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sword = GetComponent<Rigidbody>();
        sword.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {

            if (!playerHasSword)
            {

            }
            //Debug.Log("Left Click");
            if (Input.GetMouseButtonDown(0) && playerHasSword == true)
            {
                //Debug.Log("Right Click");
                transform.parent = null;
                //sword.isKinematic = false;
                //playerHasThrown = true;
                playerHasSword = false;
                sword.detectCollisions = true;
                ThrowSword();
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            ReturnSword();
            //timeSinceButtonHeld = 0;
        }

        if (isReturning)
        {
            if(time < 1.0f)
            {
                sword.position = getBQCPoint(time, old_pos, curve_point.position, target.position);
                time += Time.deltaTime;
            }
        }
        //timeSinceButtonHeld += Time.deltaTime;
        /*if (playerHasThrown == true)
        {
            
            //transform.Translate(8 * Time.deltaTime,0,0);
            //transform.Rotate(0, 0, -.5f);
        }*/
    }

    /*void throwSword()
    {
        transform.Translate(userDirection * moveSpeed * Time.deltaTime);
        //transform.Translate(1, 0, 0);
        //sword.AddForce(Vector3.forward * 500);
        //sword.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
    }*/

    void ThrowSword()
    {
        isReturning = false;
        sword.isKinematic = false;
        sword.detectCollisions = true;
        sword.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        sword.AddTorque(sword.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
    }

    void ReturnSword()
    {
        old_pos = sword.position;
        isReturning = true;
        sword.velocity = Vector3.zero;
        sword.isKinematic = true;
    }

    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment")
        {
            Debug.Log("triggered");
            playerHasThrown = false;
        }
    }
}
