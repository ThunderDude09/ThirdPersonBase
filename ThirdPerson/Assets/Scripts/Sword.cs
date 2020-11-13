using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Sword : MonoBehaviour
{
    public bool isAttacking = false;

    public float rotationSpeed;

    public static int moveSpeed = 4;
    public Vector3 userDirection = Vector3.right;
    bool playerHasThrown = false;
    bool playerHasSword = true;

    public float throwForce = 50;
    public Transform target, curve_point;
    private Vector3 old_pos;
    private bool isReturning = false;
    private float time = 0.0f;

    public GameObject hand;

    Animator anim;

    Rigidbody sword;

    float timeSinceButtonWasPressed = 0.0f;
    float timeSinceButtonHeld = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        sword = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        sword.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            sword.detectCollisions = true;
            isAttacking = true;
            timeSinceButtonHeld = 0;
        }
        if (isAttacking == true)
        {
            if (timeSinceButtonHeld > .5f)
            {
                sword.detectCollisions = false;
                isAttacking = false;
            }
        }
        

        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Left Click");
            if (Input.GetMouseButtonDown(0) && playerHasSword == true)
            {
                //Debug.Log("Right Click");
                transform.parent = null;
                //sword.isKinematic = false;
                sword.detectCollisions = true;
                playerHasSword = false;
                //anim.SetTrigger("ThrowSword");
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
                sword.rotation = Quaternion.Slerp(sword.transform.rotation, target.rotation, 50 * Time.deltaTime);
                time += Time.deltaTime;
            }else
            {
                ResetSword();
            }
        }
        timeSinceButtonWasPressed += Time.deltaTime;
        timeSinceButtonHeld += Time.deltaTime;
        /*if (playerHasThrown == true)
        {
            
            //transform.Translate(8 * Time.deltaTime,0,0);
            //transform.Rotate(0, 0, -.5f);
        }*/
    }

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
        time = 0.0f;
        old_pos = sword.position;
        isReturning = true;
        sword.velocity = Vector3.zero;
        sword.isKinematic = true;
        playerHasSword = true;
    }

    void ResetSword()
    {
        isReturning = false;
        //sword.transform.parent = transform;
        sword.position = target.position;
        sword.rotation = target.rotation;
        sword.detectCollisions = false;
        transform.parent = hand.transform;
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
            sword.isKinematic = true;
            //swordCollider.enabled = false;
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("triggered");
            sword.isKinematic = true;
        }
    }
}
